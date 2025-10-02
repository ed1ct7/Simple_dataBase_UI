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
        Service() { }
        public Service(int Id, string Name,
            string Description, int Price
            ) { 
            this.Id = Id;
            this.Name = Name;
            this.Description = Description;
            this.Price = Price;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
    }
}
