using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OO_programming
{
    public class TaxCalculator
    {
        /// <summary>
        /// Static method for Tax-free threshold NOT claimed by employee        
        /// </summary>
        /// <param name="gross"></param>
        /// <returns>double: The amount of Tax to withhold.</returns>
        public static double CalculateNoThreshold(double gross)
        {

            ///<summary>
            ///These schedules can be used to calculate weekly tax owing with the following linear
            ///equation of the form y = ax − b, where:
            ///• y is the weekly tax amount expressed in dollars
            ///• x is the number of whole dollars of the weekly earnings plus 99 cents
            ///•a and b are the values of the coefficients from the tax schedules above
            ///</summary>
            switch (gross)
            {
                case double n when (n < 88):
                    return (0.19 * gross - 0.19);
                case double n when (n < 371):
                    return (0.2348 * gross - 3.9639);
                case double n when (n < 515):
                    return (0.2190 * gross - -1.9003);
                case double n when (n < 932):
                    return (0.3477 * gross - 64.4297);
                case double n when (n < 1957):
                    return (0.3450 * gross - 61.9132);
                case double n when (n < 3111):
                    return (0.3900 * gross - 150.0093);
                default:
                    return 0.4700 * gross - 398.9324;
            }
        }

        /// <summary>
        /// Static method for Tax-free threshold CLAIMED by employee          
        /// </summary>
        /// <param name="gross">double: The gross amount for the employee pay record</param>        
        /// <returns>double: The amount of Tax to withhold</returns>
        public static double CalculateWithThreshold(double gross)
        {
            switch (gross)
            {
                case double n when (n < 359):
                    return gross;
                case double n when (n < 438):
                    return (0.1900 * gross - 68.3462);
                case double n when (n < 548):
                    return (0.2900 * gross - 112.1942);
                case double n when (n < 721):
                    return (0.2100 * gross - 68.3465);
                case double n when (n < 865):
                    return (0.2190 * gross - 74.8369);
                case double n when (n < 1282):
                    return (0.3477 * gross - 186.2119);
                case double n when (n < 2307):
                    return (0.3450 * gross - 182.7504);
                case double n when (n < 3461):
                    return (0.3900 * gross - 286.5965);
                default:
                    return 0.4700 * gross - 563.5196;
            }
        }
    }
}
