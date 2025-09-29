﻿using System;
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
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string PassportData { get; set; }
        public int PositionId { get; set; }
    }
}
