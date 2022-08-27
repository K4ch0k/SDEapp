using SDE_TimeTracking.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace SDE_TimeTracking.ViewModel
{
    public class DepartmentsTablePageVM : PropertyChangedBase
    {
        private ObservableCollection<Departments> _allDepartments;

        public ObservableCollection<Departments> AllDepartments
        {
            get { return _allDepartments; }
            set
            {
                _allDepartments = value;
                OnPropertyChanged(nameof(AllDepartments));
            }
        }

        public ObservableCollection<Employees> AllEmployees { get; set; }


        public DepartmentsTablePageVM()
        {
            UpdateTable();
        }

        public void UpdateTable()
        {
            try
            {
                using (var Context = new SDEEntities())
                {
                    AllDepartments = new ObservableCollection<Departments>(Context.Departments.ToList().Where(i => i.ID != 1));
                    AllEmployees = new ObservableCollection<Employees>(Context.Employees.ToList());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сообщение ошибки: \n" + ex.Message.ToString(), "Ошибка", (MessageBoxButtons)MessageBoxButton.OK, (MessageBoxIcon)MessageBoxImage.Error);
            }
        }

        public async void DelDepartment(object item)
        {
            Departments delItem = item as Departments;

            var employees = AllEmployees.Where(i => i.Departments == delItem).ToList();

            DialogResult resDialog = MessageBox.Show("Удалить подразделение " + delItem.Name +
                " с " + employees.Count + " сотрудниками из БД?\n",
                "Подтвердите действие", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resDialog == DialogResult.Yes)
            {
                try
                {
                    using (var Context = new SDEEntities())
                    {
                        Context.Configuration.ValidateOnSaveEnabled = false;
                        Context.Entry(delItem).State = System.Data.Entity.EntityState.Deleted;
                        foreach (var empl in employees)
                        {
                            empl.DepartmentID = 1;
                            Context.Entry(empl).State = System.Data.Entity.EntityState.Modified;
                        }
                        Context.Departments.Remove(delItem);
                        Context.Configuration.ValidateOnSaveEnabled = true;
                        await Context.SaveChangesAsync();
                        UpdateTable();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Сообщение ошибки: \n" + ex.Message.ToString(), "Ошибка", (MessageBoxButtons)MessageBoxButton.OK, (MessageBoxIcon)MessageBoxImage.Error);
                }
            }
        }

        public ObservableCollection<Departments> Search(string text)
        {
            try
            {
                using (var Context = new SDEEntities())
                {
                    AllDepartments = new ObservableCollection<Departments>(Context.Departments.Where(item =>
                    item.Name.Contains(text) ||
                    (item.Description != null && item.Description.Contains(text))).ToList().Where(i => i.ID != 1));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сообщение ошибки: \n" + ex.Message.ToString(), "Ошибка", (MessageBoxButtons)MessageBoxButton.OK, (MessageBoxIcon)MessageBoxImage.Error);
            }
            return AllDepartments;
        }

        public void AddDepartment()
        {
            View.Windows.EditDepartmentsWindow EditWin = new View.Windows.EditDepartmentsWindow(new Departments());
            EditWin.ShowDialog();

            UpdateTable();
        }

        public void EditDepartment(object item)
        {
            if (item == null)
                return;
            Departments editItem = item as Departments;
            View.Windows.EditDepartmentsWindow EditWin = new View.Windows.EditDepartmentsWindow(editItem);
            EditWin.ShowDialog();
            UpdateTable();
        }
    }
}
