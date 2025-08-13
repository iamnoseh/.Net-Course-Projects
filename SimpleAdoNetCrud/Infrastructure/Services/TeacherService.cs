using Domain.Models;
using Infrastructure.Interefaces;
using Infrastructure.Helpers;
using Npgsql;

namespace Infrastructure.Services;

public class TeacherService : ITeacherService
{
    private readonly string connectionString = DatabaseHelper.GetConnectionString();

    public bool CreateTeacher(Teacher teacher)
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            const string query = @"
            Insert into teachers(firstname, lastname, address,birthday,salary)
            values (@firstname, @lastname, @address, @birthday, @salary);
            ";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue(@"firstname", teacher.FirstName);
            command.Parameters.AddWithValue(@"lastname", teacher.LastName);
            command.Parameters.AddWithValue(@"address", teacher.Address);
            command.Parameters.AddWithValue(@"birthday", teacher.Birthday);
            command.Parameters.AddWithValue(@"salary", teacher.Salary);
            var effect = command.ExecuteNonQuery();
            connection.Close();
            if (effect > 0)
            {
                Console.WriteLine("Teacher created successfully");
                return true;
            }

            Console.WriteLine("Something went wrong");
            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public bool UpdateTeacher(Teacher teacher)
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            const string query = @"
            Update teachers set firstname = @firstname, lastname = @lastname, address = @address, birthday = @birthday, salary = @salary 
                           where id = @id;
            ";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue(@"id", teacher.Id);
            command.Parameters.AddWithValue(@"firstname", teacher.FirstName);
            command.Parameters.AddWithValue(@"lastname", teacher.LastName);
            command.Parameters.AddWithValue(@"address", teacher.Address);
            command.Parameters.AddWithValue(@"birthday", teacher.Birthday);
            command.Parameters.AddWithValue(@"salary", teacher.Salary);
            var effect = command.ExecuteNonQuery();
            connection.Close();
            if (effect > 0)
            {
                Console.WriteLine("Teacher updated successfully");
                return true;
            }

            Console.WriteLine("Something went wrong");
            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public bool DeleteTeacher(int id)
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            const string query = @"
            Delete from teachers where id = @id;
            ";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue(@"id", id);
            var effect = command.ExecuteNonQuery();
            connection.Close();
            if (effect > 0)
            {
                Console.WriteLine("Teacher deleted successfully");
                return true;
            }

            Console.WriteLine("Something went wrong");
            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public Teacher GetTeacherById(int id)
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            const string query = @"
            select * from teachers where id = @id;
            ";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue(@"id", id);
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                var teacher = new Teacher()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    FirstName = reader.GetString(reader.GetOrdinal("firstname")),
                    LastName = reader.GetString(reader.GetOrdinal("lastname")),
                    Address = reader.GetString(reader.GetOrdinal("address")),
                    Birthday = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("birthday"))),
                    Salary = reader.GetDecimal(reader.GetOrdinal("salary")),
                };
                return teacher;
            }

            connection.Close();
            Console.WriteLine("Something went wrong");
            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public List<Teacher> GetAllTeachers()
    {
        try
        {
            var teachers = new List<Teacher>();
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            const string query = @"select * from teachers";
            using var command = new NpgsqlCommand(query, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var teacher = new Teacher()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    FirstName = reader.GetString(reader.GetOrdinal("firstname")),
                    LastName = reader.GetString(reader.GetOrdinal("lastname")),
                    Address = reader.GetString(reader.GetOrdinal("address")),
                    Birthday = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("birthday"))),
                    Salary = reader.GetDecimal(reader.GetOrdinal("salary")),
                };
                teachers.Add(teacher);
            }
            connection.Close();
            if (teachers.Count > 0)
            {
                Console.WriteLine(teachers.Count + " - Teachers found!");
                return teachers;
            }

            Console.WriteLine("Teachers not found!");
            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}