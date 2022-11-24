using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//added namespaces
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace OO_programming
{
    public partial class PayCalcForm : Form
    {
        /// <summary>
        /// Initialize Form component.
        /// Holds datagrid and buttons
        /// </summary>
        public PayCalcForm()
        {
            InitializeComponent();           
        }

        private void calculate_Click(object sender, EventArgs e)
        {
            
            // payment summary (textBox2) using the PaySlip and PayCalculatorNoThreshold
            // and PayCalculatorWithThresholds classes object and methods.
            // Import the pay records
            if (dataGridView1.DataSource == null)
            {
                //displays a message box to notify the user
                MessageBox.Show("Empty data, please load file.");
            }
            else 
            {
                List<PaySlip> records = CsvImporter.ImportPayRecords("employee-payroll-data-REAL");
                //display list of records to datagrid 2
                dataGridView2.DataSource = records;
            }
        }

        private void save_Click(object sender, EventArgs e)
        {

            // calculated payment data into a csv file.
            // File naming convention: $"{DateTime.Now.Ticks}-records"
            // Data fields expected - EmployeeId, Full Name, Hours Worked, Hourly Rate, Tax Threshold, Gross Pay, Tax, Net Pay
            // We'll generate a pseudo ish unique file name using the .Ticks property of the DateTime class.
            if (dataGridView1.DataSource != null && dataGridView2.DataSource != null)
            {
                List<PaySlip> records = CsvImporter.ImportPayRecords("employee-payroll-data-REAL");
                string exportedFileName = $"{DateTime.Now.Ticks}-records";

                //Call the PayRrecordWriter class's .Write() method to write the records.
                PaySlipWriter.Write(exportedFileName, records);

                //displays a message box to notify the user
                MessageBox.Show("File saved successfuly");
            }
            else 
            {
                //displays a message box to notify the user
                MessageBox.Show("Please calculate first before saving.");
            }
                      
        }

        private void load_Click(object sender, EventArgs e)
        {
            // Implementation to populate the DataGrid
            // by reading the employee.csv file into a List of PaySlip objects, then binding this to the DataGrid.
            // CSV file format: <employee ID>, <first name>, <last name>, <hourly rate>,<taxthreshold>
            dataGridView1.DataSource = NewDataTable($@"..\..\Import\employee-payroll-data-REAL.csv", ",");
        }

        /// <summary>
        /// database table representation and provides a collection of columns and rows to store data in a grid form
        /// </summary>
        /// <param name="fileName">string: Contains the path of the file</param>
        /// <param name="delimiters">string: delimiters to be used in every column</param>
        /// <param name="firstRowContainsFieldNames">bool: set to display field names</param>
        /// <returns>DataTable: holds all the data from .csv file</returns>
        public static DataTable NewDataTable(string fileName, string delimiters, bool firstRowContainsFieldNames = true)
        {
            DataTable result = new DataTable();

            using (TextFieldParser tfp = new TextFieldParser(fileName))
            {
                tfp.SetDelimiters(delimiters);

                // Get Some Column Names
                if (!tfp.EndOfData)
                {
                    //array of string for every fields
                    string[] fields = tfp.ReadFields();

                    //iteration of fields array 
                    for (int i = 0; i < fields.Count(); i++)
                    {
                        if (firstRowContainsFieldNames)
                            result.Columns.Add(fields[i]);
                        else
                            result.Columns.Add("Col" + i);
                    }

                    // If first line is data then add it
                    if (!firstRowContainsFieldNames)
                        result.Rows.Add(fields);
                }

                // Get Remaining Rows
                while (!tfp.EndOfData)
                    result.Rows.Add(tfp.ReadFields());
            }            
            return result;
        }
    }           
    
}
