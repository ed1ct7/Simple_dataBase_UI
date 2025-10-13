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

//    Сотрудники(
//          Код сотрудника,
//          ФИО,
//          Возраст,
//          Пол,
//          Адрес,
//          Телефон,
//          Паспортные данные,
//          Код должности)

namespace Simple_dataBase_UI_Individual.Data.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>
    {
        public EmployeeRepository(string dbFilePath) { }

        public override Employee CreateInstanceFromDataRow(DataRow row)
        {
            try
            {
                var employee = new Employee();

                if (!row.IsNull("id") && row["id"] != DBNull.Value)
                {
                    employee.Id = Convert.ToInt32(row["id"]);
                }
                else
                {
                    employee.Id = 0;
                }

                employee.Full_Name = row.IsNull("full_name") ? "" : row["full_name"].ToString();

                if (!row.IsNull("age") && row["age"] != DBNull.Value)
                {
                    if (int.TryParse(row["age"].ToString(), out int ageValue))
                    {
                        employee.Age = ageValue;
                    }
                    else
                    {
                        employee.Age = 0;
                    }
                }
                else
                {
                    employee.Age = 0;
                }

                employee.Gender = row.IsNull("gender") ? "" : row["gender"].ToString();
                employee.Address = row.IsNull("address") ? "" : row["address"].ToString();
                employee.Phone = row.IsNull("phone") ? "" : row["phone"].ToString();
                employee.Passport_Data = row.IsNull("passport_data") ? "" : row["passport_data"].ToString();

                if (!row.IsNull("position_id") && row["position_id"] != DBNull.Value)
                {
                    employee.Position_Id = Convert.ToInt32(row["position_id"]);
                }
                else
                {
                    employee.Position_Id = 0;
                }

                return employee;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating Employee from DataRow: {ex.Message}");
                throw;
            }
        }

        public bool IsIdExists(int id)
        {
            try
            {
                using (var command = new SQLiteCommand(DatabaseManager.m_dbConn))
                {
                    command.CommandText = "SELECT COUNT(*) FROM Employee WHERE id = @id";
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

        public override void Add(Employee entity)
        {
            try
            {
                // Проверяем, не существует ли уже запись с таким ID
                if (entity.Id != 0 && IsIdExists(entity.Id))
                {
                    MessageBox.Show($"Employee with ID {entity.Id} already exists!");
                    return;
                }

                using (var command = new SQLiteCommand(DatabaseManager.m_dbConn))
                {
                    if (entity.Id != 0)
                    {
                        command.CommandText = @"
                            INSERT INTO Employee (id, full_name, age, gender, address, phone, passport_data, position_id)
                            VALUES (@id, @full_name, @age, @gender, @address, @phone, @passport_data, @position_id)";
                        command.Parameters.AddWithValue("@id", entity.Id);
                    }
                    else
                    {
                        command.CommandText = @"
                            INSERT INTO Employee (full_name, age, gender, address, phone, passport_data, position_id)
                            VALUES (@full_name, @age, @gender, @address, @phone, @passport_data, @position_id)";
                    }

                    command.Parameters.AddWithValue("@full_name", entity.Full_Name ?? "");
                    command.Parameters.AddWithValue("@age", entity.Age);
                    command.Parameters.AddWithValue("@gender", entity.Gender ?? "");
                    command.Parameters.AddWithValue("@address", entity.Address ?? "");
                    command.Parameters.AddWithValue("@phone", entity.Phone ?? "");
                    command.Parameters.AddWithValue("@passport_data", entity.Passport_Data ?? "");
                    command.Parameters.AddWithValue("@position_id", entity.Position_Id);

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
                MessageBox.Show("Error adding employee: " + ex.Message);
            }
        }

        public override void Update(Employee entity)
        {
            try
            {
                using (var command = new SQLiteCommand(DatabaseManager.m_dbConn))
                {
                    command.CommandText = @"
                        UPDATE Employee 
                        SET full_name = @full_name, 
                            age = @age, 
                            gender = @gender, 
                            address = @address, 
                            phone = @phone, 
                            passport_data = @passport_data, 
                            position_id = @position_id 
                        WHERE id = @id";

                    command.Parameters.AddWithValue("@full_name", entity.Full_Name ?? "");
                    command.Parameters.AddWithValue("@age", entity.Age);
                    command.Parameters.AddWithValue("@gender", entity.Gender ?? "");
                    command.Parameters.AddWithValue("@address", entity.Address ?? "");
                    command.Parameters.AddWithValue("@phone", entity.Phone ?? "");
                    command.Parameters.AddWithValue("@passport_data", entity.Passport_Data ?? "");
                    command.Parameters.AddWithValue("@position_id", entity.Position_Id);
                    command.Parameters.AddWithValue("@id", entity.Id);

                    command.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error updating employee: " + ex.Message);
            }
        }
    }
}