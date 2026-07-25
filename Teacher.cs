public class Teacher : Person
{
    public decimal Salary { get; private set; }
    public string Subject { get; private set; } = "";

    public Teacher(
        string name,
        int age,
        decimal salary,
        string subject) : base(name, age)
    {
        Salary = salary;
        Subject = subject;
    }


    public override void Display()
    {
        base.Display();
        Console.WriteLine($"Salary: {Salary}");
        Console.WriteLine($"Subject: {Subject}");
        Console.WriteLine("----------------------------");
    }
}