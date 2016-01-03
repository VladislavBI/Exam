using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_VSTBuh
{
    public class DelegatesData
    {
        //saleReport.NewSale Warehouse change delegate
        public delegate void newSaleWHChange(string tbName, string data);
        public static newSaleWHChange NSWHCHandler;

        //saleReport.NewSale OrderList change delegate
        public delegate void newSaleOrderlistChange(DataTable Full, DataTable View);
        public static newSaleOrderlistChange NSOLCHandler;
    }
}
