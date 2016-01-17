using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_VSTBuh.ChangeWindow
{
    /// <summary>
    /// изменение объекта базы данных VST
    /// </summary>
    interface IChangeItem
    {
        /// <summary>
        /// заготовка для метода изменения объекта
        /// </summary>
        /// <param name="ID">ид изменяемого объекта</param>
        void ApplyingChanges(int ID);
    }

    public class NonGoodChanger:IChangeItem
    {
        string dataBaseName;

    }
}
