using System.Windows;

namespace SDE_TimeTracking.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для CreateReportWindow.xaml
    /// </summary>
    public partial class CreateReportWindow : Window
    {
        ViewModel.CreateReportWindowVM VM = new ViewModel.CreateReportWindowVM();
        public CreateReportWindow()
        {
            InitializeComponent();
            DataContext = VM;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            int res = VM.Save();
            switch (res)
            {
                case -1:
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
