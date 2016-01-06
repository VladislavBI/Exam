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
            SqlCommand cmd = new SqlCommand("SELECT Max(Order_ID)+1 FROM [VST].[dbo].[Order]", App.con);
            
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
            orderList = Full;
            DGridOrderList.ItemsSource = View.DefaultView;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DB_Folder.StatisticWindow sw = new DB_Folder.StatisticWindow("Category", Convert.ToInt32(TBlOrderNo.Text));
            sw.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SqlConnectionStringBuilder conStringBuilder = new SqlConnectionStringBuilder();
            conStringBuilder.DataSource = @"vladp";
            conStringBuilder.InitialCatalog = "VST";
            conStringBuilder.IntegratedSecurity = true;
            conStringBuilder.Pooling = true;
            App.con = new SqlConnection(conStringBuilder.ConnectionString);
            App.con.Open();

            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(App.con))
            {
                bulkCopy.DestinationTableName =
                    "Order_Detail";

                try
                {
                    // Write from the source to the destination.
                    bulkCopy.WriteToServer(orderList);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            try
            {
            SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Order] ([Order_ID], [Date], [Sum], [Debt], [Internet], [Seller_ID], [Warehouse_ID])" +
                    "VALUES (@Order_ID, @Date, @Sum, @Debt, @Internet, @Seller_ID, @Warehouse_ID)", App.con);

            cmd.Parameters.Add("@Order_ID", SqlDbType.Int).Value =Convert.ToInt32(TBlOrderNo.Text);
            cmd.Parameters.Add("@Date", SqlDbType.SmallDateTime).Value = CurDateTBlock.Text;

            cmd.Parameters.Add("@Debt", SqlDbType.Int).Value = ChBoxResultReturn(rButDebt);
            cmd.Parameters.Add("@Internet", SqlDbType.Int).Value = ChBoxResultReturn(rButInet);
            
                SqlCommand WHGetCmd = new SqlCommand("SELECT Warehouse_ID FROM Warehouses WHERE Name=@Name", App.con);
                WHGetCmd.Parameters.Add("@Name", SqlDbType.NVarChar, 20).Value = TBlFrom.Text;
                MessageBox.Show(Convert.ToInt32(WHGetCmd.ExecuteScalar()).ToString());
                cmd.Parameters.Add("@Seller_ID", SqlDbType.Int).Value = Convert.ToInt32(WHGetCmd.ExecuteScalar());
                WHGetCmd.Parameters["@Name"].Value = TBlTo.Text;
                cmd.Parameters.Add("@Warehouse_ID", SqlDbType.Int).Value = Convert.ToInt32(WHGetCmd.ExecuteScalar());

           
            

            cmd.Parameters.Add("@Sum", SqlDbType.Int).Value = GetOrderSum();

            cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            App.con.Close();
            this.Close();
        }

        int ChBoxResultReturn(RadioButton ch)
        {
            if (ch.IsChecked == true)
                return 1;
            else
                return 0;

        }

        int GetOrderSum()
        {
            int sum = 0;
            for (int i=0; i<orderList.Rows.Count; i++)
            {
                sum += Convert.ToInt32(orderList.Rows[i]["Price"]);
            }
            return sum;
        }

    }
}
