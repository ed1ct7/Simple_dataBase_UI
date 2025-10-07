using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_dataBase_UI_Individual.Models
{
    //  Услуги(
    //      Код услуги,
    //      Наименование,
    //      Описание,
    //      Стоимость)
    public class Service
    {
        public Service() { }
        public Service(int Id, string Name,
            string Description, int Price
            ) { 
            this.Id = Id;
            this.Name = Name;
            this.Description = Description;
            this.Price = Price;
        }
        public Service(List<object> array)
        {
            if (array == null || array.Count < 4)
                throw new ArgumentException("Array must contain at least 4 elements");

            this.Id = Convert.ToInt32(array[0]);
            this.Name = array[1]?.ToString() ?? string.Empty;
            this.Description = array[2]?.ToString() ?? string.Empty;
            this.Price = Convert.ToInt32(array[3]);
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
