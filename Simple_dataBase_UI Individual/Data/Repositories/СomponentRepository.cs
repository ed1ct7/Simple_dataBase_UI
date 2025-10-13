using Simple_dataBase_UI_Individual.Data.Interfaces;
using Simple_dataBase_UI_Individual.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

//  Комплектующие(
//      Код комплектующего,
//      Код вида,
//      Марка,
//      Фирма производитель,
//      Страна производитель,
//      Дата выпуска,
//      Характеристики,
//      Срок гарантия,
//      Описание,
//      Цена)

namespace Simple_dataBase_UI_Individual.Data.Repositories
{
    public class СomponentRepository : BaseRepository<Models.Component>
    {
        public СomponentRepository(string dbFilePath) { }

        public override Models.Component CreateInstanceFromDataRow(DataRow row)
        {
            try
            {
                var component = new Models.Component();

                if (!row.IsNull("id") && row["id"] != DBNull.Value)
                {
                    component.Id = Convert.ToInt32(row["id"]);
                }
                else
                {
                    component.Id = 0;
                }

                if (!row.IsNull("type_id") && row["type_id"] != DBNull.Value)
                {
                    component.Type_Id = Convert.ToInt32(row["type_id"]);
                }
                else
                {
                    component.Type_Id = 0;
                }

                component.Brand = row.IsNull("brand") ? "" : row["brand"].ToString();
                component.Manufacturer_Company = row.IsNull("manufacturer_company") ? "" : row["manufacturer_company"].ToString();
                component.Manufacturer_Country = row.IsNull("manufacturer_country") ? "" : row["manufacturer_country"].ToString();
                component.Specifications = row.IsNull("specifications") ? "" : row["specifications"].ToString();
                component.Description = row.IsNull("description") ? "" : row["description"].ToString();

                // Обработка даты выпуска - преобразуем в строку в нужном формате
                if (!row.IsNull("release_date") && row["release_date"] != DBNull.Value)
                {
                    if (DateTime.TryParse(row["release_date"].ToString(), out DateTime releaseDate))
                    {
                        component.ReleaseDate = releaseDate.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        component.ReleaseDate = "";
                    }
                }
                else
                {
                    component.ReleaseDate = "";
                }

                if (!row.IsNull("warranty") && row["warranty"] != DBNull.Value)
                {
                    if (int.TryParse(row["warranty"].ToString(), out int warrantyValue))
                    {
                        component.Warranty = warrantyValue;
                    }
                    else
                    {
                        component.Warranty = 0;
                    }
                }
                else
                {
                    component.Warranty = 0;
                }

                if (!row.IsNull("price") && row["price"] != DBNull.Value)
                {
                    if (decimal.TryParse(row["price"].ToString(), out decimal priceValue))
                    {
                        component.Price = priceValue;
                    }
                    else
                    {
                        component.Price = 0;
                    }
                }
                else
                {
                    component.Price = 0;
                }

                return component;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating Component from DataRow: {ex.Message}");
                throw;
            }
        }

        public bool IsIdExists(int id)
        {
            try
            {
                using (var command = new SQLiteCommand(DatabaseManager.m_dbConn))
                {
                    command.CommandText = "SELECT COUNT(*) FROM Component WHERE id = @id";
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

        public override void Add(Models.Component entity)
        {
            try
            {
                // Проверяем, не существует ли уже запись с таким ID
                if (entity.Id != 0 && IsIdExists(entity.Id))
                {
                    MessageBox.Show($"Component with ID {entity.Id} already exists!");
                    return;
                }

                using (var command = new SQLiteCommand(DatabaseManager.m_dbConn))
                {
                    // Преобразуем строку даты в DateTime для базы данных
                    DateTime releaseDate = DateTime.MinValue;
                    if (!string.IsNullOrEmpty(entity.ReleaseDate) && DateTime.TryParse(entity.ReleaseDate, out DateTime parsedDate))
                    {
                        releaseDate = parsedDate;
                    }

                    if (entity.Id != 0)
                    {
                        command.CommandText = @"
                            INSERT INTO Component (id, type_id, brand, manufacturer_company, 
                                                  manufacturer_country, release_date, specifications, 
                                                  warranty, description, price)
                            VALUES (@id, @type_id, @brand, @manufacturer_company, 
                                    @manufacturer_country, @release_date, @specifications, 
                                    @warranty, @description, @price)";
                        command.Parameters.AddWithValue("@id", entity.Id);
                    }
                    else
                    {
                        command.CommandText = @"
                            INSERT INTO Component (type_id, brand, manufacturer_company, 
                                                  manufacturer_country, release_date, specifications, 
                                                  warranty, description, price)
                            VALUES (@type_id, @brand, @manufacturer_company, 
                                    @manufacturer_country, @release_date, @specifications, 
                                    @warranty, @description, @price)";
                    }

                    command.Parameters.AddWithValue("@type_id", entity.Type_Id);
                    command.Parameters.AddWithValue("@brand", entity.Brand ?? "");
                    command.Parameters.AddWithValue("@manufacturer_company", entity.Manufacturer_Company ?? "");
                    command.Parameters.AddWithValue("@manufacturer_country", entity.Manufacturer_Country ?? "");
                    command.Parameters.AddWithValue("@release_date", releaseDate);
                    command.Parameters.AddWithValue("@specifications", entity.Specifications ?? "");
                    command.Parameters.AddWithValue("@warranty", entity.Warranty);
                    command.Parameters.AddWithValue("@description", entity.Description ?? "");
                    command.Parameters.AddWithValue("@price", entity.Price);

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
                MessageBox.Show("Error adding component: " + ex.Message);
            }
        }

        public override void Update(Models.Component entity)
        {
            try
            {
                using (var command = new SQLiteCommand(DatabaseManager.m_dbConn))
                {
                    // Преобразуем строку даты в DateTime для базы данных
                    DateTime releaseDate = DateTime.MinValue;
                    if (!string.IsNullOrEmpty(entity.ReleaseDate) && DateTime.TryParse(entity.ReleaseDate, out DateTime parsedDate))
                    {
                        releaseDate = parsedDate;
                    }

                    command.CommandText = @"
                        UPDATE Component 
                        SET type_id = @type_id, 
                            brand = @brand, 
                            manufacturer_company = @manufacturer_company, 
                            manufacturer_country = @manufacturer_country, 
                            release_date = @release_date, 
                            specifications = @specifications, 
                            warranty = @warranty, 
                            description = @description, 
                            price = @price 
                        WHERE id = @id";

                    command.Parameters.AddWithValue("@type_id", entity.Type_Id);
                    command.Parameters.AddWithValue("@brand", entity.Brand ?? "");
                    command.Parameters.AddWithValue("@manufacturer_company", entity.Manufacturer_Company ?? "");
                    command.Parameters.AddWithValue("@manufacturer_country", entity.Manufacturer_Country ?? "");
                    command.Parameters.AddWithValue("@release_date", releaseDate);
                    command.Parameters.AddWithValue("@specifications", entity.Specifications ?? "");
                    command.Parameters.AddWithValue("@warranty", entity.Warranty);
                    command.Parameters.AddWithValue("@description", entity.Description ?? "");
                    command.Parameters.AddWithValue("@price", entity.Price);
                    command.Parameters.AddWithValue("@id", entity.Id);

                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error updating component: " + ex.Message);
            }
        }
    }
}