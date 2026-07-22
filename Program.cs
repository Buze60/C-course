List<string> students = new()
{
    "Bizuayehu"
};




bool isRunning = true;
static void ShowMenu()
{
    Console.WriteLine("===== Student Management System =====");
    Console.WriteLine("1. View Students");
    Console.WriteLine("2. Add Student");
    Console.WriteLine("3. Remove Student");
    Console.WriteLine("4. Exit");
}

static void ViewStudents(List<string> students)
{
    Console.WriteLine("\nStudent List:");

    foreach (var student in students)
    {
        Console.WriteLine($"- {student}");
    }
}

static void AddStudent(List<string> students)
{
    Console.Write("Enter student name: ");

    string name = Console.ReadLine()!;

    students.Add(name);

    Console.WriteLine("Student added successfully.");
}

static void RemoveStudent(List<string> students)
{
    Console.Write("Enter student name: ");

    string name = Console.ReadLine()!;

    if (students.Remove(name))
    {
        Console.WriteLine("Student removed.");
    }
    else
    {
        Console.WriteLine("Student not found.");
    }
}

while (isRunning)
{
   
    ShowMenu();
    int choice = int.Parse(Console.ReadLine()!);
    switch (choice)
    {
        case 1:
            ViewStudents(students);
            break;

        case 2:
            AddStudent(students);
            break;

        case 3:
            RemoveStudent(students);
            break;

        case 4:
            isRunning = false;
            break;

        default:
            Console.WriteLine("Invalid option.");
            break;
    }



}
