using SDE_TimeTracking.Model;
using System;
using System.Linq;
using System.Windows;

namespace SDE_TimeTracking.ViewModel
{
    public class MainWindowVM : PropertyChangedBase
    {
        private int _qAllEnterprises;
        private int _qAllEmployees;
        private int _qAllDepartments;
        private int _qAllPositions;
        private int _qAllWorkingTimes;

        public int qAllEnterprises 
        {
            get { return _qAllEnterprises; }
            set 
            { 
                _qAllEnterprises = value;
                OnPropertyChanged(nameof(qAllEnterprises));
            }
        }
        public int qAllEmployees
        {
            get { return _qAllEmployees; }
            set
            {
                _qAllEmployees = value;
                OnPropertyChanged(nameof(qAllEmployees));
            }
        }
        public int qAllDepartments
        {
            get { return _qAllDepartments; }
            set
            {
                _qAllDepartments = value;
                OnPropertyChanged(nameof(_qAllDepartments));
            }
        }
        public int qAllPositions
        {
            get { return _qAllPositions; }
            set
            {
                _qAllPositions = value;
                OnPropertyChanged(nameof(qAllPositions));
            }
        }
        public int qAllWorkingTimes
        {
            get { return _qAllWorkingTimes; }
            set
            {
                _qAllWorkingTimes = value;
                OnPropertyChanged(nameof(qAllWorkingTimes));
            }
        }

        public MainWindowVM()
        {
            UpdateView();
        }

        public void UpdateView()
        {
            try
            {
                using (var Context = new SDEEntities())
                {
                    qAllEnterprises = Context.Enterprises.ToList().Count;
                    qAllDepartments = Context.Departments.ToList().Count;
                    qAllEmployees = Context.Employees.ToList().Count;
                    qAllPositions = Context.Positions.ToList().Count;
                    qAllWorkingTimes = Context.WorkingTime.ToList().Count;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сообщение ошибки: \n" + ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void AddCompany()
        {
            View.Windows.EditEnterprisesWindow EditWin = new View.Windows.EditEnterprisesWindow(new Enterprises());
            EditWin.ShowDialog();
            UpdateView();
        }

        public void AddDepartment()
        {
            View.Windows.EditDepartmentsWindow EditWin = new View.Windows.EditDepartmentsWindow(new Departments());
            EditWin.ShowDialog();
            UpdateView();
        }

        public void AddPositions()
        {
            View.Windows.EditPositionsWindow EditWin = new View.Windows.EditPositionsWindow(new Positions());
            EditWin.ShowDialog();
            UpdateView();
        }

        public void AddEmployees()
        {
            View.Windows.EditEmployeesWindow EditWin = new View.Windows.EditEmployeesWindow(new Employees());
            EditWin.ShowDialog();
            UpdateView();
        }
        
        public void AddTimeTracking()
        {
            View.Windows.AddTimeTrackingWindow EditWin = new View.Windows.AddTimeTrackingWindow(new WorkingTime());
            EditWin.ShowDialog();
            UpdateView();
        }

        public void CreateReport()
        {
            View.Windows.CreateReportWindow EditWin = new View.Windows.CreateReportWindow();
            EditWin.ShowDialog();
            UpdateView();
        }

    }
}
