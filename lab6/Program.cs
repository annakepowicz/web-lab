//zad1
using System.Globalization;

void printTupleItem((string name, string surname, int age, double salary) tuple)
{
    Console.WriteLine("--- Metoda 1: Dostęp za pomocą ItemX ---");
    Console.WriteLine($"Imię: {tuple.Item1}"); 
    Console.WriteLine($"Nazwisko: {tuple.Item2}");
    Console.WriteLine($"Wiek: {tuple.Item3}");
    Console.WriteLine($"Płaca: {tuple.Item4}");

    Console.WriteLine("\n--- Metoda 2: Dostęp za pomocą nazw elementów (zalecane) ---");
    Console.WriteLine($"Imię: {tuple.name}"); 
    Console.WriteLine($"Nazwisko: {tuple.surname}");
    Console.WriteLine($"Wiek: {tuple.age}");
    Console.WriteLine($"Płaca: {tuple.salary}");

    Console.WriteLine("\n--- Metoda 3: Dekonstrukcja krotki ---");
    var (firstName, lastName, personAge, monthlySalary) = tuple;
    Console.WriteLine($"Imię: {firstName}");
    Console.WriteLine($"Nazwisko: {lastName}");
    Console.WriteLine($"Wiek: {personAge}");
    Console.WriteLine($"Płaca: {monthlySalary}");
}

var myTuple = ("Jan", "Kowalski", 45, 7850.50);
System.Console.WriteLine("\n--- Zadanie 1---");
printTupleItem(myTuple);


//zad2
var @class = new { name = "Jan", surname = "Kowalski", age = 45, salary = 6200.75};
System.Console.WriteLine("\n--- Zadanie 2---");
System.Console.WriteLine($"Imię: {@class.name}");
System.Console.WriteLine($"Nazwisko: {@class.surname}");


//zad3
System.Console.WriteLine("\n--- Zadanie 3---");

int[] numbers = { 5, 2, 8, 1, 9, 3, 7 };
Console.WriteLine($"Array: {string.Join(", ", numbers)}");

bool exists8 = Array.Exists(numbers, n => n == 8);
Console.WriteLine($"Exists(8): {exists8}");

int index9 = Array.IndexOf(numbers, 9);
Console.WriteLine($"IndexOf(9): {index9}");

Array.Sort(numbers);
Console.WriteLine($"Sort: {string.Join(", ", numbers)}");

Array.Reverse(numbers);
Console.WriteLine($"Reverse: {string.Join(", ", numbers)}");

int[] greaterThan5 = Array.FindAll(numbers, n => n > 5);
Console.WriteLine($"FindAll > 5: {string.Join(", ", greaterThan5)}");


//zad4
System.Console.WriteLine("\n--- Zadanie 4---");

// Definicja metody generycznej.
void printAnonymousItem<T>(T anonymousType) where T : class
{
    var type = anonymousType.GetType();
    foreach (var prop in type.GetProperties())
    {
        var value = prop.GetValue(anonymousType);
        Console.WriteLine($"{prop.Name}: {value}");
    }
}

var myAnonymousPerson = new 
{ 
    Name = "John", 
    Surname = "Smith", 
    Age = 42, 
    Salary = 9500.75
};

printAnonymousItem(myAnonymousPerson);


//zad5
System.Console.WriteLine("\n--- Zadanie 5---");

void drawCard(string firstLine,
    string secondLine = " ",
    string border = "X", 
    int borderWidth = 1, 
    int width = 21)
{
    int spaceLenFirst = width - 2 * borderWidth - firstLine.Length;
    int spaceLenSecond = width - 2 * borderWidth - secondLine.Length;
    if (spaceLenFirst < 0 || spaceLenSecond < 0)
    {
        Console.WriteLine("Error: Text is too long for the specified width and border.");
        return;
    }

    writeBorder(borderWidth, width, border);

    for (int i = 0; i < 2; i++)
    {
        writeLine(i == 0 ? firstLine : secondLine, border, borderWidth, i == 0 ? spaceLenFirst : spaceLenSecond);
    }

    writeBorder(borderWidth, width, border);


    
}

void writeBorder(int borderWidth, int width, string border)
{
    for (int i = 0; i < borderWidth; i++)
    {
        for (int j = 0; j < width; j++)
        {
            Console.Write(border);
        }
        Console.WriteLine();
    }
}

void writeLine(string text, string border, int borderWidth, int spaceLen)
{
    for (int j = 0; j < borderWidth; j++)
    {
        Console.Write(border);
    }

    int index = spaceLen;

    for ( ; index > spaceLen/2; index --)
    {
        Console.Write(" ");
    }

    Console.Write(text);

    for ( ; index > 0; index --)
    {
        Console.Write(" ");
    }

    for (int j = 0; j < borderWidth; j++)
    {
        Console.Write(border);
    }
    Console.WriteLine();
}

Console.WriteLine("--- 1. Wywołanie z wszystkimi parametrami ---");
drawCard("Ryszard", "Ryś", "X", 2, 20);

Console.WriteLine("\n--- 2. Wywołanie tylko z parametrem wymaganym (domyślne wartości) ---");
drawCard("linia");

Console.WriteLine("\n--- 3. Wywołanie z pominięciem parametru drugiego i użyciem nazwanych ---");
drawCard("Napis", border: "=", borderWidth: 3);

Console.WriteLine("\n--- 4. Wywołanie z użyciem parametrów nazwanych w innej kolejności ---");
drawCard(
    firstLine: "Super", 
    width: 40, 
    borderWidth: 1, 
    secondLine: "Produkt"
);

//zad6
System.Console.WriteLine("\n--- Zadanie 6---");

bool isEven(int number) => number % 2 == 0;
bool isDoubleAboveZero(double number) => number > 0;
bool isStringLengthGreaterThan5(string str) => str.Length >= 5;  

(int evenInts, int positiveDoubles, int longStrings, int others) countMyTypes(params object[] args)
{
    int evenInts = 0;
    int positiveDoubles = 0;
    int longStrings = 0;
    int others = 0;

    foreach (var item in args)
    {
        switch (item)
        {
            case int i when isEven(i):
                evenInts++;
                break;
            case double d when isDoubleAboveZero(d):
                positiveDoubles++;
                break;
            case string s when isStringLengthGreaterThan5(s):
                longStrings++;
                break;
            default:
                others++;
                break;
        }
    }
    return (evenInts, positiveDoubles, longStrings, others);
}

var result = countMyTypes(2, 3, -4, 2.5, -1.5, "hello", "abc", true, null);

Console.WriteLine($"Parzyste int: {result.evenInts}");
Console.WriteLine($"Dodatnie double: {result.positiveDoubles}");
Console.WriteLine($"String >=5: {result.longStrings}");
Console.WriteLine($"Inne typy: {result.others}");


