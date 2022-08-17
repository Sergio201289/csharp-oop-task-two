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

namespace HomeWork12
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Company company;

        public MainWindow()
        {
            InitializeComponent();
            company = new Company("");
            tree.ItemsSource = company.Departments;

        }
        /// <summary>
        /// Метод, заполняющий таблицу Работников в зависиммости от значения поля Департамент
        /// </summary>
        private void tree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (tree.SelectedItem != null) Workers.ItemsSource = (tree.SelectedItem as Department).Workers;
        }

        /// <summary>
        /// Метод, удаляющий выбранного работника
        /// </summary>
        private void DeleteWorkers_Click(object sender, RoutedEventArgs e)
        {
            if (tree.SelectedItem != null && Workers.SelectedIndex != -1)
            (tree.SelectedItem as Department).DeleteWorker((Workers.SelectedIndex + 1));
            Workers.Items.Refresh();
        }

        /// <summary>
        /// Метод, добавляющий работника в департамент
        /// </summary>
        private void AddWorkers_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                (tree.SelectedItem as Department).AddWorker(new Worker(Name.Text, Surname.Text, Convert.ToInt32(Age.Text), Convert.ToInt32(Salary.Text),
                    Convert.ToInt32(NOP.Text)));
                Workers.Items.Refresh();
            }
            catch { }
        }

        /// <summary>
        /// Метод, добавляющий департамент в коллекцию
        /// </summary>
        private void AddDepartment_Click(object sender, RoutedEventArgs e)
        {
            Company temp;
            Department department = tree.SelectedItem as Department;
            if (department != null) temp = department;
            else temp = company;
            if (DepartmentName.Text != "") temp.AddDepartment(new Department(DepartmentName.Text));
            tree.Items.Refresh();
        }

        /// <summary>
        /// Метод, удаляющий Департамент
        /// </summary>
        private void DeleteDepartment_Click(object sender, RoutedEventArgs e)
        {
            if (tree.SelectedItem != null)
            {
                Department Department = tree.SelectedItem as Department;
                Company ParentDepartment = company.FindParentDepartment(Department.DepartmentID, company);
                ParentDepartment.DeleteDepartment(Department);
                tree.Items.Refresh();
                Workers.ItemsSource = null;
            }   
        }

        /// <summary>
        /// Метод, редактирующий департамент
        /// </summary>
        private void EditDepartment_Click(object sender, RoutedEventArgs e)
        {
            Department Department = tree.SelectedItem as Department;
            if (Department != null)
            {
                Company ParentDepartment = company.FindParentDepartment(Department.DepartmentID, company);
                ParentDepartment.EditDepartment(DepartmentName.Text, Department.DepartmentID);
                tree.Items.Refresh();
            }
        }

        /// <summary>
        /// Метод, редактирующий работника
        /// </summary>
        private void EditWorkers_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                (tree.SelectedItem as Department).EditWorker(Name.Text, Surname.Text, Convert.ToInt32(Age.Text), Convert.ToInt32(Salary.Text),
                    Convert.ToInt32(NOP.Text), Workers.SelectedIndex + 1);
                Workers.Items.Refresh();
            }
            catch { }
        }

        /// <summary>
        /// Метод, обрабатывающий нажатие кнопки открыть компанию
        /// </summary>
        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            company.OpenCompany();
            tree.ItemsSource = company.Departments;
        }

        /// <summary>
        /// Метод, обрабатывающий нажатие кнопки сохранить компанию
        /// </summary>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            company.SaveCompany();
        }
    }
}