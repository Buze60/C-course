public class Teacher : Person
{
    public string Subject { get; }

    public Teacher(string name, int age, string subject)
        : base(name, age)
    {
        Subject = subject;
    }

    public override string Display()
    {
        Console.WriteLine("=== Teacher ===");
        return $"{base.Display()}\nSubject : {Subject}";

    }
}