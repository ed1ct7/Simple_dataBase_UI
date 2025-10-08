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

    //  Услуги(
    //      Код услуги,
    //      Наименование,
    //      Описание,
    //      Стоимость)

    namespace Simple_dataBase_UI_Individual.Data.Repositories
    {
        public class ServiceRepository : BaseRepository<Service>
        {
            public ServiceRepository(string dbFilePath){ }
        public override Service CreateInstanceFromDataRow(DataRow row)
        {
            try
            {
                var service = new Service();

                if (!row.IsNull("id") && row["id"] != DBNull.Value)
                {
                    service.Id = Convert.ToInt32(row["id"]);
                }
                else
                {
                    service.Id = 0;
                }

                service.Name = row.IsNull("name") ? "" : row["name"].ToString();
                service.Description = row.IsNull("description") ? "" : row["description"].ToString();

                if (!row.IsNull("price") && row["price"] != DBNull.Value)
                {
                    if (decimal.TryParse(row["price"].ToString(), out decimal priceValue))
                    {
                        service.Price = priceValue;
                    }
                    else
                    {
                        service.Price = 0;
                    }
                }
                else
                {
                    service.Price = 0;
                }

                Console.WriteLine($"Created Service: ID={service.Id}, Name='{service.Name}', Price={service.Price}");

                return service;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating Service from DataRow: {ex.Message}");
                throw;
            }
        }
        public bool IsIdExists(int id)
        {
            try
            {
                using (var command = new SQLiteCommand(DatabaseManager.m_dbConn))
                {
                    command.CommandText = "SELECT COUNT(*) FROM Service WHERE id = @id";
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
        public override void Add(Service entity)
        {
            try
            {
                // Проверяем, не существует ли уже запись с таким ID
                if (entity.Id != 0 && IsIdExists(entity.Id))
                {
                    MessageBox.Show($"Service with ID {entity.Id} already exists!");
                    return;
                }

                using (var command = new SQLiteCommand(DatabaseManager.m_dbConn))
                {
                    if (entity.Id != 0)
                    {
                        command.CommandText = @"
                            INSERT INTO Service (id, name, description, price)
                            VALUES (@id, @name, @description, @price)";
                        command.Parameters.AddWithValue("@id", entity.Id);
                    }
                    else
                    {
                        command.CommandText = @"
                            INSERT INTO Service (name, description, price)
                            VALUES (@name, @description, @price)";
                    }

                    command.Parameters.AddWithValue("@name", entity.Name ?? "");
                    command.Parameters.AddWithValue("@description", entity.Description ?? "");
                    command.Parameters.AddWithValue("@price", entity.Price);

                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"Rows affected: {rowsAffected}");

                    // Если ID не был указан, получаем сгенерированный ID
                    if (entity.Id == 0)
                    {
                        command.CommandText = "SELECT last_insert_rowid()";
                        entity.Id = Convert.ToInt32(command.ExecuteScalar());
                    }

                    Console.WriteLine($"Service added with ID: {entity.Id}");
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error adding service: " + ex.Message);
            }
        }
        public override void Update(Service entity)
                {
                    try
                    {
                        using (var command = new SQLiteCommand(DatabaseManager.m_dbConn))
                        {
                            command.CommandText = @"
                                UPDATE Service 
                                SET name = @name, 
                                    description = @description, 
                                    price = @price 
                                WHERE id = @id";

                            command.Parameters.AddWithValue("@name", entity.Name ?? "");
                            command.Parameters.AddWithValue("@description", entity.Description ?? "");
                            command.Parameters.AddWithValue("@price", entity.Price);
                            command.Parameters.AddWithValue("@id", entity.Id);

                            command.ExecuteNonQuery();
                        }
                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show("Error updating service: " + ex.Message);
                    }
                }
            }
    }
