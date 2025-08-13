namespace Domain.Models;

public class Person
{
    public int Id  { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly Birthday { get; set; }
    public string Address { get; set; }
}