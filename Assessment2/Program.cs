using System;
using System.Collections.Generic;
using System.Linq;
class Student
{
    public int StudentId { get; set; }
    public string Name { get; set; }
    public string Department { get; set; }
    public int Year { get; set; }
    public int Marks { get; set; }

    public Student(int studentId, string name, string department, int year, int marks)
    {
        StudentId = studentId;
        Name = name;
        Department = department;
        Year = year;
        Marks = marks;
    }

    public void Display()
    {
        Console.WriteLine($"{StudentId} | {Name} | {Department} | Year {Year} | Marks {Marks}");
    }
}

class Program
{
    static void Main()
    {
        List<Student> students = new List<Student>
        {
            new Student(1, "Dhruv", "IT", 3, 88),
            new Student(2, "Yash", "CS", 2, 76),
            new Student(3, "Krish", "IT", 4, 92),
            new Student(4, "Taksh", "EC", 1, 69),
            new Student(5, "Aryan", "CS", 3, 81)
        };

        Console.WriteLine("\n--- All Student Records ---");
        foreach (var s in students)
        {
            s.Display();
        }

        Console.WriteLine("\n--- Students with Marks > 75 ---");
        var highScorers = students.Where(s => s.Marks > 75);
        foreach (var s in highScorers)
        {
            s.Display();
        }


        Console.WriteLine("\n--- Students Sorted by Marks (Descending) ---");
        var sortedStudents = students.OrderByDescending(s => s.Marks);
        foreach (var s in sortedStudents)
        {
            s.Display();
        }


        Console.WriteLine("\n--- Top 3 Scorers ---");
        var topThree = students.OrderByDescending(s => s.Marks).Take(3);
        foreach (var s in topThree)
        {
            s.Display();
        }

        Console.ReadLine();
    }
}
