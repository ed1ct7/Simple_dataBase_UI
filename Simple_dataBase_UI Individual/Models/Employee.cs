using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_dataBase_UI_Individual.Models
{
    //    Сотрудники(
    //          Код сотрудника,
    //          ФИО,
    //          Возраст,
    //          Пол,
    //          Адрес,
    //          Телефон,
    //          Паспортные данные,
    //          Код должности)
    public class Employee
    {
        public Employee() { }
        public Employee(int Id, string Full_Name, 
            int Age, string Gender, string Address, 
            string Phone, string Passport_Data, int Position_Id
            ) { 
            this.Id = Id;
            this.Full_Name = Full_Name;
            this.Age = Age;
            this.Gender = Gender;
            this.Address = Address;
            this.Phone = Phone;
            this.Passport_Data = Passport_Data;
            this.Position_Id = Position_Id;
        }
        public Employee(List<object> array)
        {
            if (array == null || array.Count < 8)
                throw new ArgumentException("Array must contain at least 8 elements");

            this.Id = Convert.ToInt32(array[0]);
            this.Full_Name = array[1]?.ToString() ?? string.Empty;
            this.Age = Convert.ToInt32(array[2]);
            this.Gender = array[3]?.ToString() ?? string.Empty;
            this.Address = array[4]?.ToString() ?? string.Empty;
            this.Phone = array[5]?.ToString() ?? string.Empty;
            this.Passport_Data = array[6]?.ToString() ?? string.Empty;
            this.Position_Id = Convert.ToInt32(array[7]);
        }
        public int Id { get; set; }
        public string Full_Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Passport_Data { get; set; }
        public int Position_Id { get; set; }
    }
}
