using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Lab4
{
    /// <summary>
    /// Salary validation
    /// </summary>
    public class SalaryRule : ValidationRule
    {
        public int MinSalary { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int salary;
            try
            {
                if (((string)value).Length >= 0)
                    salary = int.Parse((string)value);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, "Please enter an integer!");
            }
            if (int.Parse((string)value) < MinSalary)
                return new ValidationResult(false, "Min. salary is " + MinSalary + "!");                             
            return ValidationResult.ValidResult;
        }

    }

}
