using SDE_TimeTracking.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SDE_TimeTracking.ViewModel
{
    public class EditDepartmentsWindowVM
    {
        public Departments DepartmentsObject { get; set; }

        public EditDepartmentsWindowVM()
        {

        }

        public void EditView(object item)
        {
            DepartmentsObject = item as Departments;
        }

        public int Save()
        {
            if (string.IsNullOrWhiteSpace(DepartmentsObject.Name))
            {
                MessageBox.Show("Поле \"Наименование\" обязательны для заполнения!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return -1;
            }
            if (DepartmentsObject.ID == 1)
            {
                MessageBox.Show("Данный отдел нельзя изменять !", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return 0;
            }
            try
            {
                using (var Context = new SDEEntities())
                { 
                    Context.Configuration.ValidateOnSaveEnabled = false;
                    if (DepartmentsObject.ID == 0)
                    {
                        Context.Departments.Add(DepartmentsObject);
                        Context.Departments.Attach(DepartmentsObject);
                        Context.Entry(DepartmentsObject).State = System.Data.Entity.EntityState.Added;
                    }
                    else
                    {
                        if (String.IsNullOrWhiteSpace(DepartmentsObject.Description))
                        {
                            DepartmentsObject.Description = null;
                        }
                        Context.Entry(DepartmentsObject).State = System.Data.Entity.EntityState.Modified;
                    }
                    Context.Configuration.ValidateOnSaveEnabled = true;
                    Context.SaveChanges();
                }
                return 1;
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show("Вероятно, вы пытаетесь добавить отдел, который уже занесен в БД.\nСообщение ошибки: \n" + ex.Message.ToString(),
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сообщение ошибки: \n" + ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }
        }
    }
}
