using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.Globalization;
using System.ComponentModel;

namespace Lab4
{
    /// <summary>
    /// Interaction logic for AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Window
    {
        private Employee newEmployee;
        public MainWindow mw;
        public bool IsMale { get; set; } = true;

        public AddEmployee(MainWindow mw)
        {
            InitializeComponent();
            newEmployee = new Employee();
            this.mw = mw;
            this.DataContext = newEmployee;            
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            newEmployee.Sex = (IsMale ? "Male" : "Female");
            mw.employees.Add(newEmployee);
            mw.isUpdated = false;
            ICollectionView view = CollectionViewSource.GetDefaultView(mw.employees);
            view.Refresh();
            newEmployee = new Employee();
            this.DataContext = newEmployee;
        }

    }
}
