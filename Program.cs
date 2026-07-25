using System;

class Program
{
    static void Main()
    {
        Student student1 = new Student(
            "Bizu",
            24,
            "bizu@example.com",
            "Software Engineering",
            3.90);

        Student student2 = new Student(
            "Sara",
            22,
            "sara@example.com",
            "Computer Science",
            3.75);

        Teacher teacher1 = new Teacher("Abebe", 25, 35000, "Geograpy");
        Teacher teacher2 = new Teacher("Chala", 29, 35000, "maths");

        student1.Display();
        student2.Display();
        teacher1.Display();
        teacher2.Display();

        Console.WriteLine($"Total Students: {Student.TotalStudents}");
    }
}