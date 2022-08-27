using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SDE_TimeTracking.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для EmployeesTablePage.xaml
    /// </summary>
    public partial class EmployeesTablePage : Page
    {
        ViewModel.EmployeesTablePageVM VM = new ViewModel.EmployeesTablePageVM();
        public EmployeesTablePage()
        {
            InitializeComponent();

            DataContext = VM;
            VM.UpdateTable();
        }

        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            VM.AddEmployee();
        }

        private void SearchTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string textSearch = SearchTxtBox.Text.ToLower();
            EmployeesDG.ItemsSource = VM.Search(textSearch);
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            var item = EmployeesDG.SelectedItem;
            VM.EditEmployee(item);
        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            var item = EmployeesDG.SelectedItem;
            VM.DelEmployee(item);
        }

        private void EmployeesDG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = EmployeesDG.SelectedItem;
            VM.EditEmployee(item);
        }

        private void EmployeesDG_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
    }
}
