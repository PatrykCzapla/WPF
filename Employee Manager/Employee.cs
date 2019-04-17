using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Lab4
{
    /// <summary>
    /// All classes and methods needed to operate with Employee
    /// </summary>
    public enum Currency { PLN, USD, EUR, GBP, NOK }
    public enum Role { Worker, SeniorWorker, IT, Manager, Director, CEO }
    public class Employee
    {
        public string FullName { get { return FirstName + " " + LastName; } }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthCountry { get; set; }
        public int Salary { get; set; }
        public Currency SalaryCurrency { get; set; }
        public Role CompanyRole { get; set; }

        public Employee(string firstName, string lastName, string sex, DateTime birthDate, string birthCountry, int salary, Currency salaryCurrency, Role companyRole)
        {
            FirstName = firstName;
            LastName = lastName;
            Sex = sex;
            BirthDate = birthDate;
            BirthCountry = birthCountry;
            Salary = salary;
            SalaryCurrency = salaryCurrency;
            CompanyRole = companyRole;
        }

        public Employee()
        {
            Salary = 5000;
            CompanyRole = Role.Worker;
            SalaryCurrency = Currency.PLN;
            BirthDate = DateTime.Now.AddYears(-30);
        }

        public string Concatenate()
        {
            string BDay = BirthDate.Day.ToString() + "." + BirthDate.Month.ToString() + "." + BirthDate.Year.ToString();
            return (FirstName + ";" + LastName + ";" + Sex + ";" + BDay + ";" + BirthCountry + ";" + Salary + ";" + (int)SalaryCurrency + ";" + (int)CompanyRole);
        }

    }
    /// <summary>
    /// Selects the DataTemplate for CEOs and other employees
    /// </summary>
    public class IsCEOSelector:DataTemplateSelector
    {
        public DataTemplate IsCEOTemplate { get; set; }
        public DataTemplate IsNotCEOTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            Employee emp = item as Employee;
            if (emp.CompanyRole == Role.CEO)
                return IsCEOTemplate;
            else return IsNotCEOTemplate;
        }

    }

}
