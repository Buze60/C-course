using System;

public abstract class Person
{
    public string Name { get; }
    public int Age { get; }

    protected Person(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public void ShowBasicInfo()
    {
        Console.WriteLine($"Name : {Name}");
        Console.WriteLine($"Age  : {Age}");
    }

    public abstract void Introduce();
}