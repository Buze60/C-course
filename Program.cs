using System;
using System.Collections.Generic;

enum MenuOption
{
    ViewStudents = 1,
    AddStudent,
    RemoveStudent,
    SearchStudent,
    TotalStudents,
    Exit
}

struct StudentStatistics
{
    public int TotalStudents;
    public DateTime LastUpdated;
}

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

        StudentStatistics stats = new StudentStatistics
        {
            TotalStudents = students.Count,
            LastUpdated = DateTime.Now
        };

        bool isRunning = true;

        while (isRunning)
        {
            Console.Clear();
            ShowMenu();

            Console.Write("Choose an option: ");

            if (!int.TryParse(Console.ReadLine(), out int input))
            {
                ShowMessage("Please enter a valid number.");
                Pause();
                continue;
            }

            MenuOption option = (MenuOption)input;

            try
            {
                switch (option)
                {
                    case MenuOption.ViewStudents:
                        ViewStudents(students);
                        break;

                    case MenuOption.AddStudent:
                        Console.Write("Student Name: ");
                        AddStudent(students, Console.ReadLine()!);

                        stats.TotalStudents = students.Count;
                        stats.LastUpdated = DateTime.Now;
                        break;

                    case MenuOption.RemoveStudent:
                        Console.Write("Student Name: ");
                        string name = Console.ReadLine()!;

                        if (RemoveStudent(students, name))
                        {
                            stats.TotalStudents = students.Count;
                            stats.LastUpdated = DateTime.Now;
                            ShowMessage("Student removed successfully.");
                        }
                        else
                        {
                            ShowMessage("Student not found.");
                        }

                        break;

                    case MenuOption.SearchStudent:
                        Console.Write("Student Name: ");
                        string search = Console.ReadLine()!;

                        ShowMessage(StudentExists(students, search)
                            ? "Student found."
                            : "Student not found.");
                        break;

                    case MenuOption.TotalStudents:
                        Console.WriteLine($"Total Students : {stats.TotalStudents}");
                        Console.WriteLine($"Last Updated   : {stats.LastUpdated}");
                        break;

                    case MenuOption.Exit:
                        isRunning = false;
                        ShowMessage("Goodbye!");
                        break;

                    default:
                        ShowMessage("Invalid option.");
                        break;
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Error: {ex.Message}");
            }

            if (isRunning)
                Pause();
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
        foreach (var student in students)
            Console.WriteLine($"- {student}");
    }

    static void AddStudent(List<string> students, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Student name cannot be empty.");

        students.Add(name);
        ShowMessage("Student added successfully.");
    }

    static bool RemoveStudent(List<string> students, string name)
    {
        return students.Remove(name);
    }

    static bool StudentExists(List<string> students, string name)
    {
        return students.Contains(name);
    }

    static void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }

    static void Pause()
    {
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
}