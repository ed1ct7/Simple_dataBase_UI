using Simple_dataBase_UI_Individual.Data.Interfaces;
using Simple_dataBase_UI_Individual.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Simple_dataBase_UI_Individual.Data.Repositories
{
    public class OrderRepository : BaseRepository<Order>
    {
        public OrderRepository(string dbFilePath) { }

        public override Order CreateInstanceFromDataRow(DataRow row)
        {
            try
            {
                var order = new Order();

                if (!row.IsNull("id") && row["id"] != DBNull.Value)
                {
                    order.Id = Convert.ToInt32(row["id"]);
                }
                else
                {
                    order.Id = 0;
                }

                // Для дат используем строковое представление или преобразуем в DateTime
                order.Order_Date = row.IsNull("order_date") ? "" : row["order_date"].ToString();
                order.Completion_Date = row.IsNull("completion_date") ? "" : row["completion_date"].ToString();

                if (!row.IsNull("customer_id") && row["customer_id"] != DBNull.Value)
                {
                    order.Customer_Id = Convert.ToInt32(row["customer_id"]);
                }
                else
                {
                    order.Customer_Id = 0;
                }

                if (!row.IsNull("component1_id") && row["component1_id"] != DBNull.Value)
                {
                    order.Component1_Id = Convert.ToInt32(row["component1_id"]);
                }
                else
                {
                    order.Component1_Id = 0;
                }

                if (!row.IsNull("component2_id") && row["component2_id"] != DBNull.Value)
                {
                    order.Component2_Id = Convert.ToInt32(row["component2_id"]);
                }
                else
                {
                    order.Component2_Id = 0;
                }

                if (!row.IsNull("component3_id") && row["component3_id"] != DBNull.Value)
                {
                    order.Component3_Id = Convert.ToInt32(row["component3_id"]);
                }
                else
                {
                    order.Component3_Id = 0;
                }

                if (!row.IsNull("prepayment") && row["prepayment"] != DBNull.Value)
                {
                    if (decimal.TryParse(row["prepayment"].ToString(), out decimal prepaymentValue))
                    {
                        order.Prepayment = prepaymentValue;
                    }
                    else
                    {
                        order.Prepayment = 0;
                    }
                }
                else
                {
                    order.Prepayment = 0;
                }

                if (!row.IsNull("is_paid") && row["is_paid"] != DBNull.Value)
                {
                    order.Is_Paid = Convert.ToBoolean(row["is_paid"]);
                }
                else
                {
                    order.Is_Paid = false;
                }

                if (!row.IsNull("is_completed") && row["is_completed"] != DBNull.Value)
                {
                    order.Is_Completed = Convert.ToBoolean(row["is_completed"]);
                }
                else
                {
                    order.Is_Completed = false;
                }

                if (!row.IsNull("total_amount") && row["total_amount"] != DBNull.Value)
                {
                    if (decimal.TryParse(row["total_amount"].ToString(), out decimal totalAmountValue))
                    {
                        order.Total_Amount = totalAmountValue;
                    }
                    else
                    {
                        order.Total_Amount = 0;
                    }
                }
                else
                {
                    order.Total_Amount = 0;
                }

                if (!row.IsNull("total_warranty") && row["total_warranty"] != DBNull.Value)
                {
                    if (int.TryParse(row["total_warranty"].ToString(), out int warrantyValue))
                    {
                        order.Total_Warranty = warrantyValue;
                    }
                    else
                    {
                        order.Total_Warranty = 0;
                    }
                }
                else
                {
                    order.Total_Warranty = 0;
                }

                if (!row.IsNull("service1_id") && row["service1_id"] != DBNull.Value)
                {
                    order.Service1_Id = Convert.ToInt32(row["service1_id"]);
                }
                else
                {
                    order.Service1_Id = 0;
                }

                if (!row.IsNull("service2_id") && row["service2_id"] != DBNull.Value)
                {
                    order.Service2_Id = Convert.ToInt32(row["service2_id"]);
                }
                else
                {
                    order.Service2_Id = 0;
                }

                if (!row.IsNull("service3_id") && row["service3_id"] != DBNull.Value)
                {
                    order.Service3_Id = Convert.ToInt32(row["service3_id"]);
                }
                else
                {
                    order.Service3_Id = 0;
                }

                if (!row.IsNull("employee_id") && row["employee_id"] != DBNull.Value)
                {
                    order.Employee_Id = Convert.ToInt32(row["employee_id"]);
                }
                else
                {
                    order.Employee_Id = 0;
                }

                return order;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating Order from DataRow: {ex.Message}");
                throw;
            }
        }

        public bool IsIdExists(int id)
        {
            try
            {
                using (var command = new SQLiteCommand(DatabaseManager.m_dbConn))
                {
                    command.CommandText = "SELECT COUNT(*) FROM [Order] WHERE id = @id";
                    command.Parameters.AddWithValue("@id", id);

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking ID existence: {ex.Message}");
                return false;
            }
        }

        public override void Add(Order entity)
        {
            try
            {
                // Проверяем, не существует ли уже запись с таким ID
                if (entity.Id != 0 && IsIdExists(entity.Id))
                {
                    MessageBox.Show($"Order with ID {entity.Id} already exists!");
                    return;
                }

                using (var command = new SQLiteCommand(DatabaseManager.m_dbConn))
                {
                    if (entity.Id != 0)
                    {
                        command.CommandText = @"
                            INSERT INTO [Order] (id, order_date, completion_date, customer_id, 
                                            component1_id, component2_id, component3_id, 
                                            prepayment, is_paid, is_completed, total_amount, 
                                            total_warranty, service1_id, service2_id, service3_id, employee_id)
                            VALUES (@id, @order_date, @completion_date, @customer_id, 
                                    @component1_id, @component2_id, @component3_id, 
                                    @prepayment, @is_paid, @is_completed, @total_amount, 
                                    @total_warranty, @service1_id, @service2_id, @service3_id, @employee_id)";
                        command.Parameters.AddWithValue("@id", entity.Id);
                    }
                    else
                    {
                        command.CommandText = @"
                            INSERT INTO [Order] (order_date, completion_date, customer_id, 
                                            component1_id, component2_id, component3_id, 
                                            prepayment, is_paid, is_completed, total_amount, 
                                            total_warranty, service1_id, service2_id, service3_id, employee_id)
                            VALUES (@order_date, @completion_date, @customer_id, 
                                    @component1_id, @component2_id, @component3_id, 
                                    @prepayment, @is_paid, @is_completed, @total_amount, 
                                    @total_warranty, @service1_id, @service2_id, @service3_id, @employee_id)";
                    }

                    command.Parameters.AddWithValue("@order_date", entity.Order_Date ?? "");
                    command.Parameters.AddWithValue("@completion_date", entity.Completion_Date ?? "");
                    command.Parameters.AddWithValue("@customer_id", entity.Customer_Id);
                    command.Parameters.AddWithValue("@component1_id", entity.Component1_Id);
                    command.Parameters.AddWithValue("@component2_id", entity.Component2_Id);
                    command.Parameters.AddWithValue("@component3_id", entity.Component3_Id);
                    command.Parameters.AddWithValue("@prepayment", entity.Prepayment);
                    command.Parameters.AddWithValue("@is_paid", entity.Is_Paid);
                    command.Parameters.AddWithValue("@is_completed", entity.Is_Completed);
                    command.Parameters.AddWithValue("@total_amount", entity.Total_Amount);
                    command.Parameters.AddWithValue("@total_warranty", entity.Total_Warranty);
                    command.Parameters.AddWithValue("@service1_id", entity.Service1_Id);
                    command.Parameters.AddWithValue("@service2_id", entity.Service2_Id);
                    command.Parameters.AddWithValue("@service3_id", entity.Service3_Id);
                    command.Parameters.AddWithValue("@employee_id", entity.Employee_Id);

                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"Rows affected: {rowsAffected}");

                    // Если ID не был указан, получаем сгенерированный ID
                    if (entity.Id == 0)
                    {
                        command.CommandText = "SELECT last_insert_rowid()";
                        entity.Id = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error adding order: " + ex.Message);
            }
        }

        public override void Update(Order entity)
        {
            try
            {
                using (var command = new SQLiteCommand(DatabaseManager.m_dbConn))
                {
                    command.CommandText = @"
                        UPDATE [Order] 
                        SET order_date = @order_date, 
                            completion_date = @completion_date, 
                            customer_id = @customer_id, 
                            component1_id = @component1_id, 
                            component2_id = @component2_id, 
                            component3_id = @component3_id, 
                            prepayment = @prepayment, 
                            is_paid = @is_paid, 
                            is_completed = @is_completed, 
                            total_amount = @total_amount, 
                            total_warranty = @total_warranty, 
                            service1_id = @service1_id, 
                            service2_id = @service2_id, 
                            service3_id = @service3_id, 
                            employee_id = @employee_id 
                        WHERE id = @id";

                    command.Parameters.AddWithValue("@order_date", entity.Order_Date ?? "");
                    command.Parameters.AddWithValue("@completion_date", entity.Completion_Date ?? "");
                    command.Parameters.AddWithValue("@customer_id", entity.Customer_Id);
                    command.Parameters.AddWithValue("@component1_id", entity.Component1_Id);
                    command.Parameters.AddWithValue("@component2_id", entity.Component2_Id);
                    command.Parameters.AddWithValue("@component3_id", entity.Component3_Id);
                    command.Parameters.AddWithValue("@prepayment", entity.Prepayment);
                    command.Parameters.AddWithValue("@is_paid", entity.Is_Paid);
                    command.Parameters.AddWithValue("@is_completed", entity.Is_Completed);
                    command.Parameters.AddWithValue("@total_amount", entity.Total_Amount);
                    command.Parameters.AddWithValue("@total_warranty", entity.Total_Warranty);
                    command.Parameters.AddWithValue("@service1_id", entity.Service1_Id);
                    command.Parameters.AddWithValue("@service2_id", entity.Service2_Id);
                    command.Parameters.AddWithValue("@service3_id", entity.Service3_Id);
                    command.Parameters.AddWithValue("@employee_id", entity.Employee_Id);
                    command.Parameters.AddWithValue("@id", entity.Id);

                    command.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error updating order: " + ex.Message);
            }
        }
    }
}