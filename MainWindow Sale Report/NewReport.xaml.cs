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
using System.Data;
using System.Data.SqlClient;


namespace Exam_VSTBuh.MainWindow_Sale_Report
{
    /// <summary>
    /// Interaction logic for NewReport.xaml
    /// </summary>
    public partial class NewReport : Window
    {
        /// <summary>
        /// Текущая таблица с продажами для DataGrid
        /// </summary>
        DataTable curReportTable = new DataTable();

        /// <summary>
        /// заполнение начальных окон: даты, курсов, списка продаж, списка продавцов
        /// </summary>
        /// <param name="Euro">курс евро</param>
        /// <param name="Dollar">курс доллар</param>
        public NewReport(string Euro, string Dollar)
        {
            InitializeComponent();
            //заполнение даты и курсов
            textBlockHeader.Text = DateTime.Today.ToShortDateString() + " $: " + Dollar + " €: " + Euro;
            
            //заполнение списка продаж 
            curReportTable=CreateSalesTable();
            DataGridReport.ItemsSource = curReportTable.DefaultView;
        }


        
        /// <summary>
        /// Полное создание таблицы сегодняшних продаж
        /// </summary>
        /// <returns>DataTable с полнной инфой по продажам</returns>
        DataTable CreateSalesTable()
        {
            //создание подключения
            SqlConnection con = App.ConnectionToDBCreate();
	        con.Open();
                
           //заполнение таблицы заказов
           DataTable reportTable=CreateNonBoolReportTable(con);
           AddingDebtAndInet(reportTable, con);
           FillingSellersAndWarhouses(con);
           ReportSumCounting(reportTable);
                
           con.Close();     
	        
           return reportTable;
        }

