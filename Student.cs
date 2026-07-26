using System;
using System.Collections.Generic;

public class Student : Person
{
    private readonly List<string> _courses = new();

    public Student(string name, int age)
        : base(name, age)
    {
    }

    public void RegisterCourse(string courseName)
    {
        if (string.IsNullOrWhiteSpace(courseName))
        {
            Console.WriteLine("Invalid course.");
            return;
        }

        check();

        _courses.Add(courseName);

        Console.WriteLine($"\n{Name} successfully registered for {courseName}.\n");
    }

    public void DropCourse(string courseName)
    {
        if (_courses.Contains(courseName))
        {
            _courses.Remove(courseName);
            Console.WriteLine("Course remove successfully");

        }
        else
        {
            Console.WriteLine("Course not found.");
        }
    }

    public void check()
    {
        Console.WriteLine("Checking prerequisites...");
        Console.WriteLine("Updating student record...");
        Console.WriteLine("Saving to database...");
        Console.WriteLine("Sending confirmation email...");
    }

    public override void Display()
    {
        Console.WriteLine("=== Student ===");
        Console.WriteLine($"Name : {Name}");
        Console.WriteLine($"Age  : {Age}");

        Console.WriteLine("Courses:");

        if (_courses.Count == 0)
        {
            Console.WriteLine("- None");
        }
        else
        {
            foreach (string course in _courses)
            {
                Console.WriteLine($"- {course}");
            }
        }

        Console.WriteLine();

    }
}