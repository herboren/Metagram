using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace Metagram
{
    class EnumerateRawMetaData
    {
        // Application working directory
        private List<string> FilePaths { get; set; }

        // Meta Control

        List<string> metaString;
        public List<(string, string, string)> fileMeta;
        public List<(int, string)> metaDefault;

        // Exif Particular

        IEnumerable<MetadataExtractor.Directory> directories;
        ExifSubIfdDirectory dirSubIfd;
        ExifIfd0Directory dirIfd0;

        /// <summary>
        /// Initialize working path, property tags and enumerate raw tag data
        /// </summary>
        /// <param name="paths"></param>
        public EnumerateRawMetaData(List<string> paths)
        {
            this.FilePaths = paths;
            InitializeMetaDataPropertyTags();            
        }

        public void InitializeMetaDataPropertyTags()
        {
            metaDefault = new List<(int, string)>
            {
                (0x010F,"Exif IFD0"),   // Make
                (0x0110,"Exif IFD0"),   // Model
                (0xA434,"Exif SubIFD"), // Lens Model
                (0x920A,"Exif SubIFD"), // Focal Length
                (0x9202,"Exif SubIFD"), // Aperture Value             
                (0x829A,"Exif SubIFD"), // Exposure Time
                (0x829D,"Exif SubIFD"), // FNumber
                (0x8827,"Exif SubIFD"), // ISO Speed Ratings
                // (0x8832,"Exif SubIFD"), // Recommended ExposureIndex                
                (0x0100,"Exif IFD0"),   // Image Width
                (0x0101,"Exif IFD0"),   // Image Height
                (0x0102,"Exif IFD0"),   // Bits Per Sample
                (0x0103,"Exif IFD0"),   // Compression
                (0x9003,"Exif SubIFD"), // Date/Time Original
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public List<(string, string, string)> EnumerateRawFileTagData()
        {
            // These will store tag data we need
            // We do not want all of it
            fileMeta = new List<(string, string, string)>();

            // Get all currently stored tag data on raw files
            dirSubIfd = new ExifSubIfdDirectory();
            dirIfd0 = new ExifIfd0Directory();
            
            // Gather tag data for rwach file enumerated
            foreach (string path in FilePaths)
            {
                // Always new meta, avoid concatentation and
                // mixing meta data from other images.
                metaString = new List<string>();

                // We try in case there are unsupported files.
                // Try frowned on but, ultimately depends on
                // supported filetypes within API
                try
                {
                    // Where the magic begins
                    directories = ImageMetadataReader.ReadMetadata(path);

                    // Access API Directly
                    dirSubIfd = directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();
                    dirIfd0 = directories.OfType<ExifIfd0Directory>().FirstOrDefault();

                    // Create metastring first
                    // Then add meta string to list in respect to working file
                    foreach (var meta in metaDefault)
                    {
                        if (meta.Item2 == dirIfd0?.Tags[0].DirectoryName)
                            metaString.Add($"{dirIfd0?.GetTagName(meta.Item1)}={dirIfd0?.GetString(meta.Item1)}");

                        if (meta.Item2 == dirSubIfd?.Tags[0].DirectoryName)
                            metaString.Add($"{dirSubIfd?.GetTagName(meta.Item1)}={dirSubIfd?.GetString(meta.Item1)}");
                    }                    
                } catch (Exception ex) { }

                // Add results to a list, return list to listview for review
                fileMeta.Add((BytesToString(new FileInfo(path).Length), new DirectoryInfo(path).Name, string.Join(";", metaString.ToArray())));
            }

            // Fin
            return fileMeta;
        }

        /// <summary>
        /// Gather file size, return formatting.
        /// </summary>
        /// <param name="byteCount"></param>
        /// <returns></returns>
        static string BytesToString(long bytes)
        {
            string[] acronym = { "B", "KB", "MB", "GB", "TB", "PB", "EB" };
            if (bytes == 0)
                return "0" + acronym[0];
            long lbytes = Math.Abs(bytes);
            int place = Convert.ToInt32(Math.Floor(Math.Log(lbytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(bytes) * num).ToString() + acronym[place];
        }
    }
}
