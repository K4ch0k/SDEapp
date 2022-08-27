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
    public class PositionsTablePageVM : PropertyChangedBase
    {
        private ObservableCollection<Positions> _allPositions;
        public ObservableCollection<Positions> AllPositions
        {
            get { return _allPositions; }
            set
            {
                _allPositions = value;
                OnPropertyChanged(nameof(AllPositions));
            }
        }
        public ObservableCollection<Employees> AllEmployees { get; set; }

        public PositionsTablePageVM()
        {
            UpdateTable();
        }

        public void UpdateTable()
        {
            try
            {
                using (var Context = new SDEEntities())
                {
                    AllPositions = new ObservableCollection<Positions>(Context.Positions.ToList().Where(i => i.ID != 1));
                    AllEmployees = new ObservableCollection<Employees>(Context.Employees.ToList());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сообщение ошибки: \n" + ex.Message.ToString(), "Ошибка", (MessageBoxButtons)MessageBoxButton.OK, (MessageBoxIcon)MessageBoxImage.Error);
            }
        }

        public async void DelPosition(object item)
        {
            Positions delItem = item as Positions;
            var employees = AllEmployees.Where(i => i.Positions == delItem).ToList();

            DialogResult resDialog = MessageBox.Show("Удалить должность " + delItem.Name +
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
                            empl.PositionID = 1;
                            Context.Entry(empl).State = System.Data.Entity.EntityState.Modified;
                        }
                        Context.Positions.Remove(delItem);
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

        public ObservableCollection<Positions> Search(string text)
        {
            try
            {
                using (var Context = new SDEEntities())
                {
                    AllPositions = new ObservableCollection<Positions>(Context.Positions.Where(item =>
                    item.Name.Contains(text) ||
                    (item.Description != null && item.Description.Contains(text))).ToList().Where(i => i.ID != 1));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сообщение ошибки: \n" + ex.Message.ToString(), "Ошибка", (MessageBoxButtons)MessageBoxButton.OK, (MessageBoxIcon)MessageBoxImage.Error);              
            }
            return AllPositions;
        }

        public void AddPosition()
        {
            View.Windows.EditPositionsWindow AddPosition = new View.Windows.EditPositionsWindow(new Positions());
            AddPosition.ShowDialog();
            UpdateTable();
        }

        public void EditPosition(object item)
        {
            if (item == null)
                return;
            Positions editItem = item as Positions;
            View.Windows.EditPositionsWindow AddPosition = new View.Windows.EditPositionsWindow(editItem);
            AddPosition.ShowDialog();
            UpdateTable();
        }
    }
}
