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
        int goodLevel = -1; //категория/модель/марка -1 - не товар
        bool categorySel;//отображение товаров
        string catName;//имя категории - для модели
        string brandName;//имя марки - для товара
        string tableName;//имя текущей таблицы
        string senderName=""; //доп инфа по отправителю (номер заказа/направление склада)
       
        DataTable dataTableSelectedGoods = new DataTable();//выбранные товары - основа для заказа
        DataTable dtNormalSelectView = new DataTable();//нормальный вид выбранных товаров - в datagridselect 
        #region Constuctors

        //для статистики
        public StatisticWindow(string Source)
        {
            InitializeComponent();
            CreateNewTable(Source);
            statWindow.Height = 500;
            RowDEfResultRow.Height = new GridLength(0);
            ButOK.Visibility = Visibility.Hidden;
           
        }

        //для продажи - товары
        public StatisticWindow(string Source, int sender)
        {
            InitializeComponent();
            CreateNewTable(Source);
            senderName = sender.ToString();

          

        }

        //для продажи - склады
        public StatisticWindow (string Source, string sender)
        {
            InitializeComponent();
            CreateNewTable(Source);
            senderName = sender;
            RowDEfResultRow.Height = new GridLength(0);
            
        }
#endregion

        #region StartCreation

        //создание новой таблицы
        void CreateNewTable(string Source) 
        {
            tableName = Source;
            NonGoodTableCreation();
        }

        //создание начальной не товарной (и категории)таблицы
        void NonGoodTableCreation()
        {
            DataSet ds = new DataSet();
            SqlConnectionStringBuilder connectStr = new SqlConnectionStringBuilder();
            connectStr.DataSource = "vladp";
            connectStr.InitialCatalog = "VST";
            connectStr.IntegratedSecurity = true;

            SqlDataAdapter adapter = ChooseTable(connectStr, tableName);
            adapter.Fill(ds);

            dataGridView.ItemsSource = ds.Tables[0].DefaultView;
        }
        //выбор начальной таблицы
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

                case "Brand":
                    return ad = new SqlDataAdapter("SELECT Name FROM Brand", conStr.ConnectionString);
                    break;

                default:
                    return ad= new SqlDataAdapter(
                        string.Format("SELECT Name FROM {0}",tableName), conStr.ConnectionString);
                    break;
            }

        }

