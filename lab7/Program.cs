using System;
using System.Collections.Generic;
using System.Linq; 
using ExamplesLinq;


static List<List<StudentWithTopics>> SortAndGroupStudents(List<StudentWithTopics> students, int n)
{
    var sortedList = (from s in students
                      orderby s.Name, s.Index descending
                      select s).ToList(); // Materializacja do List, aby móc iterować i używać Skip/Take

    List<List<StudentWithTopics>> result = new List<List<StudentWithTopics>>();

    for (int i = 0; i < sortedList.Count; i += n)
    {
        var group = sortedList.Skip(i).Take(n).ToList();
        result.Add(group);
    }

    return result;
}

static void DisplayGroupedStudents(List<List<StudentWithTopics>> groupedStudents)
{
    Console.WriteLine("\n--- Wyniki pogrupowanych studentów ---");
    int groupCount = 1;

    if (groupedStudents == null || groupedStudents.Count == 0)
    {
        Console.WriteLine("Brak wyników do wyświetlenia.");
        return;
    }

    foreach (var group in groupedStudents)
    {
        Console.WriteLine($"\n=== Grupa {groupCount++} (Liczba studentów: {group.Count}) ===");
        
        foreach (var student in group)
        {
            Console.WriteLine($"   {student}");
        }
    }
    Console.WriteLine("--------------------------------------");
}

//TEST
var students = Generator.GenerateStudentsWithTopicsEasy();
int chunkSize = 5;
var res = SortAndGroupStudents(students, chunkSize);
DisplayGroupedStudents(res);