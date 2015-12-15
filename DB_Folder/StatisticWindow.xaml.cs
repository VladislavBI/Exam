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

        SqlDataAdapter ChooseTable(SqlConnectionStringBuilder conStr, string tableName) 
        {
            SqlDataAdapter ad;
            switch (tableName) 
            { 
                case "Sellers":
                    return ad= new SqlDataAdapter(
                        string.Format("SELECT Name FROM {0}",tableName), conStr.ConnectionString);
                    break;

                case "Warehouses":
                    return  ad= new SqlDataAdapter(
                        string.Format("SELECT Name, Supplier_Cons FROM {0}",tableName), conStr.ConnectionString);
                    break;

                case "Category":
                    this.PrevBut.Visibility=Visibility.Visible;
                    this.dataGridView.MouseDoubleClick+=dataGridView_MouseDoubleClick;
                    return ad= new SqlDataAdapter(string.Format("SELECT Name FROM {0}",tableName), conStr.ConnectionString);
                    break;

                default:
                    return ad= new SqlDataAdapter(
                        string.Format("SELECT Name FROM {0}",tableName), conStr.ConnectionString);
                    break;
            }

        }

        void dataGridView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
