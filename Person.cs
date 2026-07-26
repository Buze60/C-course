using System;

public class Person
{
    public string Name { get; }
    public int Age { get; }

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public virtual string Display()
    
    {
        return ($"Name : {Name}\nAge  : {Age}");
    }
}