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

//  Заказчики(
//      Код заказчика,
//      ФИО,
//      Адрес,
//      Телефон).

namespace Simple_dataBase_UI_Individual.Data.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>
    {
        public CustomerRepository(string dbFilePath) { }

        public override Customer CreateInstanceFromDataRow(DataRow row)
        {
            try
            {
                var customer = new Customer();

                if (!row.IsNull("id") && row["id"] != DBNull.Value)
                {
                    customer.Id = Convert.ToInt32(row["id"]);
                }
                else
                {
                    customer.Id = 0;
                }

                customer.Full_Name = row.IsNull("full_name") ? "" : row["full_name"].ToString();
                customer.Address = row.IsNull("address") ? "" : row["address"].ToString();
                customer.Phone = row.IsNull("phone") ? "" : row["phone"].ToString();

                return customer;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating Customer from DataRow: {ex.Message}");
                throw;
            }
        }

        public bool IsIdExists(int id)
        {
            try
            {
                using (var command = new SQLiteCommand(DatabaseManager.m_dbConn))
                {
                    command.CommandText = "SELECT COUNT(*) FROM Customer WHERE id = @id";
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

        public override void Add(Customer entity)
        {
            try
            {
                // Проверяем, не существует ли уже запись с таким ID
                if (entity.Id != 0 && IsIdExists(entity.Id))
                {
                    MessageBox.Show($"Customer with ID {entity.Id} already exists!");
                    return;
                }

                using (var command = new SQLiteCommand(DatabaseManager.m_dbConn))
                {
                    if (entity.Id != 0)
                    {
                        command.CommandText = @"
                            INSERT INTO Customer (id, full_name, address, phone)
                            VALUES (@id, @full_name, @address, @phone)";
                        command.Parameters.AddWithValue("@id", entity.Id);
                    }
                    else
                    {
                        command.CommandText = @"
                            INSERT INTO Customer (full_name, address, phone)
                            VALUES (@full_name, @address, @phone)";
                    }

                    command.Parameters.AddWithValue("@full_name", entity.Full_Name ?? "");
                    command.Parameters.AddWithValue("@address", entity.Address ?? "");
                    command.Parameters.AddWithValue("@phone", entity.Phone ?? "");

                    int rowsAffected = command.ExecuteNonQuery();

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
                MessageBox.Show("Error adding customer: " + ex.Message);
            }
        }

        public override void Update(Customer entity)
        {
            try
            {
                using (var command = new SQLiteCommand(DatabaseManager.m_dbConn))
                {
                    command.CommandText = @"
                        UPDATE Customer 
                        SET full_name = @full_name, 
                            address = @address, 
                            phone = @phone 
                        WHERE id = @id";

                    command.Parameters.AddWithValue("@full_name", entity.Full_Name ?? "");
                    command.Parameters.AddWithValue("@address", entity.Address ?? "");
                    command.Parameters.AddWithValue("@phone", entity.Phone ?? "");
                    command.Parameters.AddWithValue("@id", entity.Id);

                    command.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error updating customer: " + ex.Message);
            }
        }
    }
}