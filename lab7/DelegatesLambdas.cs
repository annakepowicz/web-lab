using System;
using System.Collections.Generic;

namespace DelegatesLambdas
{
    public class Person(string name, int age)
    {
        public string Name { get; set; } = name;
        public int Age { get; set; } = age;

        public override string ToString()
        {
            return $"{Name}, {Age}";
        }
    }

    class Program
    {
        public delegate bool LessThan(Person a, Person b);

        public static void BubbleSort(Person[] arr, LessThan lessThan)
        {
            for (int i = arr.Length - 1; i > 0; i--)
                for (int j = 0; j < i; j++)
                    if (!lessThan(arr[j], arr[j + 1]))
                    {
                        (arr[j + 1], arr[j]) = (arr[j], arr[j + 1]);
                    }
        }

        public static bool LessName(Person x, Person y)
        {
            return x.Name.CompareTo(y.Name) < 0;
        }

        public static bool LessAge(Person x, Person y)
        {
            return x.Age < y.Age;
        }

        public static void ShowPerson(string comment, Person[] arr)
        {
            Console.WriteLine(comment);
            foreach (Person p in arr)
                Console.WriteLine(p);
        }
        public static void BubbleSortTest()
        {
            Person[] arr = [new Person("Nowak", 45), new Person("Nowak", 30), new Person("Kowal", 40), new Person("Wojtas", 30)];
            BubbleSort(arr, LessName);
            ShowPerson("by name:", arr);
            BubbleSort(arr, LessAge);
            ShowPerson("by age:", arr);
        }

        public static void BubbleSortLambda1Test()
        {
            Person[] arr = [new Person("Nowak", 45), new Person("Nowak", 30), new Person("Kowal", 40), new Person("Wojtas", 30)];
            BubbleSort(arr, (Person x, Person y) => { return x.Name.CompareTo(y.Name) < 0; });
            ShowPerson("by name:", arr);
            BubbleSort(arr, (x, y) => { return x.Age < y.Age; });
            ShowPerson("by age:", arr);

        }
        public static void BubbleSortLambda2Test()
        {
            Person[] arr = [new Person("Nowak", 45), new Person("Nowak", 30), new Person("Kowal", 40), new Person("Wojtas", 30)];
            BubbleSort(arr, (Person x, Person y) => x.Name.CompareTo(y.Name) < 0);
            ShowPerson("by name:", arr);
            BubbleSort(arr, (x, y) => x.Age < y.Age);
            ShowPerson("by age:", arr);
        }

        class Counter
        {
            public int InnerCounter { get; set; }
        }

        static Action CreateLamda(Counter x)
        {
            Action action;
            action = () => { Console.WriteLine(x.InnerCounter); };
            return action;
        }
        static void TestReturnLamda()
        {
            Counter c = new() { InnerCounter = 5 };
            Action ac1 = CreateLamda(c);
            c.InnerCounter++;
            Action ac2 = CreateLamda(c);
            ac1();
            ac2();
        }

        static void TestForX()
        {
            var items = new string[] { "Bolek", "Lolek", "Tola" };
            var actions = new List<Action>();
            for (int i = 0; i < items.Length; i++)
            {
                actions.Add(() => { Console.WriteLine(items[i]); });
            }
            foreach (Action action in actions)
                action();
        }

        static void TestFor()
        {
            var items = new string[] { "Bolek", "Lolek", "Tola" };
            var actions = new List<Action>();
            string item;
            for (int i = 0; i < items.Length; i++)
            {
                item = items[i];
                actions.Add(() => { Console.WriteLine(item); });
            }
            foreach (Action action in actions)
                action();
        }

        static void TestForIn()
        {
            var items = new string[] { "Bolek", "Lolek", "Tola" };
            var actions = new List<Action>();
            for (int i = 0; i < items.Length; i++)
            {
                string item;
                item = items[i];
                actions.Add(() => { Console.WriteLine(item); });
            }
            foreach (Action action in actions)
                action();
        }

        static void TestForeach()
        {
            var items = new string[] { "Bolek", "Lolek", "Tola" };
            var actions = new List<Action>();
            foreach (string item in items)
                actions.Add(() => { Console.WriteLine(item); });
            foreach (Action action in actions)
                action();
        }


        static void Main()
        {
            BubbleSortTest();
            //testForX();
            TestFor();
            TestForIn();
            TestForeach();
            BubbleSortLambda2Test();
            TestReturnLamda();
        }




    }
}
