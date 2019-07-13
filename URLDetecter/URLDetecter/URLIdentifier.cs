using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace URLDetecter
{
    class URLIdentifier
    {
        // Store input directory path
        private static string _filePath = string.Empty;

        static URLIdentifier()
        {
            // Get input directory path from cofiguration
            _filePath = System.Configuration.ConfigurationSettings.AppSettings["BaseFolderPath"];
        }
        /// <summary>
        /// Input: FFileName - Actual path of file tobe processed
        /// Output: return collection of identified URLs
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static async Task<List<string>> SearchURL(string fileName)
        {
            List<string> filelist = new List<string>();
            Console.WriteLine("File to Process:" + fileName);
            string filepath = fileName;

            //Regular expression to check valid URL
            Regex regex = new Regex(@"(http|ftp|https):\/\/([\w\-_]+(?:(?:\.[\w\-_]+)+))([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?");

            using (StreamReader reader = new StreamReader(@filepath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    // Try to match each line against the Regex.
                    var match = regex.Matches(line);
                    if (match.Count > 0)
                    {
                        // store identified URLs in collection.
                       foreach(var url in match)
                       {
                           filelist.Add(url.ToString());
                       }
                        
                    }
                }
                reader.Close();

                //reader.DiscardBufferedData();
               // reader.Dispose();
            }
            return filelist;
        }
        /// <summary>
        /// Output: Get collection of all files information available in folder
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAllFiles()
        {
           List<string> files  = new List<string>();
            try
            {
                //check vaild Directory Path
                if(Directory.Exists(_filePath))
                {
                    var fileList = Directory.GetFiles(_filePath);
                    files = fileList.ToList();
                }
                else
                {
                    Console.WriteLine(new FileNotFoundException("Directory does not found" + _filePath));
                }
            }
            catch(FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            
            
            return files;
        }
        /// <summary>
        /// Input: urls :Collection of data to be writen in file
        /// Output : Store in-memory data to physical storage
        /// </summary>
        /// <param name="urls"></param>
        public static void WriteToFile(ref List<string> urls)
        {
            //Get output file path from configuration

            var outputFile = System.Configuration.ConfigurationSettings.AppSettings["OutputFilePath"];
            Console.WriteLine("Output file path:" + outputFile);
            Console.WriteLine("Wait till we write data to file for you.This might take few seconds...");
            StreamWriter sr = new StreamWriter(outputFile);
            foreach(string url in urls)
            {
                sr.WriteLine(url);
            }
            sr.Close();
        }
    }
}
