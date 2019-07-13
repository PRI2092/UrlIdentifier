


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace URLDetecter
    
{
    /// <summary>
    /// Application start Point. 
    /// This appilcation will scan the text from file and store the result in file.
    /// Output file will contain the identified URLs from file
    /// </summary>
    class Program
    {
        //Store the output in list before writing data into file.
        // Future Change: Need smart list to optimize memory utilization during execution.
        public static List<string> _fileList;
        static void Main(string[] args)
        {
            Console.WriteLine("Application execution start Time: " + System.DateTime.Now);
            Console.WriteLine("File scan started");
             _fileList = new List<string>(); 
            //count the number of processers availale on machine 
            var processerCount = SystemConfiguration.GetProcesserCount();
           // Get all files information available in folder
            var files = URLIdentifier.GetAllFiles();
            if(files.Count > 0)
            {
                
                //process multiple files parallel with maximum parallalization degree capted to number of processers available on machine
               Parallel.ForEach(
                   files,
                   new ParallelOptions(){MaxDegreeOfParallelism = processerCount},
                   async (currentFile) => _fileList.AddRange(await URLIdentifier.SearchURL(currentFile)) );
            }
            Console.WriteLine("File scan completed");
            Console.WriteLine("File scan end Time:"  + System.DateTime.Now);
            Console.WriteLine("Number of URLs found:" + _fileList.Count);
            Console.WriteLine("Started writing data to Phycial storage");
            URLIdentifier.WriteToFile(ref _fileList);
            Console.WriteLine("Write operation completed. Please press any key to close this window");
            Console.ReadKey();
                

        }
    }
}
