using Exam_VSTBuh.AddingWindows;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for ChangeGood.xaml
    /// </summary>
    public partial class ChangeGood : Window
    {
        /// <summary>
        /// ИД изменяемого объекта
        /// </summary>
        int changingItemID;
        /// <summary>
        /// Информация об объекте до изменений
        /// </summary>
        DataTable objectBeforeChangesInfo = new DataTable();
        public ChangeGood(int ID, string catName, string brandName)
        {
            InitializeComponent();
            changingItemID = ID;

            //создание начальных значений для списков
            ComboBoxCategory.Items.Add(catName);
            ComboBoxCategory.SelectedIndex = 0;
            ComboBoxBrand.Items.Add(brandName);
            ComboBoxBrand.SelectedIndex = 0;

            GetObjectsInfo();
            CreateWindows();//заполнение списков категория и марка
            //создание делегата для обновления списков
            DelegatesData.RefreshComboBoxesHandler=new DelegatesData.RefreshComboBoxes(CreateWindows);
        }

        #region Заполнение окна
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
        

        /// <summary>
        /// получаем информацию о старом объекте, забиваем её в окно изменения
        /// </summary>
        void GetObjectsInfo()
        {
            //получение информации
            using (SqlConnection con = App.ConnectionToDBCreate())
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(string.Format("SELECT Good_Name, Price FROM Good WHERE Good_ID=@gID"), con);
                    cmd.Parameters.Add("@gId", SqlDbType.Int).Value = changingItemID;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(objectBeforeChangesInfo);

                    //заполнение значений имя и цена для изменяемого товара
                    TextBoxGoodName.Text = objectBeforeChangesInfo.Rows[0]["Good_Name"].ToString();
                    TextBoxGoodPrice.Text = objectBeforeChangesInfo.Rows[0]["Price"].ToString();
                }
                catch (Exception exc)
                {

                    MessageBox.Show(exc.Message);

                }

            }


        }
        #endregion

        /// <summary>
        /// команда изменения выбранного товара
        /// </summary>
        void ChangeItem()
        {
               if(TextBoxGoodName.Text==""||TextBoxGoodPrice.Text=="")
            {
                MessageBox.Show("Имя и цена не может быть пустым");
            }

            else 
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=vladp;Initial Catalog=VST;Integrated Security=True"))
                {
                     con.Open();
                    try
                    {

                        //получение ИД выбранной марки и категории
                       
                        SqlCommand cmd = new SqlCommand("SELECT category_ID FROM Category WHERE Name=@cName", con);
                        cmd.Parameters.Add("@cName", SqlDbType.NVarChar, 50).Value = ComboBoxCategory.SelectedValue;
                        int catID = Convert.ToInt32(cmd.ExecuteScalar());

                        cmd = new SqlCommand("SELECT brand_ID FROM Brand WHERE Name=@bName", con);
                        cmd.Parameters.Add("@bName", SqlDbType.NVarChar, 50).Value = ComboBoxBrand.SelectedValue;
                        int brandID = Convert.ToInt32(cmd.ExecuteScalar()); 

                    
                       
                        //создание команды для изменения товара
                        SqlCommand goodUpdateCmd = new SqlCommand("UPDATE [dbo].[Good] SET [category_ID]=@catID ,[Good_Name]=@gName ,[Price]=@Price ,[brand_ID]=@brandID WHERE Good_ID=@gID", con);
                        goodUpdateCmd.Parameters.Add("@catID", SqlDbType.Int).Value = catID;
                        goodUpdateCmd.Parameters.Add("@gName", SqlDbType.NVarChar, 50).Value = TextBoxGoodName.Text;
                        goodUpdateCmd.Parameters.Add("@Price", SqlDbType.Money).Value = Convert.ToDecimal(TextBoxGoodPrice.Text);
                        goodUpdateCmd.Parameters.Add("@brandID", SqlDbType.Int).Value =brandID;
                        goodUpdateCmd.Parameters.Add("@gID", SqlDbType.Int).Value=changingItemID;
                        cmd.ExecuteNonQuery();

                        goodUpdateCmd.ExecuteNonQuery();
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.Message);
                    }  
                }
            }

        }

        private void ButAdd_Click(object sender, RoutedEventArgs e)
        {
            ChangeItem();
            this.Close();
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

        private void TextBoxGoodPrice_LostFocus(dynamic sender, RoutedEventArgs e)
        {
            Regex reg = new Regex(@"[^0-9]");
            if (reg.IsMatch(sender.Text))
            {
                MessageBox.Show("Поле может содержать только цифры!");
                sender.Text = "";
            }
        }

        private void ButCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }


}
