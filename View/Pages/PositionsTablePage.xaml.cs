using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SDE_TimeTracking.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для PositionsTablePage.xaml
    /// </summary>
    public partial class PositionsTablePage : Page
    {
        ViewModel.PositionsTablePageVM VM = new ViewModel.PositionsTablePageVM();
        public PositionsTablePage()
        {
            InitializeComponent();

            DataContext = VM;
        }

        private void AddPosition_Click(object sender, RoutedEventArgs e)
        {
            VM.AddPosition();
        }

        private void SearchTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string textSearch = SearchTxtBox.Text.ToLower();
            PositionsDG.ItemsSource = VM.Search(textSearch);
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            var item = PositionsDG.SelectedItem;
            VM.EditPosition(item);
        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            var item = PositionsDG.SelectedItem;
            VM.DelPosition(item);
        }

        private void PositionsDG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = PositionsDG.SelectedItem;
            VM.EditPosition(item);
        }

        private void PositionsDG_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
    }
}
