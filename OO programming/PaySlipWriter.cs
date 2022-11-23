using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//added namespaces
using System.IO;
using CsvHelper;
using System.Globalization;

namespace OO_programming
{
    public class PaySlipWriter
    {
        /// <summary>
        /// Static method for writing a collection of Pay slip objects to a comma delimited file        
        /// </summary>
        /// <param name="file">string: the name of the file.</param>
        /// <param name="records">T: List: The collection of PaySlip objects.</param>        
        public static void Write(string file, List<PaySlip> records)
        {

            /// <summary>
            /// The using statement defines a scope at the end of which an object will be disposed.
            /// '$' allows string interpolation 
            /// '@' delimits any '\' backslashes
            /// </summary>
            using (StreamWriter stream = new StreamWriter(Path.GetFullPath($@"..\..\Export\{file}.csv")))
            using (CsvWriter csv = new CsvWriter(stream, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(records);
            }

        }
    }
}
