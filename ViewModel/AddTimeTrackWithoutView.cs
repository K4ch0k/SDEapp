using SDE_TimeTracking.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SDE_TimeTracking.ViewModel
{
    public class AddTimeTrackWithoutView
    {
        public WorkingTime WorkingObject { get; set; }

        public int AddTimeTrack(int empID)
        {
            try
            {
                using (var Context = new SDEEntities())
                {
                    Random rnd = new Random();
                    ObservableCollection<Employees> AllEmployees =
                        new ObservableCollection<Employees>(Context.Employees.ToList().Where(i => i.ID == empID));
                    Employees emp = AllEmployees[0];

                    for (DateTime allDays = emp.DateStartWork.AddDays(1); allDays < DateTime.Today; allDays = allDays.AddDays(1))
                    {
                        var check = Context.WorkingTime.ToList().Where(i => i.EmployeeID == emp.ID && i.TimeStart.Date == allDays.Date);
                        if (check.Any())
                        {
                            continue;
                        }
                        if (emp.DateEndWork != null && emp.DateEndWork < allDays)
                        {
                            continue;
                        }
                        if (allDays.DayOfWeek == DayOfWeek.Saturday || allDays.DayOfWeek == DayOfWeek.Sunday)
                        {
                            continue;
                        }
                        WorkingObject.EmployeeID = emp.ID;
                        WorkingObject.TimeStart = allDays;
                        WorkingObject.TimeEnd = allDays;
                        WorkingObject.TimeStart += new TimeSpan(7, rnd.Next(45, 59), rnd.Next(0, 59));
                        WorkingObject.TimeEnd += new TimeSpan(18, rnd.Next(0, 25), rnd.Next(0, 59));
                        Context.WorkingTime.Add(WorkingObject);
                        Context.SaveChanges();
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
