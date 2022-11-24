using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OO_programming
{
    /// <summary>
    /// Class a capture details accociated with an employee's pay slip record
    /// </summary>
    public abstract class PaySlip
    {
        protected double[] _hours, _rates;
        /// <summary>
        /// Pay slip constructor
        /// </summary>
        /// <param name="id">int: The employee identifier</param>
        /// <param name="hours">double[]: Array of the employee's shift hours worked.</param>
        /// <param name="rates">double[]: Array of the employee's pay rates for the shifts worked.</param>
        /// <param name="firstName">string: First name of the employee</param>
        /// <param name="lastName">string: Last name of the employee</param>
        /// <param name="hourlyRate">double: Hourly rate of employee</param>
        public PaySlip(int id, 
                       string firstName, 
                       string lastName, 
                       double[] hours, 
                       double[] rates, 
                       double hourlyRate)
        {
            this.Id = id;
            this._hours = hours;
            this._rates = rates;
            this.FirstName = firstName;
            this.LastName = lastName;            
            this.HourlyRate = Math.Round(hourlyRate,2);

        }

        public int Id { get; private set; }        
        public string FirstName { get; private set; }
        public string LastName { get; private set; }        
        public double HourlyRate { get; private set; }

        /// <summary>
        /// Read only derived property that return the total worked hours
        /// </summary>
        /// <returns>double: Floored value of the total worked hours.</returns>
        public double TotalHours
        {
            get
            {
                double totalHours = 0;
                for (int i = 0; i < _hours.Length; i++)
                {
                    totalHours += _hours[i];
                }
                return Math.Floor(totalHours);
            }
        }
        /// <summary>
        /// Read only derived property that returns the net payment which is 'Gross - Tax'
        /// </summary>
        public double Net
        {
            get
            {
                return Math.Round((this.Gross - this.Tax),2);
            }
        }

        /// <summary>
        /// Abstract property that must be overriden by any class that inherits from Pay Slip (derived class).
        /// </summary>
        public abstract double Tax { get; }

        /// <summary>
        /// Read only property that derives the Employee Gross pay by summing all the rates of every employee        
        /// </summary>
        /// <returns>double: Floored value of the Employee Gross.</returns>
        public double Gross
        {
            get
            {                                                
                return Math.Round(_rates.Sum(),2);
            }

        }

    }
}
