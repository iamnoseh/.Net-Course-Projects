

using Domain.Models;
using Infrastructure.Services;

StudentService service = new StudentService();
TeacherService teacherService = new TeacherService();

// var student = new  Student("Hokim"," ",new DateOnly(2001,04,23),"Hisor",12);
// student.Id = 3;
//
// var res = service.UpdateStudent(student);
// Console.WriteLine(res);

// var res = service.DeleteStudent(4);
// Console.WriteLine(res);

// var res = service.GetStudentById(3);
// Console.WriteLine("FirstName: " + res.FirstName + " LastName: " + res.LastName + " BirthDate: " +  res.Birthday);
//


var res = teacherService.GetAllTeachers();
foreach (var item in res)
{
    Console.WriteLine($"{item.FirstName} || {item.LastName} || {item.Birthday} || {item.Address} || {item.Salary}");
}