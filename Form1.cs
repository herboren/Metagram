using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Metagram
{
    public partial class Form1 : Form
    {
        EnumerateRawFileData enumRawFiles;
        EnumerateRawMetaData enumMetaData;
        public List<string> RawFiles { get; set; }
        public List<(string, string)> RawMeta { get; set; }
        private string ApplicationPath { get; set; }

        /// <summary>
        /// On initialization the rawfiles are filted based on raw type and returns as a list
        /// The list is then passed have raw meta tag data gathered and returned as a string of metadata.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            
            // Get Startup path of working directory
            this.ApplicationPath = (Application.StartupPath);

            // Do magic here on launch
            EnumerateFilteredRawFileData();
            EnumerateFilteredRawMeta();
        }

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
    }
}
