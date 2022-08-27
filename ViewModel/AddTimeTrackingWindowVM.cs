using SDE_TimeTracking.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SDE_TimeTracking.ViewModel
{
    public class AddTimeTrackingWindowVM
    {
        public WorkingTime WorkingObject { get; set; }
        public ObservableCollection<Employees> AllEmployees { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Today.AddMonths(-2);
        public DateTime EndDate { get; set; } = DateTime.Today.AddMonths(2);

        public TimeSpan StartTime { get; set; } = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        public TimeSpan EndTime { get; set; }


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
        }

        public int Save()
        {
            if (WorkingObject.Employees == null || WorkingObject.TimeStart == null)
            {
                MessageBox.Show("Поля \"Сотрудник\", \"Вход в здание\" и \"Время входа\" обязательны для заполнения!",
                    "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return -1;
            }
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
            try
            {
                WorkingObject.EmployeeID = WorkingObject.Employees.ID;
                using (var Context = new SDEEntities())
                {
                    /*
                    //Context.Configuration.ValidateOnSaveEnabled = false;
                    if (WorkingObject.TimeEnd == null)
                    {
                        Context.WorkingTime.AddOrUpdate(WorkingObject);
                        //Context.WorkingTime.Attach(WorkingObject);
                        //Context.Entry(WorkingObject).State = System.Data.Entity.EntityState.Added;
                    }
                    else
                    {
                        Context.Entry(WorkingObject).State = System.Data.Entity.EntityState.Modified;
                    }
                    //Context.Configuration.ValidateOnSaveEnabled = true;
                    */
                    Context.WorkingTime.AddOrUpdate(WorkingObject);
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
