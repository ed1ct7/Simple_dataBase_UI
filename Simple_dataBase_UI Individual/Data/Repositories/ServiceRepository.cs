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
                // Безопасное получение значения ID
                int id = 0;
                if (!row.IsNull("id") && int.TryParse(row["id"].ToString(), out int parsedId))
                {
                    id = parsedId;
                }

                // Безопасное получение decimal значения
                decimal price = 0;
                if (!row.IsNull("price") && decimal.TryParse(row["price"].ToString(), out decimal parsedPrice))
                {
                    price = parsedPrice;
                }

                return new Service
                {
                    Id = id,
                    Name = row.IsNull("name") ? "" : row["name"].ToString(),
                    Description = row.IsNull("description") ? "" : row["description"].ToString(),
                    Price = (int)(decimal)price
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating Service from DataRow: {ex.Message}");
                throw;
            }
        }
        public override void Add(Service entity)
            {
                try
                {
                    using (var command = new SQLiteCommand(DatabaseManager.m_dbConn))
                    {
                        command.CommandText = @"
                            INSERT INTO Service (name, description, price)
                            VALUES (@name, @description, @price)";

                        command.Parameters.AddWithValue("@name", entity.Name ?? "");
                        command.Parameters.AddWithValue("@description", entity.Description ?? "");
                        command.Parameters.AddWithValue("@price", entity.Price);

                        command.ExecuteNonQuery();

                        // Получаем ID новой записи
                        command.CommandText = "SELECT last_insert_rowid()";
                        entity.Id = Convert.ToInt32(command.ExecuteScalar());
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
