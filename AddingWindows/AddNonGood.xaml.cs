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

namespace Exam_VSTBuh.AddingWindows
{
    /// <summary>
    /// Interaction logic for AddNonGood.xaml
    /// </summary>
    public partial class AddNonGood : Window
    {
        SqlConnection con;
        string categoryName = "";//для создания новой марки
        string tableName;//имя таблицы, которую изменяют
        public AddNonGood(string Source)
        {
            InitializeComponent();
            tableName = Source;
            CreateWindowName();
        }
        public AddNonGood(string Source, string catName)
        {
            InitializeComponent();
            tableName = Source;
            categoryName = catName;
            CreateWindowName();
        }

        /// <summary>
        /// Создание окна для конкретной таблицы
        /// </summary>
        void CreateWindowName()
        {
            //выбор таблицы для заполнения
            switch (tableName)
            {
                case "Brand":
                    
                    TBlockGeneralInfo.Text = "Добавить марку";
                    break;
                case "Category":
                    TBlockGeneralInfo.Text = "Добавить категорию";
                    break;
                case "Sellers":
                    TBlockGeneralInfo.Text = "Добавить контрагента";
                    break;
                default:
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (TBoxName.Text == "")
            {
                MessageBox.Show("Имя не может быть пустым!!!!");
            }

                //добавление нового склада в бд 
            else
            {

                try
                {
                    //создание команды удаления и её выполнение
                    using (con = new SqlConnection(@"Data Source=vladp; Initial Catalog=VST; Integrated Security=True"))
                    {

                        con.Open();
                        SqlCommand cmd = new SqlCommand(string.Format("INSERT INTO {0} ([Name]) VALUES (@Name)", tableName), con);
                        cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = TBoxName.Text;
                        cmd.ExecuteNonQuery();
                        if (categoryName == "")
                        {
                            DelegatesData.RefreshNonGoodTableHandler();
                        }
                        else
                        {
                            DelegatesData.RefreshGoodTableHandler(tableName, categoryName);
                        }
                    }
                }
                catch (Exception exc)
                {
                    con.Close();
                }
                if (DelegatesData.RefreshComboBoxesHandler != null)
                    DelegatesData.RefreshComboBoxesHandler();
            }
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
