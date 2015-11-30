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

namespace Exam_VSTBuh.ProgStart
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            
        }

        private void ProgStartButton_Click(object sender, RoutedEventArgs e)
        {
            SqlConnectionStringBuilder conStringBuilder = new SqlConnectionStringBuilder();
            conStringBuilder.DataSource = @"vladp";
            conStringBuilder.InitialCatalog = "VST";
            conStringBuilder.UserID = UsernameTBox.Text;
            conStringBuilder.Password = PasswordTBox.Text;

            using (SqlConnection con = new SqlConnection(conStringBuilder.ConnectionString))
            {
                try
                {
                    con.Open();
                    WaitWindow ww = new WaitWindow();
                    ww.Show();
                    this.Close();
                }
                catch (SqlException exc)
                {
                    MessageBox.Show("Пользователь " + conStringBuilder.UserID + " несуществует или вы ввели невверный пароль");
                }

            }
        }
    }
}
