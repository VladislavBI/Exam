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
            conStringBuilder.Pooling = true;

                App.con = new SqlConnection(conStringBuilder.ConnectionString);//выведен как отдельная переменная в APP.xaml
            
                try
                {
                    App.con.Open();
                    WaitWindow ww = new WaitWindow();
                    this.Close();
                    ww.Show();
                    
                }
                catch (SqlException)
                {
                    MessageBox.Show("Пользователь " + conStringBuilder.UserID + " несуществует или вы ввели невверный пароль");
                }

            }
        }
    }

