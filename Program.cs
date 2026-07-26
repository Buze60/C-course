using System;

class Program
{
    static void Main()
    {
        Student student = new Student("Bizu", 24);

        student.RegisterCourse("C# Programming");
        student.RegisterCourse("ASP.NET Core");

        student.DropCourse("C# Programming");

        student.Display();
    }
}