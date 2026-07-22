using System;
using System.Collections.Generic;

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

    Console.WriteLine("===== Student Management System =====");
    Console.WriteLine("1. View Students");
    Console.WriteLine("2. Add Student");
    Console.WriteLine("3. Remove Student");
    Console.WriteLine("4. Search the student List: ");
    Console.WriteLine("5. Display Total Number of student: ");
    Console.WriteLine("6. Exit");
    Console.Write("\nChoose an option: ");

    int choice = int.Parse(Console.ReadLine()!);

    switch (choice)
    {
        case 1:
            Console.WriteLine("\nStudent List:");

            foreach (var student in students)
            {
                Console.WriteLine($"- {student}");
            }
            break;

        case 2:
            Console.Write("Enter student name: ");
            string newStudent = Console.ReadLine()!;
            students.Add(newStudent);
            Console.WriteLine("Student added successfully.");
            break;

        case 3:
            Console.Write("Enter student name to remove: ");
            string removeStudent = Console.ReadLine()!;

            if (students.Remove(removeStudent))
            {
                Console.WriteLine("Student removed.");
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
            break;

        case 4:

            string name = Console.ReadLine()!;
            if (students.Contains(name))
            {
                Console.WriteLine($"student name {name} is found");
            }
            else
            {
                Console.WriteLine($"The student name {name} is not found😒");
            }
            break;
        case 5:
            string totalStudent = students.Count.ToString();
            Console.WriteLine($"The total number of students {totalStudent}");
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
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
}