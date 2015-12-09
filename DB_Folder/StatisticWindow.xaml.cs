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
        public StatisticWindow()
        {
            InitializeComponent();

            DataSet ds = new DataSet();
            SqlConnectionStringBuilder connectStr = new SqlConnectionStringBuilder();
            connectStr.DataSource = "vladp";
            connectStr.InitialCatalog = "VST";
            connectStr.IntegratedSecurity = true;
            
            string commandStr=@"SELECT * FROM Sellers";

            SqlDataAdapter adapter = new SqlDataAdapter(commandStr, connectStr.ConnectionString);
            adapter.Fill(ds);

            dataGridView.DataSource = ds.Tables[0];
        }
    }
}
