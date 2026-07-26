using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<Person> people = new();

        people.Add(new Student(
            "Bizuayehu",
            24,
            "Software Engineering"));

        people.Add(new Teacher(
            "Sara",
            35,
            "Mathematics"));

        foreach (Person person in people)
        {
            person.Introduce();
        }

        // This line would NOT compile:
        // Person person = new Person("John", 30);
    }
}