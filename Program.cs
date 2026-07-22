using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<string> students = new()
        {
            "Abel",
            "Sara",
            "Bizu"
        };

        bool isRunning = true;

        while (isRunning)
        {
            Console.Clear();

            ShowMenu();

            Console.Write("Choose an option: ");

            int choice = int.Parse(Console.ReadLine()!);

            switch (choice)
            {
                case 1:
                    ViewStudents(students);
                    break;

                case 2:
                    Console.Write("Enter student name: ");
                    string newStudent = Console.ReadLine()!;
                    AddStudent(students, newStudent);
                    break;

                case 3:
                    Console.Write("Enter student name to remove: ");
                    string removeStudent = Console.ReadLine()!;

                    if (RemoveStudent(students, removeStudent))
                    {
                        Console.WriteLine("Student removed successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Student not found.");
                    }

                    break;

                case 4:
                    Console.Write("Enter student name to search: ");
                    string searchStudent = Console.ReadLine()!;

                    if (StudentExists(students, searchStudent))
                    {
                        Console.WriteLine("Student found.");
                    }
                    else
                    {
                        Console.WriteLine("Student not found.");
                    }

                    break;

                case 5:
                    Console.WriteLine($"Total Students: {GetStudentCount(students)}");
                    break;

                case 6:
                    isRunning = false;
                    Console.WriteLine("Goodbye!");
                    break;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }

            if (isRunning)
            {
                Console.WriteLine("\nPress any key...");
                Console.ReadKey();
            }
        }
    }

    static void ShowMenu()
    {
        Console.WriteLine("===== Student Management System =====");
        Console.WriteLine("1. View Students");
        Console.WriteLine("2. Add Student");
        Console.WriteLine("3. Remove Student");
        Console.WriteLine("4. Search Student");
        Console.WriteLine("5. Total Students");
        Console.WriteLine("6. Exit");
        Console.WriteLine();
    }

    static void ViewStudents(List<string> students)
    {
        Console.WriteLine("\nStudent List:");

        foreach (string student in students)
        {
            Console.WriteLine($"- {student}");
        }
    }

    static void AddStudent(List<string> students, string studentName)
    {
        students.Add(studentName);
        Console.WriteLine("Student added successfully.");
    }

    static bool RemoveStudent(List<string> students, string studentName)
    {
        return students.Remove(studentName);
    }

    static bool StudentExists(List<string> students, string studentName)
    {
        return students.Contains(studentName);
    }

    static int GetStudentCount(List<string> students)
    {
        return students.Count;
    }
}