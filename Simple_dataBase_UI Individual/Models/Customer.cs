using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_dataBase_UI_Individual.Models
{
//  Заказчики(
//      Код заказчика,
//      ФИО,
//      Адрес,
//      Телефон).
    public class Customer
    {
        Customer() { }
        public Customer(int id, string Full_Name, string Address, string Phone) {
            this.Id = id;
            this.Full_Name = Full_Name;
            this.Address = Address;
            this.Phone = Phone;
        }
        public Customer(List<object> array)
        {
            if (array == null || array.Count < 4)
                throw new ArgumentException("Array must contain at least 4 elements");

            this.Id = Convert.ToInt32(array[0]);
            this.Full_Name = array[1]?.ToString() ?? string.Empty;
            this.Address = array[2]?.ToString() ?? string.Empty;
            this.Phone = array[3]?.ToString() ?? string.Empty;
        }
        public int Id { get; set; }
        public string Full_Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
