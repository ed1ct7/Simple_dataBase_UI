using Simple_dataBase_UI_Individual.Data;
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

//  Виды комплектующих(
//          Код вида,
//          Наименование,
//          Описание)

namespace Simple_dataBase_UI_Individual.Data.Repositories
{
    public class ComponentTypeRepository : BaseRepository<ComponentType>
    {
        public ComponentTypeRepository(string dbFilePath) { }

        public override ComponentType CreateInstanceFromDataRow(DataRow row)
        {
            try
            {
                var componentType = new ComponentType();

                if (!row.IsNull("id") && row["id"] != DBNull.Value)
                {
                    componentType.Id = Convert.ToInt32(row["id"]);
                }
                else
                {
                    componentType.Id = 0;
                }

                componentType.Name = row.IsNull("name") ? "" : row["name"].ToString();
                componentType.Description = row.IsNull("description") ? "" : row["description"].ToString();

                Console.WriteLine($"Created ComponentType: ID={componentType.Id}, Name='{componentType.Name}'");

                return componentType;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating ComponentType from DataRow: {ex.Message}");
                throw;
            }
        }

        public bool IsIdExists(int id)
        {
            try
            {
                using (var command = new SQLiteCommand(DatabaseManager.m_dbConn))
                {
                    command.CommandText = "SELECT COUNT(*) FROM ComponentType WHERE id = @id";
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

        public override void Add(ComponentType entity)
        {
            try
            {
                // Проверяем, не существует ли уже запись с таким ID
                if (entity.Id != 0 && IsIdExists(entity.Id))
                {
                    MessageBox.Show($"ComponentType with ID {entity.Id} already exists!");
                    return;
                }

                using (var command = new SQLiteCommand(DatabaseManager.m_dbConn))
                {
                    if (entity.Id != 0)
                    {
                        command.CommandText = @"
                            INSERT INTO ComponentType (id, name, description)
                            VALUES (@id, @name, @description)";
                        command.Parameters.AddWithValue("@id", entity.Id);
                    }
                    else
                    {
                        command.CommandText = @"
                            INSERT INTO ComponentType (name, description)
                            VALUES (@name, @description)";
                    }

                    command.Parameters.AddWithValue("@name", entity.Name ?? "");
                    command.Parameters.AddWithValue("@description", entity.Description ?? "");

                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"Rows affected: {rowsAffected}");

                    // Если ID не был указан, получаем сгенерированный ID
                    if (entity.Id == 0)
                    {
                        command.CommandText = "SELECT last_insert_rowid()";
                        entity.Id = Convert.ToInt32(command.ExecuteScalar());
                    }

                    Console.WriteLine($"ComponentType added with ID: {entity.Id}");
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error adding component type: " + ex.Message);
            }
        }

        public override void Update(ComponentType entity)
        {
            try
            {
                using (var command = new SQLiteCommand(DatabaseManager.m_dbConn))
                {
                    command.CommandText = @"
                        UPDATE ComponentType 
                        SET name = @name, 
                            description = @description 
                        WHERE id = @id";

                    command.Parameters.AddWithValue("@name", entity.Name ?? "");
                    command.Parameters.AddWithValue("@description", entity.Description ?? "");
                    command.Parameters.AddWithValue("@id", entity.Id);

                    command.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error updating component type: " + ex.Message);
            }
        }
    }
}