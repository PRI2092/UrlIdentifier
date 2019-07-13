using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace URLDetecter
{
    public static class SystemConfiguration
    {
        /// <summary>
        /// Get number of CPU available on machine.
        /// </summary>
        /// <returns></returns>
        public static int GetProcesserCount()
        {
            Console.WriteLine("Number of CPU cores Detected : " + Environment.ProcessorCount);
            return Environment.ProcessorCount;

        }
    }
}
