public class Student : Person
{
    public string Department { get; }

    public Student(string name, int age, string department)
        : base(name, age)
    {
        Department = department;
    }

    public override string Display()
    {
        Console.WriteLine("=== Student ===");
        return $"{base.Display()}\nDepartment : {Department}";

    }
}