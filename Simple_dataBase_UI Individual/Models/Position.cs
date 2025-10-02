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
        public Position() { }
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
        public Position(List<object> array)
        {
            if (array == null || array.Count < 5)
                throw new ArgumentException("Array must contain at least 5 elements");

            this.Id = Convert.ToInt32(array[0]);
            this.Name = array[1]?.ToString() ?? string.Empty;
            this.Salary = Convert.ToDecimal(array[2]);
            this.Duties = array[3]?.ToString() ?? string.Empty;
            this.Requirements = array[4]?.ToString() ?? string.Empty;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public string Duties { get; set; }
        public string Requirements { get; set; }
    }
}
