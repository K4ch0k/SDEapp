using SDE_TimeTracking.Model;
using System;
using System.Linq;
using System.Windows;

namespace SDE_TimeTracking.ViewModel
{
    public class MainWindowVM
    {
        public int qAllEnterprises { get; set; }
        public int qAllEmployees { get; set; }
        public int qAllDepartments { get; set; }
        public int qAllPositions { get; set; }
        public int qAllWorkingTimes { get; set; }

        public MainWindowVM()
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
        }

        public void AddDepartment()
        {
            View.Windows.EditDepartmentsWindow EditWin = new View.Windows.EditDepartmentsWindow(new Departments());
            EditWin.ShowDialog();
        }

        public void AddPositions()
        {
            View.Windows.EditPositionsWindow EditWin = new View.Windows.EditPositionsWindow(new Positions());
            EditWin.ShowDialog();
        }

        public void AddEmployees()
        {
            View.Windows.EditEmployeesWindow EditWin = new View.Windows.EditEmployeesWindow(new Employees());
            EditWin.ShowDialog();
        }
        
        public void AddTimeTracking()
        {
            View.Windows.AddTimeTrackingWindow EditWin = new View.Windows.AddTimeTrackingWindow(new WorkingTime());
            EditWin.ShowDialog();
        }

    }
}
