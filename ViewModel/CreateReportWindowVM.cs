using Microsoft.Win32;
using SDE_TimeTracking.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace SDE_TimeTracking.ViewModel
{
    public class CreateReportWindowVM
    {
        public ObservableCollection<WorkingTime> AllWorkingTimes { get; set; }
        public ObservableCollection<Employees> AllEmployees { get; set; }
        public ObservableCollection<Positions> AllPositions { get; set; }
        public ObservableCollection<Enterprises> AllEnterprises { get; set; }
        public ObservableCollection<Departments> AllDepartments { get; set; }
        public DateTime SelectStartDate { get; set; } = DateTime.Today.AddMonths(-1);
        public DateTime SelectEndDate { get; set; } = DateTime.Today;

        public DateTime StartDate { get; set; } = DateTime.Today.AddYears(-10);
        public DateTime EndDate { get; set; } = DateTime.Today;

        public int Save()
        {
            try
            {
                if (SelectEndDate <= SelectStartDate)
                {
                    MessageBox.Show("Время выхода из здания не может быть раньше, чем время входа. " +
                        "Отчетный период должен быть больше 1 дня",
                        "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return -1;
                }

                using (var Context = new SDEEntities())
                {
                    /*
                    AllWorkingTimes = new ObservableCollection<WorkingTime>(Context.WorkingTime.ToList().
                        OrderBy(i => i.Employees.Surname).Where(item => 
                        item.TimeStart > SelectStartDate && (item.TimeEnd != null && item.TimeEnd < SelectEndDate)));
                    */
                    AllWorkingTimes = new ObservableCollection<WorkingTime>(Context.WorkingTime.ToList().
                        OrderBy(i => i.TimeStart).Where(item =>
                        item.TimeStart >= SelectStartDate && (item.TimeEnd != null && item.TimeEnd <= SelectEndDate)));

                    AllEmployees = new ObservableCollection<Employees>(Context.Employees.ToList().Where(i => 
                    i.DateEndWork == null || (i.DateEndWork > SelectStartDate && i.DateEndWork < SelectEndDate)));
                    AllEnterprises = new ObservableCollection<Enterprises>(Context.Enterprises.ToList());
                    AllPositions = new ObservableCollection<Positions>(Context.Positions.ToList());
                    AllDepartments = new ObservableCollection<Departments>(Context.Departments.ToList());
                    /*
                    foreach (var item in AllWorkingTimes)
                    {
                        item.Employees = AllEmployees.Where(i => i.ID == item.EmployeeID).ToList()[0];

                        item.Employees.Enterprises = AllEnterprises.Where(i => 
                        i.ID == item.Employees.EnterpriseID).ToList()[0];

                        item.Employees.Positions = AllPositions.Where(i => 
                        i.ID == item.Employees.PositionID).ToList()[0];

                        item.Employees.Departments = AllDepartments.Where(i =>
                        i.ID == item.Employees.DepartmentID).ToList()[0];
                    }
                    
                    foreach (var item in AllEmployees)
                    {
                        item.Positions = AllPositions.Where(i => i.ID == item.PositionID).ToList()[0];
                        item.Enterprises = AllEnterprises.Where(i => i.ID == item.EnterpriseID).ToList()[0];
                        item.Departments = AllDepartments.Where(i => i.ID == item.DepartmentID).ToList()[0];
                        item.WorkingTime = AllWorkingTimes.Where(i => i.EmployeeID == item.ID && i.TimeEnd != null).ToList();
                    }
                    */
                    using (ExcelWorkBase helper = new ExcelWorkBase())
                    {
                        SaveFileDialog saveFileDialog = new SaveFileDialog();
                        saveFileDialog.FileName = "" + SelectStartDate.ToString("d") + " - " + SelectEndDate.ToString("d") + " табель.xlsx";
                        saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                        saveFileDialog.Title = "Выберите путь сохранения отчета";
                        saveFileDialog.Filter = "Excel .xlsx Files (*.xlsx)|*.xlsx";
                        if (saveFileDialog.ShowDialog() == true)
                        {
                            if (helper.Open(saveFileDialog.FileName))
                            {
                                helper.Edit(AllWorkingTimes, AllEmployees, SelectStartDate, SelectEndDate);

                                helper.Save();
                            }
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сообщение ошибки: \n" + ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }
        }
    }
}
