using System;

public class Student
{
    public string Name { get; }

    private readonly INotificationService _notificationService;//SMS type(dependancy injection)

    public Student(
        string name,
        INotificationService notificationService)
    {
        Name = name;
        _notificationService = notificationService;
    }

    public void RegisterCourse(string courseName)
    {
        Console.WriteLine($"{Name} registered for {courseName}.");

        _notificationService.Send(
            $"Hello {Name}, your registration for '{courseName}' was successful.");
    }
}