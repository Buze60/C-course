using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<Student> students = new();

        students.Add(new Student("Bizu", 24, "bizu@example.com", "Software Engineering", 3.69));
        students.Add(new Student("Sara", 22, "sara@example.com", "Computer Science", 4));

        Console.WriteLine("===== Student List =====\n");

        foreach (Student student in students)
        {
            student.Display();
        }

        Console.WriteLine("\nCreate New Student\n");

        Console.Write("Name: ");
        string name = Console.ReadLine()!;

        Console.Write("Age: ");
        int age = int.Parse(Console.ReadLine()!);

        Console.Write("Email: ");
        string email = Console.ReadLine()!;

        Console.Write("Department: ");
        string department = Console.ReadLine()!;
        Console.Write("GPA: ");
        if (double.TryParse(Console.ReadLine(), out double gpa))
        {
            // Nothing happens
        }

        Student newStudent = new Student(name, age, email, department, gpa);

        students.Add(newStudent);

        Console.WriteLine("\nUpdated Student List\n");

        foreach (Student student in students)
        {
            student.Display();
        }
    }
}