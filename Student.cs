public class Student
{
    private int _age;

    public string Name { get; set; } = "";

    public string Email { get; set; } = "";

    public string Department { get; set; } = "";
    private double _gpa;

    public double GPA
    {
        get
        {
            return _gpa;
        }
        set
        {
            if (value >= 0 && value <= 4.0)
            {
                _gpa = value;
            }
            else
            {
                Console.WriteLine("Invalid GPA.");
            }
        }
    }
    public int Age
    {
        get
        {
            return _age;
        }
        set
        {
            if (value >= 20 && value <= 120)
            {
                _age = value;
            }
            else
            {
                Console.WriteLine("Invalid age. Age must be between 20 and 120.");
            }
        }
    }

    public Student()
    {
    }

    public Student(string name, int age, string email, string department,double gpa)
    {
        Name = name;
        Age = age;
        Email = email;
        Department = department;
        GPA = gpa;
    }

    public void Display()
    {
        Console.WriteLine("-----------------------------");
        Console.WriteLine($"Name       : {Name}");
        Console.WriteLine($"Age        : {Age}");
        Console.WriteLine($"Email      : {Email}");
        Console.WriteLine($"Department : {Department}");
        Console.WriteLine($"GPA : {GPA}");
    }
}