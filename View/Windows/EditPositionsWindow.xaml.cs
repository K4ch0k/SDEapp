using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SDE_TimeTracking.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditPositionsWindow.xaml
    /// </summary>
    public partial class EditPositionsWindow : Window
    {
        ViewModel.EditPositionsWindowVM VM = new ViewModel.EditPositionsWindowVM();
        public EditPositionsWindow(object item)
        {
            InitializeComponent();

            DataContext = VM;
            VM.EditView(item);
        }

        private void NameTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(NameTxtBox.Text))
                NameTxtBox.Background = Brushes.IndianRed;
            else
                NameTxtBox.Background = Brushes.White;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            int res = VM.Save();
            switch (res)
            {
                case -1:
                    if (String.IsNullOrWhiteSpace(NameTxtBox.Text))
                        NameTxtBox.Background = Brushes.IndianRed;
                    break;
                case 0:
                    break;
                case 1:
                    this.Close();
                    break;
            }
        }
    }
}
