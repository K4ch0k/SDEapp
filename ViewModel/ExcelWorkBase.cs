using Microsoft.Office.Interop.Excel;
using SDE_TimeTracking.Model;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using Application = Microsoft.Office.Interop.Excel.Application;
using Excel = Microsoft.Office.Interop.Excel;

namespace SDE_TimeTracking.ViewModel
{
    public class ExcelWorkBase : IDisposable
    {
        private Application _excel;
        private Workbook _workbook;
        private string _filePath;

        public ExcelWorkBase()
        {
            _excel = new Excel.Application();
        }

        public void Dispose()
        {
            try
            {
                if (_workbook != null)
                    _workbook.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сообщение ошибки: \n" + ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public bool Open(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    _workbook = _excel.Workbooks.Open(filePath);
                }
                else
                {
                    _workbook = _excel.Workbooks.Add();
                    _filePath = filePath;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сообщение ошибки: \n" + ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public bool Edit(ObservableCollection<WorkingTime> allWorkingTimes, 
            ObservableCollection<Employees> allEmployees, 
            DateTime selectStartDate, DateTime selectEndDate)
        {
            try
            {
                int i = 4;
                int j = 5;
                ((Excel.Worksheet)_excel.ActiveSheet).Name = DateTime.Today.ToString("d");
                ((Excel.Worksheet)_excel.ActiveSheet).Columns.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ((Excel.Worksheet)_excel.ActiveSheet).Columns.VerticalAlignment = XlHAlign.xlHAlignCenter;

                ((Excel.Worksheet)_excel.ActiveSheet).Columns.ColumnWidth = 30;
                ((Excel.Worksheet)_excel.ActiveSheet).Columns[1].ColumnWidth = 10;
                ((Excel.Worksheet)_excel.ActiveSheet).Columns[2].ColumnWidth = 3;
                ((Excel.Worksheet)_excel.ActiveSheet).Columns[2].NumberFormat = "0";
                ((Excel.Worksheet)_excel.ActiveSheet).Columns[3].NumberFormat = "@";
                ((Excel.Worksheet)_excel.ActiveSheet).Columns[4].NumberFormat = "@";


                ((Excel.Worksheet)_excel.ActiveSheet).Cells[3, 2] = "№";
                ((Excel.Worksheet)_excel.ActiveSheet).Cells[3, 3] = "ФИО, должность";
                ((Excel.Worksheet)_excel.ActiveSheet).Cells[3, 4] = "Организация, Отдел";

                for (DateTime allDays = selectStartDate; allDays <= selectEndDate; allDays = allDays.AddDays(1))
                {
                    if (allDays.DayOfWeek == DayOfWeek.Saturday || allDays.DayOfWeek == DayOfWeek.Sunday)
                    {
                        ((Excel.Worksheet)_excel.ActiveSheet).Cells[3, j].Interior.Color = Excel.XlRgbColor.rgbDarkSeaGreen;
                    }
                    ((Excel.Worksheet)_excel.ActiveSheet).Cells[3, j].NumberFormat = "ДД.ММ.ГГГГ";
                    ((Excel.Worksheet)_excel.ActiveSheet).Columns[j].ColumnWidth = 10;
                    ((Excel.Worksheet)_excel.ActiveSheet).Cells[3, j] = allDays.ToString("d");
                    j++;
                }
                ((Excel.Worksheet)_excel.ActiveSheet).Cells[3, j] = "Время работы";

                foreach (var item in allEmployees)
                {
                    string ln = item.LastName == null ? "" : item.LastName[0] + ".";
                    ((Excel.Worksheet)_excel.ActiveSheet).Cells[i, 2] = (i - 3).ToString();
                    ((Excel.Worksheet)_excel.ActiveSheet).Cells[i, 3] = item.Surname + " " + item.Name[0] + "." + ln + "\n" +
                        item.Positions.Name;
                    ((Excel.Worksheet)_excel.ActiveSheet).Cells[i, 4] = item.Enterprises.Name + ",\n" + item.Departments.Name;

                    j = 5;
                    TimeSpan? resTime = new TimeSpan();
                    for (DateTime allDays = selectStartDate; allDays <= selectEndDate; allDays = allDays.AddDays(1))
                    {
                        TimeSpan? qTIme = new TimeSpan();
                        foreach (var time in item.WorkingTime.ToList().Where(t => t.TimeStart.Date == allDays && t.TimeEnd != null))
                        {
                            qTIme += time.TimeEnd - time.TimeStart;
                        }
                        resTime += qTIme;
                        ((Excel.Worksheet)_excel.ActiveSheet).Cells[i, j].NumberFormat = "@";
                        ((Excel.Worksheet)_excel.ActiveSheet).Cells[i, j].ColumnWidth = 10;
                        ((Excel.Worksheet)_excel.ActiveSheet).Cells[i, j] = qTIme.ToString();
                        j++;
                    }

                    ((Excel.Worksheet)_excel.ActiveSheet).Cells[i, j] = resTime.ToString();

                    ((Excel.Worksheet)_excel.ActiveSheet).Cells[i,3].HorizontalAlignment = XlHAlign.xlHAlignLeft;
                    ((Excel.Worksheet)_excel.ActiveSheet).Cells[i,4].HorizontalAlignment = XlHAlign.xlHAlignLeft;

                    i++;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сообщение ошибки: \n" + ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public void Save()
        {
            if (!string.IsNullOrEmpty(_filePath))
            {
                _workbook.SaveAs(_filePath);
                _filePath = null;
            }
            else
            {
                _workbook.Save();
            }
        }
    }
}
