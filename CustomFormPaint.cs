using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Metagram
{
    class CustomFormPaint
    {
        private ToolStripSeparator tss { get; set; }        
        private PaintEventArgs e { get; set; }
        private object sender { get; set; }

        /// <summary>
        /// Assist in painting the form separators, or retrieving controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public CustomFormPaint(object sender, PaintEventArgs e)
        {
            this.e = e;
            this.sender = sender;
            this.tss = (ToolStripSeparator)this.sender;
        }

        // Default constructor
        public CustomFormPaint()
        {

        }

        /// <summary>
        /// This will help to repaint the separators used in Menus/Context Menus
        /// </summary>
        public void RePaintSeparator()
        {
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(26, 29, 45)), 0, 0, tss.Width, tss.Height);
            e.Graphics.DrawLine(new Pen(Color.Coral), 10, tss.Height / 2, tss.Width - 2, tss.Height / 2);
        }

        /// <summary>
        /// This is where we draw the histograms perentage key
        /// </summary>
        public void DrawHistoPercentageKey(PictureBox pictureBox, PaintEventArgs e, Graphics text)
        {
            Font font = new Font("Arial", 8, FontStyle.Regular);
            Brush brush = new SolidBrush(Color.White);
            StringFormat format = new StringFormat();
            PointF textPosition;

            // Not finished
            int yOffset = 4;
            float percentage = 0.25f;            
            string textPercentage = $"{percentage * 100}";
            Dictionary<int, float> keys = new Dictionary<int, float>()
            {
                { 64,0.25f },
                { 128,0.5f },
                { 192,0.75f }
            };

            foreach (KeyValuePair<int,float> kvp in keys)
            {
                textPosition = new PointF((pictureBox.Location.X + kvp.Key) - 8, pictureBox.Location.Y + pictureBox.Size.Height + yOffset + 2);
                text.DrawString($"{kvp.Value * 100}", font, brush, textPosition, format);
            }

            // Dispose all
            text.Dispose();
            font.Dispose();
            brush.Dispose();
            format.Dispose();
        }

        public void DrawPlotHorizontalKeyLines(PictureBox pb, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.White, 1);
            
            // This will draw the baseline for the historgram tally marks
            Point[] pointToPoint =
            {
                new Point(pb.Location.X, pb.Location.Y + pb.Size.Height),
                new Point(pb.Location.X + pb.Size.Width -1, pb.Location.Y + pb.Size.Height)
            };
            
            int yOffset = 4;            
            
            // This will draw the tally marks for the percentage values
            float distance = pb.Size.Width;

            for (int pixel = 0; pixel < distance; pixel++)
            {
                if (pixel % 64 == 0)
                {
                    Point[] tallyPoints =
                    { 
                        new Point(pb.Location.X + pixel, pb.Location.Y), 
                        new Point(pb.Location.X + pixel, pb.Location.Y + pb.Size.Height+ yOffset)
                    };

                    e.Graphics.DrawLines(pen, tallyPoints);                   
                }
            }

            // Draw Line
            e.Graphics.DrawLines(pen, pointToPoint);            
        }

        // Current Rule
        // (256 / 100) * 0.25f = 64 pixels
    }
}