#endregion
        //создание таблицы товара в зависимoсти от goodLevel - имя таблицы и имя категории/марки
        void goodTableCreate(string tableName, string addInfo)
        {
            this.tableName = tableName;
            //подключение
            DataSet ds = new DataSet();
            SqlConnectionStringBuilder connectStr = new SqlConnectionStringBuilder();
            connectStr.DataSource = "vladp";
            connectStr.InitialCatalog = "VST";
            connectStr.IntegratedSecurity = true;
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlConnection con = new SqlConnection(connectStr.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();

            //категория
            if (goodLevel == 0)
            {
                this.PrevBut.Visibility = Visibility.Hidden;
                cmd.CommandText = string.Format("SELECT Name AS Категория FROM {0}", tableName);
                adapter = new SqlDataAdapter(cmd);
            }
            //модель * вставляет категорию в catName
            if (goodLevel == 1)
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
            //марка
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
                brandName = addInfo;
            }
            //передача данных в dataGridView
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

        //создание таблицы для выбранных в заказ товаров 
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

        //действия с dataGridView (выбор товара и складов)
        private void dataGridView_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            try {//доделать!!!!

                //вытягивание фразы с выбраной строки для дальнейшего перехода
            DataRowView dRowView = (DataRowView)dataGridView.SelectedItems[0];
            DataRow dRow = dRowView.Row;
            string cellContent = dRow[0].ToString();
        

            //отправка имени склада в окно заказа
            if (goodLevel==-1&&senderName!="")
            {
                
                
                DelegatesData.NSWHCHandler(senderName, cellContent);
                this.Close();

            }
            else
            {
                //выбор товарной группы (категория/модель/марка)
                if (categorySel)
                {
                    
                    


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

            catch { }
        }

        //создает заказ и нормальный вид для dataGridSelect
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

        //проверяет товар на повторение (при повторении добавляет  1 к количеству)
        bool GoodIsExisted(string name)
        { 
            SqlCommand cmd = new SqlCommand("SELECT [Good_ID] FROM [VST].[dbo].[Good] WHERE [Good_Name]=@gName", App.con);
            cmd.Parameters.Add("@gName", SqlDbType.NVarChar, 20).Value = name;

            int ID = Convert.ToInt32(cmd.ExecuteScalar());
          foreach (DataRow dr in dataTableSelectedGoods.Rows)
          {
              if (Convert.ToInt32(dr["Good_ID"]) == ID) 
              {
                  int i= Convert.ToInt32(dr["Quantity"])+1;//добавляет 1 к кол-ву
                  dr["Quantity"] = (object)i;
                  return true;
              }
          }
          return false;
        }

        //заполняет таблицу с нормальным видом для DataGridSelected
        void dtNormalSelectViewCreate()
        {
            
            if (dtNormalSelectView.Rows.Count==0)
            {
                try
                {
                    dtNormalSelectView = new DataTable();
                    dtNormalSelectView.Columns.Add(new DataColumn("Имя товара"));
                    dtNormalSelectView.Columns["Имя товара"].DataType = System.Type.GetType("System.String");

                    dtNormalSelectView.Columns.Add(new DataColumn("Количество"));
                    dtNormalSelectView.Columns["Количество"].DataType = System.Type.GetType(dataTableSelectedGoods.Rows[0]["Quantity"].GetType().ToString());

                    dtNormalSelectView.Columns.Add(new DataColumn("Цена"));
                    dtNormalSelectView.Columns["Цена"].DataType = System.Type.GetType(dataTableSelectedGoods.Rows[0]["Price"].GetType().ToString());
               
                }
                catch (Exception)
                {}
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

        //на категорию назад (для товаров)
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

        //создать заказ
        private void ButOK_Click(object sender, RoutedEventArgs e)
        {
            DelegatesData.NSOLCHandler(dataTableSelectedGoods, dtNormalSelectView);
            this.Close();
        }

        //добавить товар - в новом окне
        private void AddBut_Click(object sender, RoutedEventArgs e)
        {
            //создание соответсвующих делегатов
            
                DelegatesData.RefreshGoodTableHandler = new DelegatesData.RefreshGoodTable(goodTableCreate);
                DelegatesData.RefreshNonGoodTableHandler = new DelegatesData.RefreshNonGoodTable(NonGoodTableCreation);
	            

            switch (tableName)
            {
                case "Warehouses":
                    AddingWindows.AddWHouse whAdd = new AddingWindows.AddWHouse();
                    whAdd.ShowDialog();
                    break;
                case "Brand":
                    AddingWindows.AddNonGood NGAdd = new AddingWindows.AddNonGood(tableName, catName);
                    NGAdd.ShowDialog();
                    break;
                case "Good":
                    AddingWindows.AddGood GoodAdd = new AddingWindows.AddGood(catName, brandName);
                    GoodAdd.ShowDialog();
                    break;
                default:
                    NGAdd = new AddingWindows.AddNonGood(tableName);
                    NGAdd.ShowDialog();
                    break;
            }
        }

        //удалить товар
        private void DelBut_Click(object sender, RoutedEventArgs e)
        {   //вытянуть имя удаляемой модели
           
           
            DataRowView dRowView = (DataRowView)dataGridView.SelectedItems[0];
            DataRow dRow = dRowView.Row;

            String name="";
            
            //проверка - товар/не товар
            if (goodLevel==2)//если товар
                name=dRow["Модель"].ToString();
            else//не товар
                name = dRow[0].ToString();

            //для товара - проверить есть ли такая модель в выбранных товарах
            bool isCheck = false;//товар присутсвует в dgNormalSelectView 
            int No = 0;//номер повторяющейся строки
            if (goodLevel == 2)
            {
                
                for (int i = 0; i < dtNormalSelectView.Rows.Count; i++)
                {
                    if (dtNormalSelectView.Rows[i][0].ToString() == name)
                    {
                        isCheck = true;
                        No = i;
                        break;
                    }
                }
            }

            //удалить элемент из бд
            try
            {
               
                    App.con.Open();
                    DataSet ds = new DataSet();
                    SqlCommand cmd;    
                if (goodLevel==2)//для товаров
                    cmd= new SqlCommand(string.Format("DELETE FROM {0} WHERE Good_Name=@Name", tableName), App.con);
                
                else//для категорий - создание нового подключения для вытягивания имени столбца
                {
                    //вытягивание имени столбца
                     ds = new DataSet();
                    SqlDataAdapter adapter = new SqlDataAdapter(string.Format("SELECT * FROM {0}", tableName), App.con.ConnectionString);
                    adapter.Fill(ds);
                    //команда удаления
                    cmd= new SqlCommand(string.Format("DELETE FROM {0} WHERE {1}=@Name", tableName, ds.Tables[0].Columns[1].ColumnName), App.con); 
                }

                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = name;
                    cmd.ExecuteNonQuery();
                    
               
            }
            catch (Exception exc)
            {
                App.con.Close();
                MessageBox.Show(exc.Message);
            }
            
            //обновление dataGridView - для товаров
            //имя бренда для goodTableCreate
            if (goodLevel==2)
            {
            string brandname = dRow["Марка"].ToString();
            goodTableCreate("Good", brandname);
            }
            else//для не товара
	        {
                if (goodLevel == 1)//для марки
                {
                    goodTableCreate("Brand", catName);
                }
                else
                {
                    NonGoodTableCreation();
                }
	        }
            
                //обновление datagridselect- для товара
            if (goodLevel==2)
            {
                if(No!=0)
                dataTableSelectedGoods.Rows.RemoveAt(No);
                dtNormalSelectViewCreate();
            }
            App.con.Close();
        }

        private void ChangeBut_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
