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
        Order() { }
        public Order(int Id, string Order_Date, string Completion_Date,
            int Customer_Id, int Component1_Id, int Component2_Id,
            int Component3_Id, int Prepayment, bool Is_Paid,
            bool Is_Completed, decimal Total_Amount,string Total_Warranty,
            int Service1_Id, int Service2_Id, int Service3_Id, int Employee_Id
            ) {
            this.Id = Id;
            this.Order_Date = Order_Date;
            this.Completion_Date = Completion_Date;
            this.Customer_Id = Customer_Id;
            this.Component1_Id = Component1_Id;
            this.Component2_Id = Component2_Id;
            this.Component3_Id = Component3_Id;
            this.Prepayment = Prepayment;
            this.Is_Paid = Is_Paid;
            this.Is_Completed = Is_Completed;
            this.Total_Amount = Total_Amount;
            this.Total_Warranty = Total_Warranty;
            this.Service1_Id = Service1_Id;
            this.Service2_Id = Service2_Id;
            this.Service3_Id = Service3_Id;
            this.Employee_Id = Employee_Id;
        }
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
