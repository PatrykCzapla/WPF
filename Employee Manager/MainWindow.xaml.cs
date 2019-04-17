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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Employee> employees;
        private int index;
        private string currentFile;
        private string firstLine;
        public bool isUpdated;
        private AddEmployee addEmp;

        public MainWindow()
        {
            InitializeComponent();
            index = -1;
            employees = new ObservableCollection<Employee>();
            isUpdated = true;
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            if (isUpdated == false)
            {
                MessageBoxResult answer = MessageBox.Show("Do you want to save changes before opening new file?", "Save changes", MessageBoxButton.YesNoCancel);
                if (answer == MessageBoxResult.Cancel) return;
                else if (answer == MessageBoxResult.Yes)
                {
                    Save(false);
                    return;
                }                    
            }
            employees = new ObservableCollection<Employee>();
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "CSV files (*.csv)|*.csv";
            if (fileDialog.ShowDialog() == true)
            {
                var tx = File.ReadAllLines(fileDialog.FileName);
                firstLine = tx[0];
                for (int i = 1; i < tx.Length; i++)
                {
                    string[] val = tx[i].Split(';');
                    string[] date = val[3].Split('.');
                    DateTime dt = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]));
                    Employee emp = new Employee(val[0], val[1], val[2], dt, val[4], int.Parse(val[5]), (Currency)int.Parse(val[6]), (Role)int.Parse(val[7]));
                    employees.Add(emp);
                }
                currentFile = fileDialog.FileName;
            }
            isUpdated = true;
            this.DataContext = employees;
            ICollectionView view = CollectionViewSource.GetDefaultView(employees);
            view.Refresh();
        }

        private void Button_MoveUp(object sender, RoutedEventArgs e)
        {
            if (this.employees == null) return;
            index = Listbox.SelectedIndex;
            if (index <= 0) return;
            Employee em = employees[index - 1];
            employees[index - 1] = employees[index];
            employees[index] = em;
            index--;
            Listbox.SelectedIndex = index;
            isUpdated = false;
        }

        private void Button_MoveDown(object sender, RoutedEventArgs e)
        {
            if (this.employees == null) return;
            index = Listbox.SelectedIndex;
            if (index >= employees.Count - 1) return;
            Employee em = employees[index + 1];
            employees[index + 1] = employees[index];
            employees[index] = em;
            index++;
            Listbox.SelectedIndex = index;
            isUpdated = false;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Save(false);
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            Save(true);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            if (isUpdated == false)
            {
                var answer = MessageBox.Show("Do you want to save changes before closing?", "Save changes", MessageBoxButton.YesNoCancel);
                if (answer == MessageBoxResult.Cancel) return;
                else if (answer == MessageBoxResult.Yes)
                    Save(false);
            }
            if (addEmp != null) addEmp.Close();
            Close();
        }

        private void Save(bool Ask)
        {
            if (currentFile == null) return;
            if (employees == null) return;
            if (Ask == false && isUpdated == true) return;
            if (Ask == true)
            {
                SaveFileDialog fileDialog = new SaveFileDialog();
                fileDialog.Filter = "CSV files (*.csv)|*.csv";
                if (fileDialog.ShowDialog() == true)
                    currentFile = fileDialog.FileName;
            }
            List<string> result = new List<string>();
            result.Add(firstLine);
            foreach (Employee emp in employees)
                result.Add(emp.Concatenate());
            File.WriteAllLines(currentFile, result.ToArray());
            isUpdated = true;
        }

        private void ComboBox_IsMouseCaptureWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            isUpdated = false;
        }

        private void salary_TextChanged(object sender, TextChangedEventArgs e)
        {
            isUpdated = false;
        }

        private void AddNewEmployee(object sender, RoutedEventArgs e)
        {
            if (employees.Count == 0) return;
            if (addEmp != null)
            {
                addEmp.WindowState = WindowState.Normal;
                return;
            }
            addEmp = new AddEmployee(this);
            addEmp.Closed += delegate { addEmp = null; };
            addEmp.Show();
        }

        private void DeleteEmployee(object sender, RoutedEventArgs e)
        {
            index = Listbox.SelectedIndex;
            if (employees != null && index >= 0 && index <= employees.Count - 1)
                employees.RemoveAt(index);
        }

    }
}
