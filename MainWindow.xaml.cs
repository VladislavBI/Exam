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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Exam_VSTBuh.MainWindow_Sale_Report;
using System.Text.RegularExpressions;

namespace Exam_VSTBuh
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Создание даты - главное меню
            DateTime curDate=DateTime.Today;
            CurDateTblock.Text = curDate.Day.ToString() + "." + curDate.Month.ToString() + "." + curDate.Year.ToString(); ;
        }


        #region Действия на гланом экране
        private void CreateNewSale(object sender, RoutedEventArgs e)
        {
            NewSale NS = new NewSale();
            NS.Show();
        }

        private void CreateReport(object sender, RoutedEventArgs e)
        {
            NewReport NR = new NewReport();
            NR.Show();
        }
        #endregion

        private void CheckWritten(object sender, RoutedEventArgs e) //проверка правописания курсов
        {
            TextBox temp = sender as TextBox;
            string wrote = temp.Text.ToString();
            //проверка правописания курсов
            wrote = wrote.Contains('.') ? wrote : wrote.Replace(",", ".");//запятая в точку
           
            if (Regex.IsMatch(wrote, @"[^0-9|.]")||Regex.IsMatch(wrote, @"^+[0]+$"))
            { wrote = "1"; }//проверка на буквы и только 0
            
           
            temp.Text = wrote;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            DB_Folder.StatisticWindow sw = new DB_Folder.StatisticWindow();
            sw.Show();
        }
    }
}
