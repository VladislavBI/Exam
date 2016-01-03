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
using System.Data;

namespace Exam_VSTBuh.MainWindow_Sale_Report
{
    /// <summary>
    /// Interaction logic for NewSale.xaml
    /// </summary>
    public partial class NewSale : Window
    {
        DataTable orderList = new DataTable();
        public NewSale()
        {
            InitializeComponent();

            //Создание даты - продажка
            DateTime curDate = DateTime.Today;
            CurDateTBlock.Text = curDate.Day.ToString() + "." + curDate.Month.ToString() + "." + curDate.Year.ToString();
            App.con = new SqlConnection(@"Data Source=localhost;Initial Catalog=VST;Integrated Security=True");
            App.con.Open();
            SqlCommand cmd = new SqlCommand("SELECT Max(Order_ID) FROM [VST].[dbo].[Order]", App.con);
            
            if (cmd.ExecuteScalar() == DBNull.Value)
            {
                TBlOrderNo.Text = "1";
            }
            else
            {
                TBlOrderNo.Text = cmd.ExecuteScalar().ToString();
            }

            DelegatesData.NSWHCHandler = new DelegatesData.newSaleWHChange(WHChange);
            DelegatesData.NSOLCHandler = new DelegatesData.newSaleOrderlistChange(OrderListChange);
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
            cBoxSellerList.SelectedIndex = 0;
            sqlCon.Close();
            

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button temp = sender as Button;
            DB_Folder.StatisticWindow sw = new DB_Folder.StatisticWindow("Warehouses", (string)temp.Tag);
            sw.ShowDialog();
        }

     
        void WHChange(string name, string data)
        {
            if (name == "TBlFrom")
                TBlFrom.Text = data;
            else
                TBlTo.Text = data;
        }

        void OrderListChange(DataTable Full, DataTable View)
        {
            orderList = Full.Clone();
            DGridOrderList.ItemsSource = View.DefaultView;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DB_Folder.StatisticWindow sw = new DB_Folder.StatisticWindow("Category", Convert.ToInt32(TBlOrderNo.Text));
            sw.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

    }
}
