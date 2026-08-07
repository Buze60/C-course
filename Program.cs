using System;

public class Program
{
    static async Task Main()
    {
        StudentService service = new();

        service.StudentAdded += student =>
        {
            Console.WriteLine($"New Student Added: {student.Name}");
        };

        service.Add(new Student
        {
            Id = 1,
            Name = "Bizu",
            Age = 24
        });

        service.Add(new Student
        {
            Id = 2,
            Name = "Sara",
            Age = 22
        });

        Console.WriteLine();

        Console.WriteLine("All Students:");

        foreach (var student in service.GetAll())
        {
            Console.WriteLine(student);
        }

        Console.WriteLine();

        Student? found = service.FindById(2);

        Console.WriteLine($"Found: {found}");

        await service.SaveAsync("Data/students.txt");

        Console.WriteLine();

        Console.WriteLine("Saved Successfully.");

         await service.LoadAsync("Data/student.txt");

        foreach (Student student in service.GetAll())
        {
            Console.WriteLine(student);
        }


        
    }
}