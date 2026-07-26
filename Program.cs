using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<Person> people = new();

        people.Add(new Student(
            "Bizu",
            24,
            "Software Engineering"));

        people.Add(new Teacher(
            "Sara",
            35,
            "Mathematics"));

        people.Add(new Student(
            "Abel",
            22,
            "Computer Science"));


        people.Add(new Administrator("Bizuayehu", 30, 30, "management"));
        people.Add(new Administrator("Birara", 30, 30, "Dupty Manager"));

        Console.WriteLine("===== People =====\n");

        foreach (Person person in people)
        {
            Console.WriteLine(person.Display());
        }

        Console.WriteLine("Checking object types...\n");

        foreach (Person person in people)
        {
            if (person is Student student)
            {
                Console.WriteLine($"{student.Name} is a Student.");
            }
            else if (person is Teacher teacher)
            {
                Console.WriteLine($"{teacher.Name} is a Teacher.");
            }
            else if (person is Administrator admin)
            {
                Console.WriteLine($"{admin.Name} is a Adminstractor");
            }

        }
    }
}