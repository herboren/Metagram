using Microsoft.VisualBasic.FileIO;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Point = System.Drawing.Point;

namespace Metagram
{
    public partial class Form1 : Form
    {
        EnumerateRawFileData enumRawFiles;
        EnumerateRawMetaData enumMetaData;
        RgbHistogramGenerator rgbHistGen;

        public List<string> RawFiles { get; set; }
        public List<(string, string, string)> RawMeta { get; set; }
        private string ApplicationPath { get; set; }

        private string AbsoluteFilePath { get; set; }
        public List<Bitmap> histograms { get; set; }

        /// <summary>
        /// On initialization the rawfiles are filtered based on raw type and returns as a list
        /// The list is then passed have raw meta tag data gathered and returned as a string of metadata.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            // Get Startup path of working directory
            this.ApplicationPath = (Application.StartupPath);

            // Do magic here on launch
            EnumerateFilteredRawFileData();
            EnumerateFilteredRawMeta();
            DrawListViewItems();
        }

        /// <summary>
        /// Initialize raw file data here
        /// </summary>
        public void EnumerateFilteredRawFileData()
        {
            enumRawFiles = new EnumerateRawFileData(this.ApplicationPath);
            RawFiles = enumRawFiles.ReturnFileList();
        }

        public void EnumerateFilteredRawMeta()
        {
            enumMetaData = new EnumerateRawMetaData(RawFiles);
            RawMeta = enumMetaData.EnumerateRawFileTagData();
        }

        /// <summary>
        /// Draw listview data from class
        /// </summary>
        public void DrawListViewItems()
        {
            // Add listview items, subitems.
            foreach ((string, string, string) item in RawMeta)
            {
                ListViewItem items = lstvRawFileMeta.Items.Add(item.Item1);
                items.SubItems.Add(item.Item2);
                items.SubItems.Add(item.Item3);
            }

            // Align after adding list items
            foreach (ColumnHeader column in lstvRawFileMeta.Columns)
            {
                column.Width = -2;
            }
        }

        /// <summary>
        /// This control ultimately determines whats being viewed, generated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstvRawFileMeta_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Makes it easier to use in more than one place            

            if (lstvRawFileMeta.SelectedItems.Count > 0)
            {
                this.AbsoluteFilePath = ApplicationPath + "\\" + lstvRawFileMeta.SelectedItems[0].SubItems[1].Text;
                if (File.Exists(AbsoluteFilePath))
                {
                    txbMetaFormatted.Lines = lstvRawFileMeta.SelectedItems[0].SubItems[2].Text.Split(';');
                    // Draw Histogram
                    rgbHistGen = new RgbHistogramGenerator(AbsoluteFilePath);
                    histograms = rgbHistGen.GenerateHistogramData();

                    // RGB Historgrams for PictureBoxes
                    pbRedHistogram.Image = histograms[0];
                    pbGreenHistogram.Image = histograms[1];
                    pbBlueHistogram.Image = histograms[2];
                    pbImagePreview.Image = histograms[3];
                }
            }
        }

        /// <summary>
        /// Context menu for more options, makes copy/paste metadata formatted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstvRawFileMeta.SelectedItems.Count > 0)
            {
                string formatted = string.Join("\n", lstvRawFileMeta.SelectedItems[0].SubItems[2].Text.Split(';'));
                Clipboard.SetText(formatted);
            }
        }

        /// <summary>
        /// Contect menu for opening file location and selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFileLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstvRawFileMeta.SelectedItems.Count > 0)
            {
                Process.Start("explorer", $"/select, {AbsoluteFilePath}");
            }
        }

        /// <summary>
        /// Context menu, send to recycle bin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void recycleBinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(AbsoluteFilePath))
            {
                FileSystem.DeleteFile($"{AbsoluteFilePath}", UIOption.AllDialogs, RecycleOption.SendToRecycleBin, UICancelOption.ThrowException);
                lstvRawFileMeta.SelectedItems[0].ForeColor = Color.FromArgb(0, 58, 58, 58);
            }
        }
        /// <summary>
        /// Context Menu, delete permanently
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void permanentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(AbsoluteFilePath))
            {
                FileSystem.DeleteFile($"{AbsoluteFilePath}", UIOption.AllDialogs, RecycleOption.DeletePermanently, UICancelOption.ThrowException);
                lstvRawFileMeta.SelectedItems[0].ForeColor = Color.Red;
            }
        }

        /// <summary>
        /// Plots points for drawing for histogram levels
        /// Points are based on size and location of pictureboxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // Draw Base Lines
            CustomFormPaint customPaint = new CustomFormPaint();
            customPaint.DrawPlotHorizontalKeyLines(pbRedHistogram, e);
            customPaint.DrawPlotHorizontalKeyLines(pbGreenHistogram, e);
            customPaint.DrawPlotHorizontalKeyLines(pbBlueHistogram, e);

            // Draw Percentage Values
            PictureBox[] pictureBoxes = new PictureBox[] {
                pbRedHistogram,
                pbGreenHistogram,
                pbBlueHistogram
            };

            // Numbering the baseline with a key.
            foreach (PictureBox pbox in pictureBoxes)
            {
                customPaint.DrawHistoPercentageKey(pbox, e, this.CreateGraphics());
            }
        }

        /// <summary>
        /// Exit the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
