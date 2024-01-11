using System;
using System.Collections.Generic;
using System.IO;

public class Teacher
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string ClassSection { get; set; }

    public override string ToString()
    {
        return $"ID: {ID}, Name: {Name}, Class and Section: {ClassSection}";
    }
}

public class TeacherDataManager
{
    private const string FilePath = @"C:\Users\priscilla glory\Desktop\project\Rainbow_school\Rainbow_school\teacher_data.txt";

    public List<Teacher> ReadTeachers()
    {
        List<Teacher> teachers = new List<Teacher>();

        try
        {
            if (File.Exists(FilePath))
            {
                using (StreamReader reader = new StreamReader(FilePath))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] data = line.Split(',');

                        Teacher teacher = new Teacher
                        {
                            ID = int.Parse(data[0]),
                            Name = data[1],
                            ClassSection = data[2]
                        };

                        teachers.Add(teacher);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading file: {ex.Message}");
        }

        return teachers;
    }

    public void WriteTeachers(List<Teacher> teachers)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(FilePath))
            {
                foreach (Teacher teacher in teachers)
                {
                    writer.WriteLine($"{teacher.ID},{teacher.Name},{teacher.ClassSection}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing file: {ex.Message}");
        }
    }

    public void AddTeacher(Teacher newTeacher)
    {
        List<Teacher> teachers = ReadTeachers();
        teachers.Add(newTeacher);
        WriteTeachers(teachers);
    }

    public void UpdateTeacher(int teacherID, Teacher updatedTeacher)
    {
        List<Teacher> teachers = ReadTeachers();
        Teacher existingTeacher = teachers.Find(t => t.ID == teacherID);

        if (existingTeacher != null)
        {
            existingTeacher.Name = updatedTeacher.Name;
            existingTeacher.ClassSection = updatedTeacher.ClassSection;
            WriteTeachers(teachers);
        }
        else
        {
            Console.WriteLine($"Teacher with ID {teacherID} not found.");
        }
    }
}

namespace Rainbow_school
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TeacherDataManager teacherManager = new TeacherDataManager();
            List<Teacher> teachers = teacherManager.ReadTeachers();

            Console.WriteLine("Existing Teachers:");
            foreach (Teacher teacher in teachers)
            {
                Console.WriteLine(teacher);
            }

            Teacher newTeacher = new Teacher
            {
                ID = 104,
                Name = "Charu",
                ClassSection = "Math-A"
            };

            // Add a new teacher
            teacherManager.AddTeacher(newTeacher);

            Console.WriteLine("\nTeachers after adding a new teacher:");
            foreach (Teacher teacher in teacherManager.ReadTeachers())
            {
                Console.WriteLine(teacher);
            }

            // Update an existing teacher
            int teacherIDToUpdate = 104;
            Teacher updatedTeacher = new Teacher
            {
                ID = teacherIDToUpdate,
                Name = "UpdatedCharu",
                ClassSection = "UpdatedMath-A"
            };
            teacherManager.UpdateTeacher(teacherIDToUpdate, updatedTeacher);

            Console.WriteLine("\nTeachers after updating a teacher:");
            foreach (Teacher teacher in teacherManager.ReadTeachers())
            {
                Console.WriteLine(teacher);
            }

            Console.ReadKey();
        }
    }
}
