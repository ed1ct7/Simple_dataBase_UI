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
            string Phone, string Passport_Data, int Position_Id) { 
            this.Id = Id;
            this.Full_Name = Full_Name;
            this.Age = Age;
            this.Gender = Gender;
            this.Address = Address;
            this.Phone = Phone;
            this.Passport_Data = Passport_Data;
            this.Position_Id = Position_Id;
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
