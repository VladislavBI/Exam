using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace Exam_VSTBuh.ChangeWindow
{
    /// <summary>
    /// Interaction logic for ChangeNonGood.xaml
    /// </summary>
    public partial class ChangeNonGood : Window
    {
        
        /// <summary>
        /// имя изменяемой БД
        /// </summary>
        string dataBaseName;
        /// <summary>
        /// ИД изменяемого объекта
        /// </summary>
        int changingItemID;
        /// <summary>
        /// Информация об объекте до изменений
        /// </summary>
        DataTable objectBeforeChangesInfo=new DataTable();

        public ChangeNonGood(string DBName, int ID)
        {
            InitializeComponent();

            dataBaseName = DBName;
            changingItemID = ID;

            

            textBlockGeneralInfo.Text = "Изменить " + App.engToRusDBNameTranslator[dataBaseName] + ": ";

            GetObjectsInfo();
            
        }


       /// <summary>
       /// получаем информацию о старом объекте, забиваем её в окно измененич
       /// </summary>
        void GetObjectsInfo()
        {
            //получение информации
            using(SqlConnection con = App.ConnectionToDBCreate())
            {
                try 
	            {	        
		        con.Open();
                SqlCommand cmd=new SqlCommand(string.Format("SELECT Name FROM {0} WHERE {1}={2}", dataBaseName, App.getIDPool[dataBaseName], changingItemID), con);
                
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(objectBeforeChangesInfo);
                
                TextBoxName.Text += objectBeforeChangesInfo.Rows[0][0].ToString();
	            }
	            catch (Exception exc)
	            {
		
		            MessageBox.Show(exc.Message);
                    
	            }
                
            }

            
        }

        /// <summary>
        /// команда изменения объекта в выбраной таблице БД VST
        /// </summary>
        void ChangeItem()
        {
            if (TextBoxName.Text == "")
            {
                MessageBox.Show("Имя не может быть пустым!!!!");
            }

               //добавление нового склада в бд 
            else
            {
                using (SqlConnection con = App.ConnectionToDBCreate())
                {
                    try
                    {
                        con.Open();
                        objectBeforeChangesInfo.Rows[0][0] = TextBoxName.Text;
                        SqlCommand cmd = new SqlCommand(string.Format("UPDATE {0} SET Name='{1}' WHERE {2}={3}", dataBaseName, TextBoxName.Text, App.getIDPool[dataBaseName], changingItemID), con);
                        cmd.ExecuteNonQuery();

                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.Message);
                    }

                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChangeItem();
            DelegatesData.RefreshNonGoodTableHandler();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
