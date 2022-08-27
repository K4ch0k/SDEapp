using SDE_TimeTracking.Model;
using System;
using System.Data.Entity.Infrastructure;
using System.Windows;

namespace SDE_TimeTracking.ViewModel
{
    public class EditPositionsWindowVM
    {
        public Positions PositionObject { get; set; }

        public void EditView(object item)
        {
            PositionObject = item as Positions;
        }

        public int Save()
        {
            if (string.IsNullOrWhiteSpace(PositionObject.Name))
            {
                MessageBox.Show("Поле \"Наименование\" обязательно для заполнения!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return -1;
            }
            if (PositionObject.ID == 1)
            {
                MessageBox.Show("Данную должность нельзя изменять !", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return 0;
            }
            try
            {
                using (var Context = new SDEEntities())
                {
                    Context.Configuration.ValidateOnSaveEnabled = false;
                    if (PositionObject.ID == 0)
                    {
                        Context.Positions.Add(PositionObject);
                        Context.Positions.Attach(PositionObject);
                        Context.Entry(PositionObject).State = System.Data.Entity.EntityState.Added;
                    }
                    else
                    {
                        if (String.IsNullOrWhiteSpace(PositionObject.Description))
                        {
                            PositionObject.Description = null;
                        }
                        Context.Entry(PositionObject).State = System.Data.Entity.EntityState.Modified;
                    }
                    Context.Configuration.ValidateOnSaveEnabled = true;
                    Context.SaveChanges();
                }
                return 1;
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show("Вероятно, вы пытаетесь добавить должность, которая уже занесена в БД.\nСообщение ошибки: \n" + ex.Message.ToString(),
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
