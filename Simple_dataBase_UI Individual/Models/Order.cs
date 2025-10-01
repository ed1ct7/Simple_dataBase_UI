using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_dataBase_UI_Individual.Models
{
    //      Заказы(
    //          Дата заказа,
    //          Дата исполнения,
    //          Код заказчика,
    //          Код комплектующего 1,
    //          Код комплектующего 2,
    //          Код комплектующего 3,
    //          Доля предоплаты,
    //          Отметка об оплате,
    //          Отметка об исполнении,
    //          Общая стоимость,
    //          Срок общей гарантии,
    //          Код услуги 1,
    //          Код услуги 2,
    //          Код услуги 3,
    //          Код сотрудника)

    public class Order
    {
        public int Id { get; set; }
        public string Order_Date { get; set; }
        public string Completion_Date { get; set; }
        public int Customer_Id { get; set; }
        public int Component1_Id { get; set; }
        public int Component2_Id { get; set; }
        public int Component3_Id { get; set; }
        public decimal Prepayment { get; set; }
        public bool Is_Paid { get; set; }
        public bool Is_Completed { get; set; }
        public decimal Total_Amount { get; set; }
        public string Total_Warranty { get; set; }
        public int Service1_Id { get; set; }
        public int Service2_Id { get; set; }
        public int Service3_Id { get; set; }
        public int Employee_Id { get; set; }

    }
}
