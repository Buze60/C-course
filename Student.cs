using System;

public class Student : Person
{
    public string Department { get; }

    public Student(string name, int age, string department)
        : base(name, age)
    {
        Department = department;
    }

    public override void Introduce()
    {
        Console.WriteLine("=== Student ===");
        ShowBasicInfo();
        Console.WriteLine($"Department : {Department}");
        Console.WriteLine("I am studying at the university.");
        Console.WriteLine();
    }
}