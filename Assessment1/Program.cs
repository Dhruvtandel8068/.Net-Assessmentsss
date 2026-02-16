using System;
using System.Collections.Generic;
using System.Linq;

class Student
{
    public int StudentId;
    public string Name;
    public string Department;
    public int Marks;
}

class Program
{
    static void Main()
    {
        List<Student> students = new List<Student>();

        Console.Write("Enter number of students: ");
        int n = int.Parse(Console.ReadLine());


        // 1. Accept student details
        for (int i = 0; i < n; i++)
        {
            Student s = new Student();

            Console.WriteLine($"\nEnter details for student {i + 1}");

            Console.Write("Student ID: ");
            s.StudentId = int.Parse(Console.ReadLine());

            Console.Write("Name: ");
            s.Name = Console.ReadLine();

            Console.Write("Department: ");
            s.Department = Console.ReadLine();

            Console.Write("Marks: ");
            s.Marks = int.Parse(Console.ReadLine());

            students.Add(s);
        }


        // 2. Display all students
        Console.WriteLine("\n--- All Student Records ---");
        foreach (var s in students)
        {
            Console.WriteLine($"{s.StudentId} | {s.Name} | {s.Department} | {s.Marks}");
        }


        // 3. Display only name and department
        Console.WriteLine("\n--- Name & Department ---");
        foreach (var s in students)
        {
            Console.WriteLine($"{s.Name} | {s.Department}");
        }


        // 4. Students with marks > 75
        Console.WriteLine("\n--- Marks Greater Than 75 ---");
        foreach (var s in students.Where(s => s.Marks > 75))
        {
            Console.WriteLine($"{s.Name} | {s.Marks}");
        }


        // 5. Students from specific department
        Console.Write("\nEnter department to search: ");
        string dept = Console.ReadLine();

        Console.WriteLine($"\n--- Students from {dept} Department ---");
        foreach (var s in students.Where(s => s.Department.Equals(dept, StringComparison.OrdinalIgnoreCase)))
        {
            Console.WriteLine($"{s.Name} | {s.Marks}");
        }


        // 6. Sort by marks (descending)
        Console.WriteLine("\n--- Sorted by Marks (Descending) ---");
        var sorted = students.OrderByDescending(s => s.Marks);
        foreach (var s in sorted)
        {
            Console.WriteLine($"{s.Name} | {s.Marks}");
        }


        // 7. Top scorer
        var topper = students.OrderByDescending(s => s.Marks).First();
        Console.WriteLine("\n--- Top Scorer ---");
        Console.WriteLine($"{topper.Name} | {topper.Marks}");
    }
}