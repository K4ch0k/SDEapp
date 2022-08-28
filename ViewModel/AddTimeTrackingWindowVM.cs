using SDE_TimeTracking.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace SDE_TimeTracking.ViewModel
{
    public class AddTimeTrackingWindowVM
    {
        public WorkingTime WorkingObject { get; set; }
        public ObservableCollection<Employees> AllEmployees { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Today.AddMonths(-2);
        public DateTime EndDate { get; set; } = DateTime.Today.AddDays(1);

        public TimeSpan StartTime { get; set; } = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        public TimeSpan EndTime { get; set; } = new TimeSpan(DateTime.Now.Hour + 6, DateTime.Now.Minute - 10, DateTime.Now.Second + 133);

        public string StartTimeS { get; set; } = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second).ToString();
        public string EndTimeS { get; set; } = new TimeSpan(DateTime.Now.Hour + 6, DateTime.Now.Minute - 10, DateTime.Now.Second + 133).ToString();

        public AddTimeTrackingWindowVM()
        {
            try
            {
                using (var Context = new SDEEntities())
                {
                    AllEmployees = new ObservableCollection<Employees>(Context.Employees.ToList());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сообщение ошибки: \n" + ex.Message.ToString(), "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void EditView(object item)
        {
            WorkingObject = item as WorkingTime;
            WorkingObject.TimeStart = DateTime.Today;
            WorkingObject.TimeEnd = DateTime.Today;
        }

        public int Save()
        {
            if (WorkingObject.Employees != null)
                WorkingObject.EmployeeID = WorkingObject.Employees.ID;
            if (WorkingObject.EmployeeID != 0)
            {
                var listD = AllEmployees.Where(i => i.ID == WorkingObject.EmployeeID).ToList();
                var resD = listD[0];
                WorkingObject.Employees = resD;
            }
            if (WorkingObject.Employees == null || WorkingObject.TimeStart == null)
            {
                MessageBox.Show("Поля \"Сотрудник\", \"Вход в здание\" и \"Время входа\" обязательны для заполнения!",
                    "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return -1;
            }
            
            try
            {
                StartTime = TimeSpan.Parse(StartTimeS);
                EndTime = TimeSpan.Parse(EndTimeS);

                WorkingObject.TimeStart += StartTime;
                if (WorkingObject.TimeEnd != null)
                {
                    WorkingObject.TimeEnd += EndTime;
                }
                if (WorkingObject.TimeEnd < WorkingObject.TimeStart)
                {
                    MessageBox.Show("Время выхода из здания не может быть раньше, чем время входа",
                        "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return -1;
                }
                if (WorkingObject.TimeStart < WorkingObject.Employees.DateStartWork)
                {
                    MessageBox.Show("Дата входа в здание должно быть позже, чем дата устройства на работу! (" +
                        WorkingObject.Employees.DateStartWork.ToString("d") + ")",
                        "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return -1;
                }

                WorkingObject.Employees = null;
                using (var Context = new SDEEntities())
                {
                    Context.Configuration.ValidateOnSaveEnabled = false;
                    if (WorkingObject.ID == 0)
                    {
                        Context.WorkingTime.Attach(WorkingObject);
                        Context.Entry(WorkingObject).State = System.Data.Entity.EntityState.Added;
                    }
                    else
                    {
                        Context.Entry(WorkingObject).State = System.Data.Entity.EntityState.Modified;
                    }
                    Context.Configuration.ValidateOnSaveEnabled = true;

                    Context.SaveChanges();
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
