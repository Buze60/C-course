using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public class StudentService : IRepository<Student>
{
    private readonly List<Student> students = new();

    public event Action<Student>? StudentAdded;

    public void Add(Student student)
    {
        students.Add(student);

        StudentAdded?.Invoke(student);
    }


    public List<Student> GetAll()
    {
        return students;
    }

    public Student? FindById(int id)
    {
        return students.FirstOrDefault(s => s.Id == id);
    }

    public void Delete(int id)
    {
        var student = FindById(id);

        if (student != null)
            students.Remove(student);
    }

    public async Task SaveAsync(string path)
    {
        List<string> lines = students
            .Select(s => $"{s.Id},{s.Name},{s.Age}")
            .ToList();

        await File.WriteAllLinesAsync(path, lines);
    }

    public async Task LoadAsync(string path)
    {
        if (!File.Exists(path))
            return;

        string[] lines = await File.ReadAllLinesAsync(path);

        students.Clear();

        foreach (var line in lines)
        {
            string[] parts = line.Split(',');

            students.Add(new Student
            {
                Id = int.Parse(parts[0]),
                Name = parts[1],
                Age = int.Parse(parts[2])
            });
        }
    }
}