using System;

public class Teacher : Person
{
    public string Subject { get; }

    public Teacher(string name, int age, string subject)
        : base(name, age)
    {
        Subject = subject;
    }

    public override void Introduce()
    {
        Console.WriteLine("=== Teacher ===");
        ShowBasicInfo();
        Console.WriteLine($"Subject : {Subject}");
        Console.WriteLine("I teach university students.");
        Console.WriteLine();
    }
}