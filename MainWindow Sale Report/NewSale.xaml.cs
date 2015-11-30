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

namespace Exam_VSTBuh.MainWindow_Sale_Report
{
    /// <summary>
    /// Interaction logic for NewSale.xaml
    /// </summary>
    public partial class NewSale : Window
    {
        public NewSale()
        {
            InitializeComponent();

            //Создание даты - продажка
            DateTime curDate = DateTime.Today;
            CurDateTBlock.Text = curDate.Day.ToString() + "." + curDate.Month.ToString() + "." + curDate.Year.ToString();
        }
    }
}
