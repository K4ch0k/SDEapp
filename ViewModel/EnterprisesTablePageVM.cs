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
    public class EnterprisesTablePageVM : PropertyChangedBase
    {
        private ObservableCollection<Enterprises> _allEnterprises;
        public ObservableCollection<Enterprises> AllEnterprises 
        { 
            get { return _allEnterprises; } 
            set 
            { 
                _allEnterprises = value;
                OnPropertyChanged(nameof(AllEnterprises));
            }
        }
        public ObservableCollection<Employees> AllEmployees { get; set; }

        public EnterprisesTablePageVM()
        {
            UpdateTable();
        }

        public void UpdateTable()
        {
            try
            {
                using (var Context = new SDEEntities())
                {
                    AllEnterprises = new ObservableCollection<Enterprises>(Context.Enterprises.ToList().Where(i => i.ID != 1));
                    AllEmployees = new ObservableCollection<Employees>(Context.Employees.ToList());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сообщение ошибки: \n" + ex.Message.ToString(), "Ошибка", (MessageBoxButtons)MessageBoxButton.OK, (MessageBoxIcon)MessageBoxImage.Error);
            }
        }

        public async void DelEnterprise(object item)
        {
            Enterprises delItem = item as Enterprises;

            var employees = AllEmployees.Where(i => i.Enterprises == delItem).ToList();

            DialogResult resDialog = MessageBox.Show("Удалить организацию " + delItem.Name + 
                " с " + employees.Count + " сотрудниками из БД?",
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
                            empl.EnterpriseID = 1;
                            Context.Entry(empl).State = System.Data.Entity.EntityState.Modified;
                        }
                        Context.Enterprises.Remove(delItem);
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

        public ObservableCollection<Enterprises> Search(string text)
        {
            try
            {
                using (var Context = new SDEEntities())
                {
                    AllEnterprises = new ObservableCollection<Enterprises>(Context.Enterprises.Where(item =>
                    item.Name.Contains(text) ||
                    item.Address.Contains(text) ||
                    (item.Description != null && item.Description.Contains(text))).ToList().Where(i => i.ID != 1));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сообщение ошибки: \n" + ex.Message.ToString(), "Ошибка", (MessageBoxButtons)MessageBoxButton.OK, (MessageBoxIcon)MessageBoxImage.Error);
            }
            return AllEnterprises;
        }

        public void AddCompany()
        {
            View.Windows.EditEnterprisesWindow EditWin = new View.Windows.EditEnterprisesWindow(new Enterprises());
            EditWin.ShowDialog();
            UpdateTable();
        }

        public void EditCompany(object item)
        {
            if (item == null)
                return;
            Enterprises editItem = item as Enterprises;
            View.Windows.EditEnterprisesWindow EditWin = new View.Windows.EditEnterprisesWindow(editItem);
            EditWin.ShowDialog();
            UpdateTable();
        }
    }
}
