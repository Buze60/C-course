using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<Student> students = new();

        students.Add(new Student
        {
            Name = "Abel",
            Age = 20,
            Email = "abel@example.com"
        });

        students.Add(new Student
        {
            Name = "Sara",
            Age = 22,
            Email = "sara@example.com"
        });

        bool isRunning = true;

        while (isRunning)
        {
            Console.Clear();
            ViewMain();


            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid input.");
                Pause();
                continue;
            }

            switch (choice)
            {
                case 1:
                    ViewStudents(students);
                    break;

                case 2:
                    AddStudent(students);
                    break;

                case 3:
                    isRunning = false;
                    break;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }

            if (isRunning)
                Pause();
        }
    }

    static void ViewStudents(List<Student> students)
    {
        Console.WriteLine();

        foreach (Student student in students)
        {
            Console.WriteLine($"Name : {student.Name}");
            Console.WriteLine($"Age  : {student.Age}");
            Console.WriteLine($"Email: {student.Email}");
            Console.WriteLine("=======================");
        }
    }

    static void AddStudent(List<Student> students)
    {
        Student student = new Student();

        Console.Write("Name : ");
        student.Name = Console.ReadLine()!;

        Console.Write("Age : ");
        student.Age = int.Parse(Console.ReadLine()!);

        Console.Write("Email : ");
        student.Email = Console.ReadLine()!;

        students.Add(student);

        Console.WriteLine("Student added successfully.");
    }

    static void Pause()
    {
        Console.WriteLine("\nPress any key...");
        Console.ReadKey();
    }

    static void ViewMain()
    {
        Console.WriteLine("===== Student Management System =====");
        Console.WriteLine("1. View Students");
        Console.WriteLine("2. Add Student");
        Console.WriteLine("3. Exit");
        Console.Write("Choose: ");
    }
}