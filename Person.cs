public class Person
{
    public string Name { get; private set; }

    public int Age { get; private set; }

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public virtual void Display()
    {
        Console.WriteLine($"Name : {Name}");
        Console.WriteLine($"Age  : {Age}");
    }
}