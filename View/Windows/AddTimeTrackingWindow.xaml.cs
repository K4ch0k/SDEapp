using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SDE_TimeTracking.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddTimeTrackingWindow.xaml
    /// </summary>
    public partial class AddTimeTrackingWindow : Window
    {
        ViewModel.AddTimeTrackingWindowVM VM = new ViewModel.AddTimeTrackingWindowVM();
        public AddTimeTrackingWindow(object item)
        {
            InitializeComponent();

            DataContext = VM;
            VM.EditView(item);
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            int res = VM.Save();
            switch (res)
            {
                case -1:
                    break;
                case 0:
                    break;
                case 1:
                    this.Close();
                    break;
            }
        }
    }
}
