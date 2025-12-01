using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq; 
using ExamplesLinq;



// ====================================================================================
// PROGRAM
// ====================================================================================
class Program
{
    static void Main(string[] args)
    {
        //Zadanie 1
        Console.WriteLine("\n--- Zadanie 1---");

        //TEST
        var students = Generator.GenerateStudentsWithTopicsEasy();
        int chunkSize = 5;
        var res = SortAndGroupStudents(students, chunkSize); 
        DisplayGroupedStudents(res);

        //Zadanie 2
        System.Console.WriteLine("\n--- Zadanie 2---");

        //TEST
        var students2 = Generator.GenerateStudentsWithTopicsEasy();
        var sortedTopics = SortedTopicsByStudentCount(students2);
        DisplaySortedTopics(sortedTopics);

        //TEST
        var students3 = Generator.GenerateStudentsWithTopicsEasy();
        var women = SeparateStudentsByGender(students3).Item1;
        var men = SeparateStudentsByGender(students3).Item2;
        var sortedTopicsWomen = SortedTopicsByStudentCount(women);
        var sortedTopicsMen = SortedTopicsByStudentCount(men);
        Console.WriteLine("\n--- Tematy u kobiet ---");
        DisplaySortedTopics(sortedTopicsWomen);
        Console.WriteLine("\n--- Tematy u mężczyzn ---");
        DisplaySortedTopics(sortedTopicsMen);

        //zadanie 3
        System.Console.WriteLine("\n--- Zadanie 3---");
        
        // TEST
        var students4 = Generator.GenerateStudentsWithTopicsEasy();
        var convertedStudents = ConvertStudentsTopicsToIds(students4); 
        Console.WriteLine($"Tematy studenta 1: {string.Join(", ", convertedStudents.First().TopicIds)}");

        //zadanie 4 
        Console.WriteLine("\n--- Zadanie 4---");

        // A. Tworzenie instancji klasy XYZ za pomocą refleksji
        string className = "XYZ"; 
        Type xyzType = Type.GetType(className);

        if (xyzType == null)
        {
            Console.WriteLine($"Błąd: Nie znaleziono typu '{className}'.");
            return;
        }

        object obj1 = Activator.CreateInstance(xyzType);
        Console.WriteLine($"Utworzono obj1: {obj1.GetType().Name}. Value po refleksji: {((XYZ)obj1).Value}");
        ((XYZ)obj1).Value = 100;

        object obj2 = Activator.CreateInstance(xyzType, new object[] { 50 });
        Console.WriteLine($"Utworzono obj2: {obj2.GetType().Name}. Value po refleksji: {((XYZ)obj2).Value}");

        Console.WriteLine("--------------------------------------");

        // B. Uruchomienie metody z parametrami
        string methodName = "CalculateSum";
        Type[] parameterTypes = new Type[] { typeof(int), typeof(int) };

        MethodInfo sumMethod = xyzType.GetMethod(methodName, parameterTypes);

        if (sumMethod == null)
        {
            Console.WriteLine($"Błąd: Nie znaleziono metody '{methodName}' z podanymi parametrami.");
            return;
        }

        object[] methodArgs = new object[] { 15, 20 }; 
        object resultObj = sumMethod.Invoke(obj1, methodArgs); 

        Console.WriteLine($"Wywołano metodę '{methodName}' na obj1 (Value={((XYZ)obj1).Value}).");
        Console.WriteLine($"Wynik metody (typ object): {resultObj} (Typ: {resultObj.GetType().Name})");

        Console.WriteLine("--------------------------------------");
    
    }   

    // ZADANIE 1
    static List<List<StudentWithTopics>> SortAndGroupStudents(List<StudentWithTopics> students, int n)
    {
        var sortedList = (from s in students
                          orderby s.Name, s.Index descending
                          select s).ToList(); 

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
    // ZADANIE 2
    static List<(string Topic, int StudentCount)> CountStudentsPerTopic(List<StudentWithTopics> students)
    {
        var topicCounts = new Dictionary<string, int>();

        foreach (var student in students)
        {
            foreach (var topic in student.Topics)
            {
                if (topicCounts.ContainsKey(topic))
                {
                    topicCounts[topic]++;
                }
                else
                {
                    topicCounts[topic] = 1;
                }
            }
        }

        return topicCounts.Select(tc => (tc.Key, tc.Value)).ToList();
    }

    static List<string> SortedTopicsByStudentCount(List<StudentWithTopics> students)
    {
        var topicCounts = CountStudentsPerTopic(students);

        return topicCounts
            .OrderByDescending(tc => tc.StudentCount)
            .ThenBy(tc => tc.Topic)
            .Select(tc => tc.Topic)
            .ToList();
    }

    static void DisplaySortedTopics(List<string> topics)
    {
        Console.WriteLine("\n--- Posortowane tematy według liczby studentów ---");
        foreach (var topic in topics)
        {
            Console.WriteLine(topic);
        }
        Console.WriteLine("--------------------------------------");
    }

    static (List<StudentWithTopics> Women, List<StudentWithTopics> Men) SeparateStudentsByGender(List<StudentWithTopics> students)
    {
        var women = students.Where(s => s.Gender == Gender.Female).ToList();
        var men = students.Where(s => s.Gender == Gender.Male).ToList();

        return (Women: women, Men: men);
    }
    
    // ZADANIE 3 
    static List<StudentWithTopicIds> ConvertStudentsTopicsToIds(List<StudentWithTopics> students)
    {
        var convertedStudents = new List<StudentWithTopicIds>();

        foreach (var student in students)
        {
            var studentWithIds = new StudentWithTopicIds
            {
                Id = student.Id,
                Index = student.Index,
                Name = student.Name,
                Gender = student.Gender,      
                Active = student.Active,
                DepartmentId = student.DepartmentId, 
                
                TopicIds = student.Topics
                    .Select(topicName => TopicMapper.GetIdFromName(topicName)) 
                    .Where(id => id != -1) 
                    .ToList()
            }; 

            convertedStudents.Add(studentWithIds); 
        }

        return convertedStudents; 
    }
}

// ====================================================================================
// OTHER CLASSES
// ====================================================================================

class Topic
{
    public int id { get; set; }
    public string name { get; set; }
}

class StudentWithTopicIds
{
    public int Id { get; set; }
    public int Index { get; set; }
    public string Name { get; set; }
    public Gender Gender { get; set; }
    public bool Active { get; set; }
    public int DepartmentId { get; set; }
    public List<int> TopicIds { get; set; } = new List<int>();
}

public static class TopicMapper
{
    public static readonly Dictionary<string, int> TopicNameToId = new Dictionary<string, int>
    {
        {"C#", 0},
        {"algorithms", 1},
        {"fuzzy logic", 2},
        {"web programming", 3},
        {"Basic", 4},
        {"C++", 5},
        {"Java", 6},
        {"JavaScript", 7},
        {"neural networks", 8},
        {"PHP", 9}
    };

    public static int GetIdFromName(string topicName) 
    {
        if (TopicNameToId.TryGetValue(topicName, out int id))
        {
            return id;
        }
        return -1; 
    }
}

public class XYZ
{
    public int Value { get; set; }

    public XYZ()
    {
        this.Value = 0;
    }

    public XYZ(int initialValue)
    {
        this.Value = initialValue;
    }

    public int CalculateSum(int a, int b)
    {
        int sum = a + b + this.Value;
        Console.WriteLine($"[Refleksja] Wartość wewnątrz metody: {sum}");
        return sum;
    }
}