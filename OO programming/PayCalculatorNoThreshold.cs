using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OO_programming
{


    /// <summary>
    /// Extends PayCalculator class handling No tax threshold
    /// </summary>
    public class PayCalculatorNoThreshold : PaySlip
    {
        /// <summary>
        /// Employee that did NOT claim tax threshold
        /// </summary>
        /// <param name="id">int: ID of the employee</param>
        /// <param name="firstName">string: First name of the employee</param>
        /// <param name="lastName">string: Last name of the employee</param>
        /// <param name="hours">double[]: Worked hours of every employee</param>
        /// <param name="rates">rates[]: Daily rates of every employee</param>
        /// <param name="hourlyRate"></param>
        public PayCalculatorNoThreshold(int id, 
                                        string firstName, 
                                        string lastName,
                                        double[] hours, 
                                        double[] rates, 
                                        double hourlyRate) 
        : base(id, firstName, lastName, hours, rates, hourlyRate)
        {
            //Constructor, passes args up to the base class (parent class).
        }

        /// <summary>
        /// Property that overrides the Tax property in the base class.
        /// Calls the static method CalculateWithNoThreshold of TaxCalculator
        /// to determine the tax.
        /// </summary>
        /// <return>double: Returns rounded value of Tax with 2 decimal places</return>
        public override double Tax
        {
            get
            {
                return Math.Round((TaxCalculator.CalculateNoThreshold(Gross)), 2);
            }
        }
    }
}
