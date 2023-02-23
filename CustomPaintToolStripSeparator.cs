using System.Drawing;
using System.Windows.Forms;

namespace Metagram
{
    class CustomPaintToolStripSeparator
    {
        private ToolStripSeparator tss { get; set; }
        private PaintEventArgs e { get; set; }
        private object sender { get; set; }

        /// <summary>
        /// Assist in painting the form separators
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public CustomPaintToolStripSeparator(object sender, PaintEventArgs e)
        {
            this.e = e;
            this.sender = sender;
            this.tss = (ToolStripSeparator)this.sender;
        }

        /// <summary>
        /// This will help to repaint the separators used in Menus/Context Menus
        /// </summary>
        public void RePaintSeparator()
        {
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(26, 29, 45)), 0, 0, tss.Width, tss.Height);
            e.Graphics.DrawLine(new Pen(Color.Coral), 10, tss.Height / 2, tss.Width - 2, tss.Height / 2);
        }
    }
}
