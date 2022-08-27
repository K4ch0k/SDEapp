using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace SDE_TimeTracking.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditEmployeesWindow.xaml
    /// </summary>
    public partial class EditEmployeesWindow : Window
    {
        ViewModel.EditEmployeesWindowVM VM = new ViewModel.EditEmployeesWindowVM();
        public EditEmployeesWindow(object item)
        {
            InitializeComponent();
            DataContext = VM;
            VM.EditView(item);
        }

        private void GenerateNumBtn_Click(object sender, RoutedEventArgs e)
        {
            PersNumTxtBox.Text = VM.GenerateNumber();
        }

        private void PersNumTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(PersNumTxtBox.Text))
                PersNumTxtBox.Background = Brushes.IndianRed;
            else
                PersNumTxtBox.Background = Brushes.White;
        }

        private void SurnameTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(SurnameTxtBox.Text))
                SurnameTxtBox.Background = Brushes.IndianRed;
            else
                SurnameTxtBox.Background = Brushes.White;
        }

        private void PhoneNumTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            /*
            if (String.IsNullOrWhiteSpace(PhoneNumTxtBox.Text))
                PhoneNumTxtBox.Background = Brushes.IndianRed;
            */
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
                    if (String.IsNullOrWhiteSpace(PersNumTxtBox.Text))
                        NameTxtBox.Background = Brushes.IndianRed;
                    if (String.IsNullOrWhiteSpace(SurnameTxtBox.Text))
                        SurnameTxtBox.Background = Brushes.IndianRed;
                    if (String.IsNullOrWhiteSpace(NameTxtBox.Text))
                        NameTxtBox.Background = Brushes.IndianRed;
                    /*
                    if (String.IsNullOrWhiteSpace(PhoneNumTxtBox.Text))
                        PhoneNumTxtBox.Background = Brushes.IndianRed;
                    */
                    break;
                case 0:
                    break;
                case 1:
                    this.Close();
                    break;
            }
        }

        private void AllPosCB_TextChanged(object sender, TextChangedEventArgs e)
        {
            //AllPosCB.ItemsSource = VM.SearchPos(AllPosCB.Text);
        }
    }
}
