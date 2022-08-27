using SDE_TimeTracking.Model;
using System;
using System.Data.Entity.Infrastructure;
using System.Windows;

namespace SDE_TimeTracking.ViewModel
{
    public class EditEnterprisesWindowVM
    {
        public Enterprises EnterpriseObject { get; set; }

        public EditEnterprisesWindowVM()
        {

        }

        public void EditView(object item)
        {
            EnterpriseObject = item as Enterprises;
        }

        public int Save()
        {
            if (string.IsNullOrWhiteSpace(EnterpriseObject.Name) || string.IsNullOrWhiteSpace(EnterpriseObject.Address))
            {
                MessageBox.Show("Поля \"Наименование\" и \"Адрес\" обязательны для заполнения!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return -1;
            }
            if (EnterpriseObject.ID == 1)
            {
                MessageBox.Show("Данную организацию нельзя изменять !", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return 0;
            }
            try
            {
                using (var Context = new SDEEntities())
                {
                    Context.Configuration.ValidateOnSaveEnabled = false;
                    if (EnterpriseObject.ID == 0)
                    {
                        Context.Enterprises.Add(EnterpriseObject);
                        Context.Enterprises.Attach(EnterpriseObject);
                        Context.Entry(EnterpriseObject).State = System.Data.Entity.EntityState.Added;
                    }
                    else
                    {
                        if (String.IsNullOrWhiteSpace(EnterpriseObject.Description))
                        {
                            EnterpriseObject.Description = null;
                        }
                        Context.Entry(EnterpriseObject).State = System.Data.Entity.EntityState.Modified;
                    }
                    Context.Configuration.ValidateOnSaveEnabled = true;
                    Context.SaveChanges();
                }
                return 1;
            }
            catch(DbUpdateException ex)
            {
                MessageBox.Show("Вероятно, вы пытаетесь добавить организацию, которая уже занесена в БД.\nСообщение ошибки: \n" + ex.Message.ToString(),
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
