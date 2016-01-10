using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Exam_VSTBuh
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static SqlConnection con=new SqlConnection(@"Data Source=vladp;Initial Catalog=VST;Integrated Security=True");
        public static DataSet addingDataSet;

        /// <summary>
        /// Создание нового подключения к базе VST
        /// </summary>
        /// <returns>экземпляр SqlConnection, соединен - VST</returns>
        public static SqlConnection ConnectionToDBCreate()
        {
            SqlConnectionStringBuilder builder=new SqlConnectionStringBuilder();
            builder.DataSource="localhost";
            builder.InitialCatalog="VST";
            builder.IntegratedSecurity=true;
            SqlConnection con = new SqlConnection(builder.ConnectionString);
            return  con;

        }
    }
}
