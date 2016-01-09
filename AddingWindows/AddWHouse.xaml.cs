using System;
using System.Collections.Generic;
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
using System.Data;

namespace Exam_VSTBuh.AddingWindows
{
    /// <summary>
    /// Interaction logic for AddWHouse.xaml
    /// </summary>
    public partial class AddWHouse : Window
    {
        SqlConnection con;
        public AddWHouse()
        {
            InitializeComponent();
            CreateWHWindow();
        }

        ///<summary>
        ///создание окна под склады - изменение TBlockAddInfo и ComboBoxAddInfo пол тип склада
        ///</summary>
        void CreateWHWindow()
        {
            //изменение textBlock
            TBlockAddInfo.Text = "Тип склада";

            //Тип склада, добавлять сюда, в проследствии создать отдельную бд
            ComboBoxAddInfo.Items.Add("Склад");
            ComboBoxAddInfo.Items.Add("Поставщик");
            ComboBoxAddInfo.Items.Add("Покупатель");
            ComboBoxAddInfo.SelectedIndex = 0;
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
                        
                        SqlCommand cmd = new SqlCommand(string.Format("INSERT INTO [dbo].[Warehouses] ([Name],[Supplier_Cons]) VALUES (@Name, @Type)"), con);
                        cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = TBoxName.Text;
                        cmd.Parameters.Add("@Type", SqlDbType.NVarChar, 50).Value = ComboBoxAddInfo.SelectedValue.ToString();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            ComboBoxAddInfo.Items.Add(reader[2]);
                        }
                        DelegatesData.RefreshNonGoodTableHandler();
                       
                    }
                }
                catch (Exception exc)
                {
                    con.Close();
                    MessageBox.Show(exc.Message);
                } 
            }
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
