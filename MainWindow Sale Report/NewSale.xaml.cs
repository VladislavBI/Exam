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
using System.Data.SqlClient;

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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SqlConnectionStringBuilder stringCon = new SqlConnectionStringBuilder();
            stringCon.DataSource = "vladp";
            stringCon.InitialCatalog = "VST";
            stringCon.IntegratedSecurity = true;

            SqlConnection sqlCon = new SqlConnection(stringCon.ConnectionString);
                
            sqlCon.Open();
            SqlCommand sqlCom = new SqlCommand("SELECT Name from Sellers", sqlCon);
            SqlDataReader reader = sqlCom.ExecuteReader();

            while (reader.Read()) 
               {
                            cBoxSellerList.Items.Add(reader[0]);
               }
                
            sqlCon.Close();
            

        }


    }
}
