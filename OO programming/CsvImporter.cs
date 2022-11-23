using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO; // added

namespace OO_programming
{
    /// <summary>
    /// A.NET library for reading and writing CSV files.
    /// Installed using Package Manager Console
    /// -- PM> Install-Package CsvHelper
    /// Or through .NET CLI Console
    /// > dotnet add package CsvHelper
    /// </summary>
    public class CsvImporter
    {
        /// <summary>
        /// Imports the data from a csv file and reads every line in the .csv file and adds to Pay Slip objects        
        /// </summary>
        public static List<PaySlip> ImportPayRecords(string fileName)
        {
            //Creates a new instance type for storing objets
            List<PaySlip> records = new List<PaySlip>();

            /// <summary>
            /// Implements a TextReader that reads characters from a byte stream in a particular encoding.
            /// </summary>
            using (StreamReader stream = new StreamReader(Path.GetFullPath($@"..\..\Import\{fileName}.csv")))
            {
                //Reads a line of characters from the current stream and returns the data as a string
                stream.ReadLine();
                
                int id = -1;
                string taxFreeThreshold = String.Empty;
                string yearToDate = string.Empty;

                //use to add all the hours and rates of every employee
                List<double> hours = new List<double>();
                List<double> rates = new List<double>();

                string firstName = string.Empty;
                string lastName = string.Empty;
                double hourlyRate = 0;

                //iterate every line from .csv file
                for (string line = stream.ReadLine(); line != null; line = stream.ReadLine())
                {
                    //using split to separate every column in .csv file
                    string[] columns = line.Split(',');
                    
                    if (int.Parse(columns[0]) != id && id != -1)
                    {
                        //adds new PaySlipRecord object
                        records.Add(CreatePaySlipRecord(id, firstName, lastName, hours.ToArray(), rates.ToArray(), taxFreeThreshold, hourlyRate));

                        //clearing hours and rates to be used for the next employee
                        hours.Clear();
                        rates.Clear();
                    }
                    
                    //assigning every column from the .csv file
                    id = int.Parse(columns[0]);
                    firstName = columns[1];
                    lastName = columns[2];
                    hours.Add(double.Parse(columns[3]));
                    rates.Add(double.Parse(columns[4]));
                    taxFreeThreshold = columns[5]; 
                    hourlyRate = double.Parse(columns[4]) / double.Parse(columns[3]);
                }
                
                //check if all the record has been read.
                if (id != -1)
                {
                    //adds the last Employee from the .csv file
                    records.Add(CreatePaySlipRecord(id, firstName, lastName, hours.ToArray(), rates.ToArray(), taxFreeThreshold, hourlyRate));
                }

            }
            
            //returns all the list of PaySlip objects
            return records;

        }

        /// <summary>
        /// Static method of ImportPayRecords()
        /// Responsible for determing whether the employee claimed the tax threshold or not
        /// </summary>
        /// <param name="id">int: The employee identifier</param>
        /// <param name="hours">double[]: Array of the employee's shift hours worked.</param>
        /// <param name="rates">double[]: Array of the employee's pay rates for the shifts worked.</param>
        /// <param name="firstName">string: First name of the employee</param>
        /// <param name="lastName">string: Last name of the employee</param>
        /// <param name="taxFreeThreshold">string: To determine if the employee claimed the tax threshold or not</param>
        /// <param name="hourlyRate">double: Hourly rate of employee</param>rking Holiday Employee. An empty string is passed for Resident Employees.</param>        
        public static PaySlip CreatePaySlipRecord(int id, 
                                              string firstName,
                                              string lastName, 
                                              double[] hours, 
                                              double[] rates, 
                                              string taxFreeThreshold, 
                                              double hourlyRate)
        {
            //determine whether the employee claimed a tax threshold or not
            if (taxFreeThreshold == "N")
            {
                return new PayCalculatorNoThreshold(id, firstName, lastName, hours, rates, hourlyRate);
            }
            else
            {
                return new PayCalculatorWithThreshold(id, firstName, lastName, hours, rates, hourlyRate);
            }
            
            
        }
    }
}
