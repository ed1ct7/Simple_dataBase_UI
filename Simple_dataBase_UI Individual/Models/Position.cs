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
        Position() { }
        public Position(
                int Id, string Name,
                decimal Salary, string Duties,
                string Requirements
            ) { 
            this.Id = Id;
            this.Name = Name;
            this.Salary = Salary;
            this.Duties = Duties;
            this.Requirements = Requirements;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public string Duties { get; set; }
        public string Requirements { get; set; }
    }
}
