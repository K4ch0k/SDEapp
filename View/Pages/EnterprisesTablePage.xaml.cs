using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SDE_TimeTracking.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для EnterprisesTablePage.xaml
    /// </summary>
    public partial class EnterprisesTablePage : Page
    {
        ViewModel.EnterprisesTablePageVM VM = new ViewModel.EnterprisesTablePageVM();
        public EnterprisesTablePage()
        {
            InitializeComponent();
            DataContext = VM;
        }

        private void AddCompany_Click(object sender, RoutedEventArgs e)
        {
            VM.AddCompany();
        }

        private void SearchTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string textSearch = SearchTxtBox.Text.ToLower();
            EnterprisesDG.ItemsSource = VM.Search(textSearch);
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            var item = EnterprisesDG.SelectedItem;
            VM.EditCompany(item);
        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            var item = EnterprisesDG.SelectedItem;
            VM.DelEnterprise(item);
        }

        private void EnterprisesDG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = EnterprisesDG.SelectedItem;
            VM.EditCompany(item);
        }

        private void EnterprisesDG_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex()+1).ToString();
        }
    }
}
