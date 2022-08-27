using SDE_TimeTracking.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Windows;

namespace SDE_TimeTracking.ViewModel
{
    public class EditEmployeesWindowVM : PropertyChangedBase
    {
        public Employees EmployeeObject { get; set; }
        public ObservableCollection<Positions> AllPositions { get; set; }
        public ObservableCollection<Enterprises> AllEnterprises { get; set; }
        public ObservableCollection<Departments> AllDepartments { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Today.AddMonths(-2);
        public DateTime EndDate { get; set; } = DateTime.Today.AddYears(1);
        private bool _isNotWorking = false;
        public bool IsNotWorking
        {
            get { return _isNotWorking; }
            set
            {
                _isNotWorking = value;
                OnPropertyChanged(nameof(IsNotWorking));
            }
        }

        public EditEmployeesWindowVM()
        {
            try
            {
                using (var Context = new SDEEntities())
                {
                    AllPositions = new ObservableCollection<Positions>(Context.Positions.ToList());
                    AllEnterprises = new ObservableCollection<Enterprises>(Context.Enterprises.ToList());
                    AllDepartments = new ObservableCollection<Departments>(Context.Departments.ToList());
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
            EmployeeObject = item as Employees;
            if (EmployeeObject.ID == 0)
            {
                EmployeeObject.PersonnelNumber = GenerateNumber();
                EmployeeObject.DateStartWork = DateTime.Now;
                EmployeeObject.Positions = AllPositions[0] as Positions;
                EmployeeObject.Enterprises = AllEnterprises[0] as Enterprises;
                EmployeeObject.Departments = AllDepartments[0] as Departments;
            }
            else
            {
                if (EmployeeObject.DateEndWork != null)
                {
                    IsNotWorking = true;
                }
                StartDate = EmployeeObject.DateStartWork.AddMonths(-1);

                

                var listD = AllDepartments.Where(i => i.ID == EmployeeObject.DepartmentID).ToList();
                var resD = listD[0];
                EmployeeObject.Departments = resD;

                var listP = AllPositions.Where(i => i.ID == EmployeeObject.PositionID).ToList();
                var resP = listP[0];
                EmployeeObject.Positions = resP;

                var listE = AllEnterprises.Where(i => i.ID == EmployeeObject.EnterpriseID).ToList();
                var resE = listE[0];
                EmployeeObject.Enterprises = resE;
            }
        }

        public string GenerateNumber()
        {
            Random rnd = new Random();
            string st = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            string st2 = "";
            int n;
            int q = 0;

            while (q < 10)
            {
                q++;
                n = rnd.Next(0, 61);
                st2 += st.Substring(n, 1);
            }

            return st2;
        }

        public ObservableCollection<Positions> SearchPos(string text)
        {
            /*
            var CBsearch = sender as ComboBox;
            var tb = (TextBox)e.OriginalSource;
            tb.Select(tb.SelectionStart + tb.SelectionLength, 0);
            CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(CBsearch.ItemsSource as AllPositions);
            cv.Filter = s => ((string)s).IndexOf(CBsearch.Text, StringComparison.CurrentCultureIgnoreCase) >= 0;
            */
            try
            {
                using (var Context = new SDEEntities())
                {
                    AllPositions = new ObservableCollection<Positions>(Context.Positions.Where(item =>
                    item.Name.Contains(text) ||
                    (item.Description != null && item.Description.Contains(text))));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сообщение ошибки: \n" + ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return AllPositions;
        }

        public int Save()
        {
            if (string.IsNullOrWhiteSpace(EmployeeObject.Surname) ||
                string.IsNullOrWhiteSpace(EmployeeObject.Name) ||
                string.IsNullOrWhiteSpace(EmployeeObject.PersonnelNumber) ||
                string.IsNullOrWhiteSpace(EmployeeObject.DateStartWork.ToString()))
            {
                MessageBox.Show("Поля \"Таб. номер\", \"Фамилия\", \"Имя\" и \"Дата трудоустройства\" обязательны для заполнения!", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return -1;
            }
            try
            {
                using (var Context = new SDEEntities())
                {
                    EmployeeObject.DepartmentID = EmployeeObject.Departments.ID;
                    EmployeeObject.EnterpriseID = EmployeeObject.Enterprises.ID;
                    EmployeeObject.PositionID = EmployeeObject.Positions.ID;
                    EmployeeObject.Departments = null;
                    EmployeeObject.Enterprises = null;
                    EmployeeObject.Positions = null;
                    //Context.Configuration.ValidateOnSaveEnabled = false;
                    if (EmployeeObject.ID == 0)
                    {
                        Context.Employees.Add(EmployeeObject);

                        //Context.Employees.Attach(EmployeeObject);
                        //Context.Entry(EmployeeObject).State = System.Data.Entity.EntityState.Added;
                    }
                    else
                    {
                        if (IsNotWorking == false || String.IsNullOrWhiteSpace(EmployeeObject.DateEndWork.ToString()))
                        {
                            EmployeeObject.DateEndWork = null;
                        }
                        Context.Entry(EmployeeObject).State = System.Data.Entity.EntityState.Modified;
                    }
                    //Context.Configuration.ValidateOnSaveEnabled = true;


                    Context.SaveChanges();
                }
                return 1;
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show("Вероятно, вы пытаетесь добавить сотрудника, который уже занесен в БД (Таб. номер и номер телефона должны быть уникальны)." +
                    "\nСообщение ошибки: \n" + ex.Message.ToString(),
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сообщение ошибки: \n" + ex.Message.ToString(), "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }
        }

    }
}
