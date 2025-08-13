using Domain.Models;
using Infrastructure.Interefaces;
using Infrastructure.Helpers;
using Npgsql;
using System.Data;

namespace Infrastructure.Services;

public class StudentService : IStudentService
{
    private readonly string connectionString = DatabaseHelper.GetConnectionString();

    public bool CreateStudent(Student student)
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            const string query = @"
                Insert into students(firstname,lastname,address,birthday,level) 
                values(@firstname,@lastname,@address,@birthday,@level);
            ";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("firstname", student.FirstName);
            command.Parameters.AddWithValue("lastname", student.LastName);
            command.Parameters.AddWithValue("address", student.Address);
            command.Parameters.AddWithValue("birthday", student.Birthday);
            command.Parameters.AddWithValue("level", student.Level);
            var effect =  command.ExecuteNonQuery();  
            connection.Close();
            if (effect > 0)
            {
                Console.WriteLine("Student created successfully");
                return true;
            }

            Console.WriteLine("Student creation failed");
            return false;
          
        }
        catch (Exception ex)
        {
            Console.WriteLine($" {ex.Message}");
            throw new Exception("Something went wrong");
        }
    }

    public bool UpdateStudent(Student student)
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            const string query = @"
                UPDATE Students 
                SET firstname = @firstname, lastname = @lastname, birthday = @birthday, address = @address, level = @level
                WHERE Id = @Id;";

            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", student.Id);
            command.Parameters.AddWithValue("@firstname", student.FirstName);
            command.Parameters.AddWithValue("@lastname", student.LastName);
            command.Parameters.AddWithValue("@birthday", student.Birthday);
            command.Parameters.AddWithValue("@address", student.Address);
            command.Parameters.AddWithValue("@level", student.Level);
            var effect = command.ExecuteNonQuery();
            connection.Close();

            if (effect == 0)
            {
                Console.WriteLine("Something went wrong");
                return false;
            }

            Console.WriteLine("Student updated successfully");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($" {ex.Message}");
            throw new Exception("Something went wrong");
        }
    }

    public bool DeleteStudent(int id)
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            const string query = "DELETE FROM Students WHERE Id = @Id;";

            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            var rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected == 0)
            {
                Console.WriteLine("Something went wrong");
                return false;
            }

            Console.WriteLine("Student deleted successfully");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
            return false;
        }
    }

    public Student GetStudentById(int id)
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            const string query = "SELECT Id, firstname, lastname, birthday, address, level FROM Students WHERE Id = @Id;";

            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new Student
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    FirstName = reader.GetString(reader.GetOrdinal("firstname")),
                    LastName = reader.GetString(reader.GetOrdinal("lastname")),
                    Address = reader.GetString(reader.GetOrdinal("address")),
                    Birthday = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("birthday"))),
                    Level = reader.GetInt32(reader.GetOrdinal("level"))
                };
            }

            Console.WriteLine("Student Not Found");
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($" {ex.Message}");
            throw new Exception("Some thing went  wrong");
        }
    }

    public List<Student> GetAllStudents()
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            const string query = "SELECT Id, firstname, lastname, birthday, address, level FROM Students ORDER BY Id;";

            using var command = new NpgsqlCommand(query, connection);
            using var reader = command.ExecuteReader();

            var students = new List<Student>();

            while (reader.Read())
            {
                var student = new Student
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    FirstName = reader.GetString(reader.GetOrdinal("firstname")),
                    LastName = reader.GetString(reader.GetOrdinal("lastname")),
                    Address = reader.GetString(reader.GetOrdinal("address")),
                    Birthday = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("birthday"))),
                    Level = reader.GetInt32(reader.GetOrdinal("level"))
                };

                students.Add(student);
            }

            if (students.Count == 0)
            {
                throw new Exception("Students Not Found");
            }
            
            Console.WriteLine($"{students.Count} - Student found!");
            return students;
        }
        catch (Exception ex)
        {
            Console.WriteLine($" {ex.Message}");
            throw new Exception("Some thing went  wrong");
        }
    }
}