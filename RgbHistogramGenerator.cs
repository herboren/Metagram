using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using PointCv = OpenCvSharp.Point;
using SizeCv = OpenCvSharp.Size;

namespace Metagram
{
    class RgbHistogramGenerator
    {
        BackgroundWorker bgw;

        // Returns histogram result
        private Mat _ResultB { get; set; } = new Mat();
        private Mat _ResultG { get; set; } = new Mat();
        private Mat _ResultR { get; set; } = new Mat();
        
        // Returns Original color image and RGB
        private Mat _Color { get; set; } = new Mat();
        private Mat _R { get; set; } = new Mat();
        private Mat _G { get; set; } = new Mat();
        private Mat _B { get; set; } = new Mat();
        
        // Image source
        private Mat ImageSource { get; set; }

        // Call on class constructor initialization
        public RgbHistogramGenerator(string path)
        {
            this.ImageSource = Cv2.ImRead(path);
            InitializeCv2Configuration();
            CalculateHistogramData();
        }

        /// <summary>
        /// Configure historgram
        /// </summary>
        public void InitializeCv2Configuration()
        {
            // Create new materials types
            _ResultB = Mat.Ones(new SizeCv(256, ImageSource.Height), MatType.CV_8UC3);
            _ResultG = Mat.Ones(new SizeCv(256, ImageSource.Height), MatType.CV_8UC3);
            _ResultR = Mat.Ones(new SizeCv(256, ImageSource.Height), MatType.CV_8UC3);

            Cv2.CvtColor(ImageSource, _Color, ColorConversionCodes.BGR2BGRA);
        }

        /// <summary>
        /// All histogram data compiled here, generated in result
        /// </summary>
        public void CalculateHistogramData()
        {
            Cv2.CalcHist(new Mat[] { _Color }, new int[] { 0 }, null, _B, 1, new int[] { 256 }, new Rangef[] { new Rangef(0, 256) });
            Cv2.Normalize(_B, _B, 64, 5184, NormTypes.MinMax);

            Cv2.CalcHist(new Mat[] { _Color }, new int[] { 1 }, null, _G, 1, new int[] { 256 }, new Rangef[] { new Rangef(0, 256) });
            Cv2.Normalize(_G, _G, 64, 5184, NormTypes.MinMax);

            Cv2.CalcHist(new Mat[] { _Color }, new int[] { 2 }, null, _R, 1, new int[] { 256 }, new Rangef[] { new Rangef(0, 256) });
            Cv2.Normalize(_R, _R, 64, 5184, NormTypes.MinMax);

            //
            for (int i = 0; i < _B.Rows; i++)
            {
                Cv2.Line(_ResultB, new PointCv(i, ImageSource.Height), new PointCv(i, ImageSource.Height - _B.Get<float>(i)), Scalar.DodgerBlue);
            }
            for (int i = 0; i < _G.Rows; i++)
            {
                Cv2.Line(_ResultG, new PointCv(i, ImageSource.Height), new PointCv(i, ImageSource.Height - _G.Get<float>(i)), Scalar.LimeGreen);
            }
            for (int i = 0; i < _R.Rows; i++)
            {
                Cv2.Line(_ResultR, new PointCv(i, ImageSource.Height), new PointCv(i, ImageSource.Height - _R.Get<float>(i)), Scalar.Coral);
            }
        }

        /// <summary>
        /// Resize and conversion of histogram to fit appropriately in picture boxes.
        /// </summary>
        /// <returns></returns>
        public List<Bitmap> GenerateHistogramData()
        {
            SizeCv histSize = new SizeCv(256, 192);

            List<Mat> rgbHists = new List<Mat>{
                _ResultR,
                _ResultG,
                _ResultB
            };

            List<Bitmap> histMaps = new List<Bitmap>();
            foreach (Mat hist in rgbHists)
            {
                Cv2.Resize(hist, hist, histSize, 0, 0, InterpolationFlags.Linear);
                histMaps.Add(new Bitmap(BitmapConverter.ToBitmap(hist)));
            }
            
            histMaps.Add(new Bitmap(BitmapConverter.ToBitmap(_Color)));
            return histMaps;
        }
    }
}
