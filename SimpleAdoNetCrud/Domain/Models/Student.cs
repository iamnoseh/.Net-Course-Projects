namespace Domain.Models;

public class Student : Person
{
    public int Level { get; set; }

    public Student()
    {
        
    }
    public Student(string firstName,string lastName,DateOnly birthDate,string address,int level)
    {
        FirstName = firstName;
        LastName = lastName;
        Birthday = birthDate;
        Address = address;
        Level = level;
    }
}