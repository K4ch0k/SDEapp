using SDE_TimeTracking.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace SDE_TimeTracking.ViewModel
{
    public class EmployeesTablePageVM : PropertyChangedBase
    {
        private ObservableCollection<Employees> _allEmployees;
        public ObservableCollection<Employees> AllEmployees
        {
            get { return _allEmployees; }
            set 
            { 
                _allEmployees = value;
                OnPropertyChanged(nameof(AllEmployees));
            }
        }

        public ObservableCollection<Positions> AllPositions { get; set; }
        public ObservableCollection<Enterprises> AllEnterprises { get; set; }
        public ObservableCollection<Departments> AllDepartments { get; set; }



        public EmployeesTablePageVM()
        {
            //UpdateTable();
        }

        public void UpdateTable()
        {
            try
            {
                using (var Context = new SDEEntities())
                {
                    AllEmployees = new ObservableCollection<Employees>(Context.Employees.ToList());
                    AllEnterprises = new ObservableCollection<Enterprises>(Context.Enterprises.ToList());
                    AllDepartments = new ObservableCollection<Departments>(Context.Departments.ToList());
                    AllPositions = new ObservableCollection<Positions>(Context.Positions.ToList());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сообщение ошибки: \n" + ex.Message.ToString(), "Ошибка", (MessageBoxButtons)MessageBoxButton.OK, (MessageBoxIcon)MessageBoxImage.Error);
            }
        }

        public async void DelEmployee(object item)
        {
            Employees delItem = item as Employees;

            DialogResult resDialog = MessageBox.Show("Удалить сотрудника " + delItem.Name + " из БД?",
                "Подтвердите действие", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resDialog == DialogResult.Yes)
            {
                try
                {
                    using (var Context = new SDEEntities())
                    {
                        Context.Configuration.ValidateOnSaveEnabled = false;
                        Context.Entry(delItem).State = System.Data.Entity.EntityState.Deleted;
                        Context.Employees.Remove(delItem);
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

        public ObservableCollection<Employees> Search(string text)
        {
            try
            {
                using (var Context = new SDEEntities())
                {
                    AllEmployees = new ObservableCollection<Employees>(Context.Employees.Where(item =>
                    item.PersonnelNumber.Contains(text) ||
                    item.Phone.Contains(text) ||
                    item.Address.Contains(text) ||
                    item.Positions.Name.Contains(text) ||
                    item.Enterprises.Name.Contains(text) ||
                    item.Enterprises.Address.Contains(text) ||
                    item.Departments.Name.Contains(text) ||
                    item.Name.Contains(text) ||
                    item.Surname.Contains(text) ||
                    (item.LastName != null && item.LastName.Contains(text))).ToList());
                    AllEnterprises = new ObservableCollection<Enterprises>(Context.Enterprises.ToList());
                    AllDepartments = new ObservableCollection<Departments>(Context.Departments.ToList());
                    AllPositions = new ObservableCollection<Positions>(Context.Positions.ToList());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сообщение ошибки: \n" + ex.Message.ToString(), "Ошибка", (MessageBoxButtons)MessageBoxButton.OK, (MessageBoxIcon)MessageBoxImage.Error);
            }
            return AllEmployees;
        }

        public void AddEmployee()
        {
            View.Windows.EditEmployeesWindow EditWin = new View.Windows.EditEmployeesWindow(new Employees());
            EditWin.ShowDialog();
            UpdateTable();
        }

        public void EditEmployee(object item)
        {
            if (item == null)
                return;
            Employees editItem = item as Employees;


            var a = AllPositions.ToList().Where(i => i.ID == editItem.PositionID);

            /*
            editItem.Positions = (Positions)AllPositions.Select(i => i.ID == editItem.PositionID);
            editItem.Enterprises = (Enterprises)AllEnterprises.Where(i => i.ID == editItem.EnterpriseID);
            editItem.Departments = (Departments)AllDepartments.Where(i => i.ID == editItem.DepartmentID);
            */

            View.Windows.EditEmployeesWindow EditWin = new View.Windows.EditEmployeesWindow(editItem);
            EditWin.ShowDialog();
            UpdateTable();
        }
    }
}
