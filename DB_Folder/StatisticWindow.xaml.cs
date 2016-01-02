using System;
using System.Collections.Generic;
using System.Data;
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

namespace Exam_VSTBuh.DB_Folder
{
    /// <summary>
    /// Interaction logic for StatisticWindow.xaml
    /// </summary>
    public partial class StatisticWindow : Window
    {
        int goodLevel = 0;
        bool categorySel;
        string catName;
        public StatisticWindow(string Source)
        {
            InitializeComponent();

            DataSet ds = new DataSet();
            SqlConnectionStringBuilder connectStr = new SqlConnectionStringBuilder();
            connectStr.DataSource = "vladp";
            connectStr.InitialCatalog = "VST";
            connectStr.IntegratedSecurity = true;

            
            SqlDataAdapter adapter=ChooseTable(connectStr, Source);
            
           
            
            
            adapter.Fill(ds);
           
            dataGridView.ItemsSource = ds.Tables[0].DefaultView;
        }

        void goodTableCreate(string tableName, string addInfo)
        {
            
            DataSet ds = new DataSet();
            SqlConnectionStringBuilder connectStr = new SqlConnectionStringBuilder();
            connectStr.DataSource = "vladp";
            connectStr.InitialCatalog = "VST";
            connectStr.IntegratedSecurity = true;
            SqlDataAdapter adapter=new SqlDataAdapter();
            SqlConnection con = new SqlConnection(connectStr.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection=con;
            con.Open();

            if (goodLevel == 0)
            {
                this.PrevBut.Visibility = Visibility.Hidden;
                cmd.CommandText = string.Format("SELECT Name AS Категория FROM {0}", tableName);
                adapter = new SqlDataAdapter(cmd); 
            }

            if (goodLevel==1)
            {
                this.PrevBut.Visibility = Visibility.Visible;
                cmd.CommandText = "SELECT DISTINCT b.Name AS Модель " +
                    "FROM Brand b " +
                    "JOIN Good g " +
                    "ON b.brand_ID=g.brand_ID " +
                    "JOIN Category c " +
                    "ON g.category_ID=c.category_ID " +
                    "WHERE c.Name=@CatName";
                cmd.Parameters.Add("@CatName", SqlDbType.NVarChar, 20).Value = addInfo;
                adapter = new SqlDataAdapter(cmd);
                catName = addInfo;
            }

            if (goodLevel == 2)
            {

                cmd.CommandText = "SELECT c.Name AS Категория, b.Name AS Марка, g.Good_Name AS Модель " +
                    "FROM Good g " +
                    "JOIN Brand b " +
                    "ON g.brand_ID=b.brand_ID " +
                    "JOIN Category c " +
                    "ON g.category_ID=c.category_ID " +
                    "WHERE c.Name=@CatName AND b.Name=@brName";
                cmd.Parameters.Add("@CatName", SqlDbType.NVarChar, 20).Value = catName;
                cmd.Parameters.Add("@brName", SqlDbType.NVarChar, 20).Value = addInfo;
                adapter = new SqlDataAdapter(cmd);
                
            }

            try
            {
                adapter.Fill(ds);
                dataGridView.ItemsSource = ds.Tables[0].DefaultView;
            }
            catch (Exception exc)
            {

                MessageBox.Show(exc.Message);
            }
                
            
            
        }

        SqlDataAdapter ChooseTable(SqlConnectionStringBuilder conStr, string tableName) 
        {
            SqlDataAdapter ad;
            switch (tableName) 
            { 
                case "Sellers":
                    return ad= new SqlDataAdapter(
                        string.Format("SELECT Name AS Имя FROM {0}",tableName), conStr.ConnectionString);
                    break;

                case "Warehouses":
                    return  ad= new SqlDataAdapter(
                        string.Format("SELECT Name AS Имя , Supplier_Cons AS Тип FROM {0}", tableName), conStr.ConnectionString);
                    break;

                case "Category":

                    categorySel = true;
                    return ad = new SqlDataAdapter(string.Format("SELECT Name FROM {0}", tableName), conStr.ConnectionString);
                    break;

                default:
                    return ad= new SqlDataAdapter(
                        string.Format("SELECT Name FROM {0}",tableName), conStr.ConnectionString);
                    break;
            }

        }

    

        private void dataGridView_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            if (categorySel)
            {
                string cellContent = ((DataRowView)dataGridView.SelectedItems[0]).Row[0].ToString();
               
               
                goodLevel++;
                switch (goodLevel)
                {
                    case 1:
                        goodTableCreate("Brand", cellContent);
                        break;
                    case 2:
                        goodTableCreate("Good", cellContent);
                        break;
                    default:
                        goodLevel = 2;
                        break;
                }

            } 
        }

        private void PrevBut_Click(object sender, RoutedEventArgs e)
        {
            goodLevel--;
            switch (goodLevel)
            {
                case 1:
                    goodTableCreate("Brand", catName);
                    break;
                case 0:
                    goodTableCreate("Category", "");
                    break;
                default:
                    goodLevel = 2;
                    break;
            }
        }
    }
}
