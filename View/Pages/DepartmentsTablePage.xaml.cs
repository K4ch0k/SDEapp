using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SDE_TimeTracking.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для DepartmentsTablePage.xaml
    /// </summary>
    public partial class DepartmentsTablePage : Page
    {
        ViewModel.DepartmentsTablePageVM VM = new ViewModel.DepartmentsTablePageVM();
        public DepartmentsTablePage()
        {
            InitializeComponent();
            DataContext = VM;
        }

        private void AddDepartments_Click(object sender, RoutedEventArgs e)
        {
            VM.AddDepartment();
        }

        private void SearchTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string textSearch = SearchTxtBox.Text.ToLower();
            DepartmentsDG.ItemsSource = VM.Search(textSearch);
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            var item = DepartmentsDG.SelectedItem;
            VM.EditDepartment(item);
        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            var item = DepartmentsDG.SelectedItem;
            VM.DelDepartment(item);
        }

        private void DepartmentsDG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = DepartmentsDG.SelectedItem;
            VM.EditDepartment(item);
        }

        private void DepartmentsDG_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
    }
}
