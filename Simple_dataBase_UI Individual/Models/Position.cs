using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_dataBase_UI_Individual.Models
{
    //  Должности(
    //      Код должности,
    //      Наименование должности,
    //      Оклад,
    //      Обязанности,
    //      Требования)
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public string Responsibilities { get; set; }
        public string Requirements { get; set; }
    }
}
