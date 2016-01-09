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
using System.Text.RegularExpressions;
using System.Data;

namespace Exam_VSTBuh.AddingWindows
{
    /// <summary>
    /// Interaction logic for AddGood.xaml
    /// </summary>
    public partial class AddGood : Window
    {
        public AddGood(string catName, string brandName)
        {
            InitializeComponent();

            //создание начальных значений для списков
            ComboBoxCategory.Items.Add(catName);
            ComboBoxCategory.SelectedIndex = 0;
            ComboBoxBrand.Items.Add(brandName);
            ComboBoxBrand.SelectedIndex = 0;

            
            CreateWindows();//заполнение списков категория и марка

            //создание делегата для обновления списков
            DelegatesData.RefreshComboBoxesHandler=new DelegatesData.RefreshComboBoxes(CreateWindows);
        }

        /// <summary>
        /// заполнение списков категория и марка
        /// </summary>
        
        void CreateWindows()
        {
            //проверка на переполненость списков
            if (ComboBoxCategory.Items.Count > 1)
                ComboBoxCategory.Items.Clear();
            if (ComboBoxBrand.Items.Count > 1)
                ComboBoxBrand.Items.Clear();

            //создание подключения, ридера
            using (SqlConnection con = new SqlConnection(@"Data Source=vladp;Initial Catalog=VST;Integrated Security=True"))
            {
                
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT Name FROM Category; SELECT Name FROM Brand", con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    
                    
              
                
                //заполнение списков категории и марки
                while (reader.Read())
                {
                  
                        ComboBoxCategory.Items.Add(reader[0].ToString());
                }
                reader.NextResult();
                while (reader.Read())
                {
                        ComboBoxBrand.Items.Add(reader[0].ToString());
                }
                

                ComboBoxCategory.SelectedIndex = 0;
                ComboBoxBrand.SelectedIndex = 0;
                con.Close();
            }
        }

        private void ButNewCat_Click(object sender, RoutedEventArgs e)
        {
            AddNonGood ANGood = new AddNonGood("Category");
            ANGood.ShowDialog();
        }

        private void ButNewBrand_Click(object sender, RoutedEventArgs e)
        {
            AddNonGood ANGood = new AddNonGood("Brand");
            ANGood.Show();
        }

        private void ButAdd_Click(object sender, RoutedEventArgs e)
        {
            if(TBoxGoodName.Text==""||TBoxGoodPrice.Text=="")
            {
                MessageBox.Show("Имя и цена не может быть пустым");
            }

            else 
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=vladp;Initial Catalog=VST;Integrated Security=True"))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT category_ID FROM Category WHERE Name=@Name", con);
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = ComboBoxCategory.SelectedValue;
                    int catID = Convert.ToInt32(cmd.ExecuteScalar()) + 1;

                    cmd = new SqlCommand("SELECT brand_ID FROM Brand WHERE Name=@Name", con);
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = ComboBoxCategory.SelectedValue;
                    int brandID = Convert.ToInt32(cmd.ExecuteScalar()) + 1; 

                    
                    SqlCommand goodInsertCmd = new SqlCommand("INSERT INTO [dbo].[Good] ([category_ID],[Good_Name],[Price],[brand_ID]) VALUES(@catID, @gName, @Price, @brandID)", con);
                    goodInsertCmd.Parameters.Add("@catID", SqlDbType.Int).Value = brandID;
                    goodInsertCmd.Parameters.Add("@gName", SqlDbType.NVarChar, 50).Value = TBoxGoodName.Text;
                    goodInsertCmd.Parameters.Add("@Price", SqlDbType.Money).Value = Convert.ToDecimal(TBoxGoodPrice.Text);
                    goodInsertCmd.Parameters.Add("@brandID", SqlDbType.Int).Value =catID;

                    goodInsertCmd.ExecuteNonQuery();
                }
            }
        }

        private void TBoxGoodPrice_LostFocus(dynamic sender, RoutedEventArgs e)
        {
            Regex reg = new Regex(@"[^0-9]");
            if (reg.IsMatch(sender.Text))
            {
                MessageBox.Show("Поле может содержать только цифры!");
                sender.Text = "";
            }
        }
    
    }
}
