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
        int goodLevel = -1;
        bool categorySel;
        string catName;
        string senderName="";
        DataTable dataTableSelectedGoods = new DataTable();
        DataTable dtNormalSelectView = new DataTable();
        #region Constuctors
        public StatisticWindow(string Source)
        {
            InitializeComponent();
            CreateNewTable(Source);
            statWindow.Height = 500;
            
           
        }

        public StatisticWindow(string Source, int sender)
        {
            InitializeComponent();
            CreateNewTable(Source);
            senderName = sender.ToString();


        }

        public StatisticWindow (string Source, string sender)
        {
            InitializeComponent();
            CreateNewTable(Source);
            senderName = sender;
            
            
        }
#endregion
        void CreateNewTable(string Source) 
        {
            DataSet ds = new DataSet();
            SqlConnectionStringBuilder connectStr = new SqlConnectionStringBuilder();
            connectStr.DataSource = "vladp";
            connectStr.InitialCatalog = "VST";
            connectStr.IntegratedSecurity = true;

            SqlDataAdapter adapter = ChooseTable(connectStr, Source);
            adapter.Fill(ds);

            dataGridView.ItemsSource = ds.Tables[0].DefaultView;
        }

        void goodTableCreate(string tableName, string addInfo)
        {
            
            DataSet ds = new DataSet();
            SqlConnectionStringBuilder connectStr = new SqlConnectionStringBuilder();
            connectStr.DataSource = "vladp";
            connectStr.InitialCatalog = "VST";
            connectStr.IntegratedSecurity = true;
            SqlDataAdapter adapter=new SqlDataAdapter();
            SqlConnection con = new SqlConnection(connectStr.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection=con;
            con.Open();

            if (goodLevel == 0)
            {
                this.PrevBut.Visibility = Visibility.Hidden;
                cmd.CommandText = string.Format("SELECT Name AS Категория FROM {0}", tableName);
                adapter = new SqlDataAdapter(cmd); 
            }

            if (goodLevel==1)
            {
                this.PrevBut.Visibility = Visibility.Visible;
                cmd.CommandText = "SELECT DISTINCT b.Name AS Модель " +
                    "FROM Brand b " +
                    "JOIN Good g " +
                    "ON b.brand_ID=g.brand_ID " +
                    "JOIN Category c " +
                    "ON g.category_ID=c.category_ID " +
                    "WHERE c.Name=@CatName";
                cmd.Parameters.Add("@CatName", SqlDbType.NVarChar, 20).Value = addInfo;
                adapter = new SqlDataAdapter(cmd);
                catName = addInfo;
            }

            if (goodLevel == 2)
            {

                cmd.CommandText = "SELECT c.Name AS Категория, b.Name AS Марка, g.Good_Name AS Модель " +
                    "FROM Good g " +
                    "JOIN Brand b " +
                    "ON g.brand_ID=b.brand_ID " +
                    "JOIN Category c " +
                    "ON g.category_ID=c.category_ID " +
                    "WHERE c.Name=@CatName AND b.Name=@brName";
                cmd.Parameters.Add("@CatName", SqlDbType.NVarChar, 20).Value = catName;
                cmd.Parameters.Add("@brName", SqlDbType.NVarChar, 20).Value = addInfo;
                adapter = new SqlDataAdapter(cmd);
                
            }

            try
            {
                adapter.Fill(ds);
                dataGridView.ItemsSource = ds.Tables[0].DefaultView;
            }
            catch (Exception exc)
            {

                MessageBox.Show(exc.Message);
            }
                
            
            
        }

        SqlDataAdapter ChooseTable(SqlConnectionStringBuilder conStr, string tableName) 
        {
            SqlDataAdapter ad;
            switch (tableName) 
            { 
                case "Sellers":
                    return ad= new SqlDataAdapter(
                        string.Format("SELECT Name AS Имя FROM {0}",tableName), conStr.ConnectionString);
                    break;

                case "Warehouses":
                    return  ad= new SqlDataAdapter(
                        string.Format("SELECT Name AS Имя , Supplier_Cons AS Тип FROM {0}", tableName), conStr.ConnectionString);
                    break;

                case "Category":
                    goodLevel = 0;
                    categorySel = true;
                    CreatedataTableSelectedGoods();
                    
                    return ad = new SqlDataAdapter(string.Format("SELECT Name FROM {0}", tableName), conStr.ConnectionString);
                    break;

                default:
                    return ad= new SqlDataAdapter(
                        string.Format("SELECT Name FROM {0}",tableName), conStr.ConnectionString);
                    break;
            }

        }

    
        void CreatedataTableSelectedGoods()
        {
            App.con = new SqlConnection(@"Data Source=localhost;Initial Catalog=VST;Integrated Security=True");
            App.con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Order_Detail", App.con);
            SqlDataReader reader = cmd.ExecuteReader();
            dataTableSelectedGoods.Load(reader);
            dataTableSelectedGoods.Clear();
            App.con.Close();
        }
        private void dataGridView_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            DataRowView dRowView = (DataRowView)dataGridView.SelectedItems[0];
            DataRow dRow = dRowView.Row;

            if (goodLevel==-1&&senderName!="")
            {
                
                string wareHouseName = dRow[0].ToString();
                DelegatesData.NSWHCHandler(senderName, wareHouseName);
                this.Close();

            }
            else
            {
                if (categorySel)
                {
                    string cellContent = ((DataRowView)dataGridView.SelectedItems[0]).Row[0].ToString();


                    goodLevel++;
                    switch (goodLevel)
                    {
                        case 1:
                            goodTableCreate("Brand", cellContent);
                            break;
                        case 2:
                            goodTableCreate("Good", cellContent);

                            break;
                        default:
                            goodLevel = 2;
                            CreateOrderListView(dRow[2].ToString());
                            break;
                    }

                } 
            }
            
        }

        void CreateOrderListView(string goodName)
        {
            App.con.Open();
            if (!GoodIsExisted(goodName))
            {
                DataRow drow = dataTableSelectedGoods.NewRow();
                drow["Order_ID"] = Convert.ToInt32(senderName);
                

                InfoFromGood(drow, goodName);

                drow["Quantity"] = 1;


                
                dataTableSelectedGoods.Rows.Add(drow);
            }
            dtNormalSelectViewCreate();
            App.con.Close();
            dataGridSelect.ItemsSource = dtNormalSelectView.DefaultView;
        }


        #region OrderDetailsCreate 
        //заполняет ID и цену товара в DataGridSelect
        void InfoFromGood (DataRow drow, string name)
        {
            SqlCommand cmd = new SqlCommand("SELECT [Good_ID] ,[Price] FROM [VST].[dbo].[Good] WHERE [Good_Name]=@gName", App.con);
            cmd.Parameters.Add("@gName", SqlDbType.NVarChar, 20).Value = name;

            SqlDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
               

          
            try
            {
                drow["Good_ID"] = reader[0];
                drow["Price"] = reader[1];
            }
            catch (Exception ezc)
            {

                MessageBox.Show(ezc.Message);
            }
            }
            reader.Close();
            
        }

        //проверяет товар на повторение
        bool GoodIsExisted(string name)
        { 
            SqlCommand cmd = new SqlCommand("SELECT [Good_ID] FROM [VST].[dbo].[Good] WHERE [Good_Name]=@gName", App.con);
            cmd.Parameters.Add("@gName", SqlDbType.NVarChar, 20).Value = name;

            int ID = Convert.ToInt32(cmd.ExecuteScalar());
          foreach (DataRow dr in dataTableSelectedGoods.Rows)
          {
              if (Convert.ToInt32(dr["Good_ID"]) == ID) 
              {
                  int i= Convert.ToInt32(dr["Quantity"])+1;
                  dr["Quantity"] = (object)i;
                  return true;
              }
          }
          return false;
        }

        //заполняет таблица с нормальным видом для DataGridSelected
        void dtNormalSelectViewCreate()
        {
            
            if (dtNormalSelectView.Rows.Count==0)
            {
                dtNormalSelectView = new DataTable();
                dtNormalSelectView.Columns.Add(new DataColumn("Имя товара"));
                dtNormalSelectView.Columns["Имя товара"].DataType = System.Type.GetType("System.String");

                dtNormalSelectView.Columns.Add(new DataColumn("Количество"));
                dtNormalSelectView.Columns["Количество"].DataType = System.Type.GetType(dataTableSelectedGoods.Rows[0]["Quantity"].GetType().ToString());

                dtNormalSelectView.Columns.Add(new DataColumn("Цена"));
                dtNormalSelectView.Columns["Цена"].DataType = System.Type.GetType(dataTableSelectedGoods.Rows[0]["Price"].GetType().ToString());
            }
            else
            {
                dtNormalSelectView.Clear();
            }
            

            foreach (DataRow temp in dataTableSelectedGoods.Rows)
            {
                DataRow dr = dtNormalSelectView.NewRow();

                SqlCommand cmd = new SqlCommand("SELECT [Good_Name] FROM [VST].[dbo].[Good] WHERE [Good_ID]=@gName", App.con);
                cmd.Parameters.Add("@gName", SqlDbType.Int).Value = Convert.ToInt32(temp["Good_ID"]);
                string s=(string)cmd.ExecuteScalar();
                dr["Имя товара"] = (object)s;

                dr["Количество"] = temp["Quantity"];
                dr["Цена"] = temp["Price"];
                dtNormalSelectView.Rows.Add(dr);
            }
        }

#endregion
        private void PrevBut_Click(object sender, RoutedEventArgs e)
        {
            goodLevel--;
            switch (goodLevel)
            {
                case 1:
                    goodTableCreate("Brand", catName);
                    break;
                case 0:
                    goodTableCreate("Category", "");
                    break;
                default:
                    goodLevel = 2;
                    break;
            }
        }

        private void ButOK_Click(object sender, RoutedEventArgs e)
        {
            DelegatesData.NSOLCHandler(dataTableSelectedGoods, dtNormalSelectView);
            this.Close();
        }
    }
}
