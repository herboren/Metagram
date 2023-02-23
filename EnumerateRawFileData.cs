using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Metagram
{
    class EnumerateRawFileData
    {
        readonly List<string> rawExtensions;
        private List<string> Files { get; set; }
        private string Path { get; set; }

        /// <summary>
        /// All potential common raw files used in digital photography
        /// </summary>
        /// <param name="path"></param>
        public EnumerateRawFileData(string path)
        {
            this.Path = path;

            rawExtensions = new List<string>
            {
                (".cr2"),  // Canon Raw 2 Image
                (".rw2"),  // Panasonic RAW Image
                (".raf"),  // Fujifilm RAW Image
                (".erf"),  // Epson RAW File
                (".nrw"),  // Nikon Raw Image
                (".nef"),  // Nikon Electronic Format RAW Image
                (".arw"),  // Sony Alpha Raw Digital Camera Image
                (".rwz"),  // Rawzor Compressed Image
                (".dng"),  // Digital Negative Image
                (".eip"),  // Enhanced Image Package File
                (".bay"),  // Casio RAW Image
                (".dcr"),  // Kodak Digital Camera RAW Image
                (".gpr"),  // GoPro RAW Image
                (".raw"),  // Raw Image Data
                (".crw"),  // Canon Raw CIFF Image File
                (".3fr"),  // Hasselblad 3F RAW Image
                (".sr2"),  // Sony RAW Image
                (".k25"),  // Kodak K25 Image
                (".kc2"),  // Kodak DCS200 Camera Raw Image
                (".mef"),  // Mamiya RAW Image
                (".dng"),  // Apple ProRAW Image
                (".cs1"),  // CaptureShop 1-shot Raw Image
                (".orf"),  // Olympus RAW File
                (".mos"),  // Leaf Camera RAW Image
                (".ari"),  // ARRIRAW Image
                (".cr3"),  // Canon Raw 3 Image File
                (".kdc"),  // Kodak Photo-Enhancer File
                (".fff"),  // Hasselblad RAW Image
                (".srf"),  // Sony RAW Image
                (".srw"),  // Samsung RAW Image
                (".j6i"),  // Ricoh Camera Image File
                (".mrw"),  // Minolta Raw Image
                (".mfw"),  // Mamiya Camera Raw File
                (".x3f"),  // SIGMA X3F Camera RAW File
                (".rwl"),  // Leica RAW Image
                (".pef"),  // Pentax Electronic File
                (".iiq"),  // Phase One RAW Image
                (".cxi"),  // FMAT RAW Image
                (".nksC"), // Nikon Capture NX-D Sidecar File
                (".mdc"),  // Minolta Camera Raw Image
            };
        }

        /// <summary>
        /// This will grab all the necessary RAW formats present in the current working directory.
        /// </summary>
        /// <returns></returns>
        public List<string> ReturnFileList()
        {
            // Fixed using LINQ
            Files = Directory
                .GetFiles(this.Path, "*", SearchOption.TopDirectoryOnly)
                .Where(ext => rawExtensions.Any(ext.ToLower().EndsWith))
                .ToList();

            return Files;
        }
    }
}
