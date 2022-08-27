using System.Windows;

namespace SDE_TimeTracking.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel.MainWindowVM VM = new ViewModel.MainWindowVM();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = VM;
        }

        public void EditVsisible()
        {
            CreateEnterprises.Visibility = Visibility.Visible;
            CreateDepartments.Visibility = Visibility.Visible;
            CreatePositions.Visibility = Visibility.Visible;
            CreateEmployees.Visibility = Visibility.Visible;
        }

        private void SeeEnterprises_Click(object sender, RoutedEventArgs e)
        {
            EditVsisible();
            MainFrame.Navigate(new Pages.EnterprisesTablePage());
            InfoSP.Visibility = Visibility.Hidden;
            CreateEnterprises.Visibility = Visibility.Collapsed;
        }

        private void CreateEnterprises_Click(object sender, RoutedEventArgs e)
        {
            VM.AddCompany();
        }

        private void SeeDepartments_Click(object sender, RoutedEventArgs e)
        {
            EditVsisible();
            MainFrame.Navigate(new Pages.DepartmentsTablePage());
            InfoSP.Visibility = Visibility.Hidden;
            CreateDepartments.Visibility = Visibility.Collapsed;
        }

        private void CreateDepartments_Click(object sender, RoutedEventArgs e)
        {
            VM.AddDepartment();
        }

        private void SeePositions_Click(object sender, RoutedEventArgs e)
        {
            EditVsisible();
            MainFrame.Navigate(new Pages.PositionsTablePage());
            InfoSP.Visibility = Visibility.Hidden;
            CreatePositions.Visibility = Visibility.Collapsed;
        }

        private void CreatePositions_Click(object sender, RoutedEventArgs e)
        {
            VM.AddPositions();
        }

        private void SeeEmployees_Click(object sender, RoutedEventArgs e)
        {
            EditVsisible();
            MainFrame.Navigate(new Pages.EmployeesTablePage());
            InfoSP.Visibility = Visibility.Hidden;
            CreateEmployees.Visibility = Visibility.Collapsed;
        }

        private void CreateEmployees_Click(object sender, RoutedEventArgs e)
        {
            VM.AddEmployees();
        }

        private void CreateTimeTracking_Click(object sender, RoutedEventArgs e)
        {
            VM.AddTimeTracking();
        }

    }
}
