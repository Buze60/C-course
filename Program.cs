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

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                ShowMessage("Please enter a valid number.");
                Pause();
                continue;
            }

            switch (choice)
            {
                case 1:
                    ViewStudents(students);
                    break;

                case 2:
                    Console.Write("Student Name: ");
                    string newStudent = Console.ReadLine()!;
                    AddStudent(students, newStudent);
                    break;

                case 3:
                    Console.Write("Student Name: ");
                    string removeStudent = Console.ReadLine()!;

                    if (RemoveStudent(students, removeStudent))
                        ShowMessage("Student removed successfully.");
                    else
                        ShowMessage("Student not found.");

                    break;

                case 4:
                    Console.Write("Student Name: ");
                    string searchStudent = Console.ReadLine()!;

                    if (StudentExists(students, searchStudent))
                        ShowMessage("Student found.");
                    else
                        ShowMessage("Student not found.");

                    break;

                case 5:
                    ShowMessage("Total Students", GetStudentCount(students));
                    break;

                case 6:
                    isRunning = false;
                    ShowMessage("Goodbye!");
                    break;
                case 7:
                    RenameStudent(students);
                    break;

                default:
                    ShowMessage("Invalid choice.");
                    break;
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
        Console.WriteLine("7. Rename the student name");
        Console.WriteLine();
    }

    static void RenameStudent(List<string> students)
    {
        string searchName = Console.ReadLine()!;

        if (students.Contains(searchName))
        {
            int indexNumber = students.IndexOf(searchName);
            Console.WriteLine("Please Enter the new name! ");
            string RenamedStudent = Console.ReadLine()!;
            students[indexNumber] = RenamedStudent;
        }
    }

    static void ViewStudents(List<string> students)
    {
        Console.WriteLine("\nStudents:");

        foreach (string student in students)
        {
            Console.WriteLine($"- {student}");
        }
    }

    static void AddStudent(List<string> students, string studentName)
    {
        students.Add(studentName);
        ShowMessage("Student added successfully.");
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

    static void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }

    static void ShowMessage(string title, int number)
    {
        Console.WriteLine($"{title}: {number}");
    }

    static void Pause()
    {
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
}