        #region Создание базового отчета
        /// <summary>
        /// Создает таблицу для сегодняшнего! отчета - товар, кол-во, цена, сумма, продавец - без инета и долга
        /// </summary>
        /// <param name="connect">активное! подключение к базе данных</param>
        /// <returns>таблицу с инфой по текущему дню</returns>
        DataTable CreateNonBoolReportTable(SqlConnection connect)
        {
            //команда для выборки данных
            SqlCommand cmd = new SqlCommand("SELECT [VST].[dbo].[Order].Order_ID, Good.Good_Name, Order_Detail.Quantity, Order_Detail.Price, Order_Detail.Quantity*Order_Detail.Price AS Sum, Sellers.Name, Warehouses.Name" +
                                           " FROM [VST].[dbo].[Order]" +
                                           " JOIN Order_Detail" +
                                           " ON [VST].[dbo].[Order].Order_ID=Order_Detail.Order_ID" +
                                           " JOIN Good" +
                                           " ON Order_Detail.Good_ID=Good.Good_ID" +
                                           " Join Sellers" +
                                           " ON [VST].[dbo].[Order].Seller_ID=Sellers.Seller_ID" +
                                           " Join Warehouses"+
										   " ON [VST].[dbo].[Order].Warehouse_ID=Warehouses.Warehouse_ID"+
                                           " WHERE [VST].[dbo].[Order].Date=@date", connect);
            cmd.Parameters.Add("@date", SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();

            DataTable tempTable = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(tempTable);

            return tempTable;
        }

        /// <summary>
        /// добаление интернета и долга в сегодняшний! отчет
        /// </summary>
        /// <param name="incomeTable">таблица для добавления</param>
        /// <param name="con">активное! подключение к базе данных</param>
        void AddingDebtAndInet(DataTable incomeTable, SqlConnection con)
        {
            //добавление столбцов для долга и инета
            DataColumn dcDebt=new DataColumn("Долг");
            incomeTable.Columns.Add(dcDebt);
            DataColumn dcInet=new DataColumn("Интернет");
            incomeTable.Columns.Add(dcInet);

            //вытаскивание данных по инету и долгу
            SqlCommand cmd = new SqlCommand("SELECT [VST].[dbo].[Order].Order_ID, [VST].[dbo].[Order].Debt, [VST].[dbo].[Order].Internet FROM [VST].[dbo].[Order] WHERE Date=@date", con);
            cmd.Parameters.Add("@date", SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
            SqlDataReader reader = cmd.ExecuteReader();

            //номер текущей строки - для перебора
            int currentRowNumber = 0;
            //заполнение новых столбцов
            while (reader.Read())
            {
                //создание значений для таблицы
                string inet=IntToBoolstring(reader[1]);
                string debt=IntToBoolstring(reader[2]);

                
                //заполнение значений таблицы для конкретного order_ID
                while (reader[0].ToString() == incomeTable.Rows[currentRowNumber][0].ToString())
                {
                incomeTable.Rows[currentRowNumber][dcDebt] = debt;
                incomeTable.Rows[currentRowNumber][dcInet] = inet;


                //проверка на выход из диапозона значений
                if (currentRowNumber+1<incomeTable.Rows.Count)
                {
                    currentRowNumber++;    
                }
                else
                {
                    break;
                }

                }

               
            }
            reader.Close();
        }

        /// <summary>
        /// имитация bool - конвертация результата из int в string 
        /// (1="+"; ="-")  
        /// </summary>
        /// <param name="recieveValue">принятое значение</param>
        /// <returns>результат имитации (+ или -)</returns>
        string IntToBoolstring(object recieveValue)
        {
            if (Convert.ToInt32(recieveValue) == 0)
                return "-";
            else
                return "+";
        }

        /// <summary>
        /// заполняет списки продавцов и складов
        /// </summary>
        void FillingSellersAndWarhouses(SqlConnection con)
        {
            //создание значений для всех складов, продавцов
            comboBoxSeller.Items.Clear();
            comboBoxWarehouses.Items.Clear();
            comboBoxSeller.Items.Add("Все");
            comboBoxWarehouses.Items.Add("Все");


            //запрос для получения всех продавцов
            SqlCommand cmd=new SqlCommand("SELECT Name FROM Sellers; SELECT Name FROM Warehouses",con);
            SqlDataReader reader=cmd.ExecuteReader();

            //заполнение списков складов и продавцов
            while (reader.Read())
            {
                comboBoxSeller.Items.Add(reader[0]); 
            }
            reader.NextResult();
            while (reader.Read())
            {
                 comboBoxWarehouses.Items.Add(reader[0]);
            }

            comboBoxSeller.SelectedIndex=0;
            comboBoxWarehouses.SelectedIndex = 0;

            reader.Close();
        }

        /// <summary>
        /// подсчет суммы отчета
        /// </summary>
        /// <param name="reportTable">Таблица с данными (должна иметь поле Sum)</param>
        void ReportSumCounting(DataTable reportTable)
        {
            //сумма всех продаж
            int Sum = 0;

            //подсчет суммы
            for (int i = 0; i < reportTable.Rows.Count; i++)
            {
                Sum += Convert.ToInt32(reportTable.Rows[i]["Sum"]);  
            }
            TextBlockSum.Text = Sum.ToString();
        }
        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataTable temp = curReportTable;
            //выполнение поочередно всех критериев поиска
            try
            {
               
                if(comboBoxSeller.SelectedValue.ToString()!="Все")
            {
                temp = SearchCriteriaUse(temp, "Name", comboBoxSeller.SelectedValue.ToString());
            }
            if (comboBoxWarehouses.SelectedValue.ToString() != "Все")
            {
                temp = SearchCriteriaUse(temp, "Name1", comboBoxWarehouses.SelectedValue.ToString());
            }
            if (expanderAdditionalSearchInfo.IsExpanded==true)
            {
                if(checkBoxDebt.IsChecked==true)
                {
                    temp = SearchCriteriaUse(temp, "Долг", "+");
                }
                else
                {
                    temp = SearchCriteriaUse(temp, "Долг", "-");
                }

                if (checkBoxInet.IsChecked == true)
                {
                    temp = SearchCriteriaUse(temp, "Интернет", "+");
                }
                else
                {
                    temp = SearchCriteriaUse(temp, "Интернет", "-");
                }
            }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

            //изменение отчета
            DataGridReport.ItemsSource = temp.DefaultView;
            ReportSumCounting(temp);
        }

        /// <summary>
        /// применение одного критерия поиска
        /// </summary>
        /// <param name="incomeTable">исходная таблица</param>
        /// <param name="searchCriteria">критерий для поиска</param>
        /// <param name="searchString">фраза, которую ищут</param>
        /// <returns>таблицу с примененным критерием поиска</returns>
        DataTable SearchCriteriaUse(DataTable incomeTable, string searchCriteria, string searchString)
        {
            DataTable tmp = incomeTable.Clone();
            
            for (int i = 0; i < incomeTable.Rows.Count; i++)
            {
                if (incomeTable.Rows[i][searchCriteria].ToString() == searchString)
                {
                    DataRow dr = incomeTable.Rows[i];
                    tmp.ImportRow(dr);
                }
                    
            }
            return tmp;
        }
    }
}
