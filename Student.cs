using System;

public class Student : Person
{
    public static int TotalStudents { get; private set; }

    public string Email { get; private set; }

    public string Department { get; private set; }

    public double GPA { get; private set; }

    public Student(
        string name,
        int age,
        string email,
        string department,
        double gpa)
        : base(name, age)
    {
        Email = email;
        Department = department;
        GPA = gpa;

        TotalStudents++;
    }

    public override void Display()
    {
        base.Display();

        Console.WriteLine($"Email      : {Email}");
        Console.WriteLine($"Department : {Department}");
        Console.WriteLine($"GPA        : {GPA:F2}");
        Console.WriteLine("----------------------------");
    }
}