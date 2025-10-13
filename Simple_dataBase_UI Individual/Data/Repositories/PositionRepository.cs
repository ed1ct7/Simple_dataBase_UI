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

//  Должности(
//      Код должности,
//      Наименование должности,
//      Оклад,
//      Обязанности,
//      Требования)

namespace Simple_dataBase_UI_Individual.Data.Repositories
{
    public class PositionRepository : BaseRepository<Position>
    {
        public PositionRepository(string dbFilePath) { }

        public override Position CreateInstanceFromDataRow(DataRow row)
        {
            try
            {
                var position = new Position();

                if (!row.IsNull("id") && row["id"] != DBNull.Value)
                {
                    position.Id = Convert.ToInt32(row["id"]);
                }
                else
                {
                    position.Id = 0;
                }

                position.Name = row.IsNull("name") ? "" : row["name"].ToString();
                position.Duties = row.IsNull("duties") ? "" : row["duties"].ToString();
                position.Requirements = row.IsNull("requirements") ? "" : row["requirements"].ToString();

                if (!row.IsNull("salary") && row["salary"] != DBNull.Value)
                {
                    if (decimal.TryParse(row["salary"].ToString(), out decimal salaryValue))
                    {
                        position.Salary = salaryValue;
                    }
                    else
                    {
                        position.Salary = 0;
                    }
                }
                else
                {
                    position.Salary = 0;
                }

                return position;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating Position from DataRow: {ex.Message}");
                throw;
            }
        }

        public bool IsIdExists(int id)
        {
            try
            {
                using (var command = new SQLiteCommand(DatabaseManager.m_dbConn))
                {
                    command.CommandText = "SELECT COUNT(*) FROM Position WHERE id = @id";
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

        public override void Add(Position entity)
        {
            try
            {
                // Проверяем, не существует ли уже запись с таким ID
                if (entity.Id != 0 && IsIdExists(entity.Id))
                {
                    MessageBox.Show($"Position with ID {entity.Id} already exists!");
                    return;
                }

                using (var command = new SQLiteCommand(DatabaseManager.m_dbConn))
                {
                    if (entity.Id != 0)
                    {
                        command.CommandText = @"
                            INSERT INTO Position (id, name, salary, duties, requirements)
                            VALUES (@id, @name, @salary, @duties, @requirements)";
                        command.Parameters.AddWithValue("@id", entity.Id);
                    }
                    else
                    {
                        command.CommandText = @"
                            INSERT INTO Position (name, salary, duties, requirements)
                            VALUES (@name, @salary, @duties, @requirements)";
                    }

                    command.Parameters.AddWithValue("@name", entity.Name ?? "");
                    command.Parameters.AddWithValue("@salary", entity.Salary);
                    command.Parameters.AddWithValue("@duties", entity.Duties ?? "");
                    command.Parameters.AddWithValue("@requirements", entity.Requirements ?? "");

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
                MessageBox.Show("Error adding position: " + ex.Message);
            }
        }

        public override void Update(Position entity)
        {
            try
            {
                using (var command = new SQLiteCommand(DatabaseManager.m_dbConn))
                {
                    command.CommandText = @"
                        UPDATE Position 
                        SET name = @name, 
                            salary = @salary, 
                            duties = @duties, 
                            requirements = @requirements 
                        WHERE id = @id";

                    command.Parameters.AddWithValue("@name", entity.Name ?? "");
                    command.Parameters.AddWithValue("@salary", entity.Salary);
                    command.Parameters.AddWithValue("@duties", entity.Duties ?? "");
                    command.Parameters.AddWithValue("@requirements", entity.Requirements ?? "");
                    command.Parameters.AddWithValue("@id", entity.Id);

                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"Rows updated: {rowsAffected}");
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error updating position: " + ex.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                using (var command = new SQLiteCommand(DatabaseManager.m_dbConn))
                {
                    command.CommandText = "DELETE FROM Position WHERE id = @id";
                    command.Parameters.AddWithValue("@id", id);

                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"Position deleted: {rowsAffected} rows affected");
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error deleting position: " + ex.Message);
            }
        }

        public Position GetById(int id)
        {
            try
            {
                using (var command = new SQLiteCommand(DatabaseManager.m_dbConn))
                {
                    command.CommandText = "SELECT * FROM Position WHERE id = @id";
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var position = new Position
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Name = reader.IsDBNull(reader.GetOrdinal("name")) ? "" : reader.GetString(reader.GetOrdinal("name")),
                                Salary = reader.IsDBNull(reader.GetOrdinal("salary")) ? 0 : reader.GetDecimal(reader.GetOrdinal("salary")),
                                Duties = reader.IsDBNull(reader.GetOrdinal("duties")) ? "" : reader.GetString(reader.GetOrdinal("duties")),
                                Requirements = reader.IsDBNull(reader.GetOrdinal("requirements")) ? "" : reader.GetString(reader.GetOrdinal("requirements"))
                            };
                            return position;
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error getting position by ID: " + ex.Message);
            }

            return null;
        }
    }
}