namespace Collections
{
    class Program
    {
        public static void QueueProbe()
        {
            Queue<string> queueS = new();
            Console.WriteLine("Queue:");
            queueS.Enqueue("one");
            queueS.Enqueue("two");
            queueS.Enqueue("three");
            foreach (string s in queueS)
                Console.WriteLine(s);
            while (queueS.Count != 0)
                Console.WriteLine(queueS.Dequeue());
            Console.WriteLine("Queue after dequeu:");
            foreach (string s in queueS)
                Console.WriteLine(s);
        }

        public static void DictionaryProbe()
        {
            var dict = new Dictionary<int, string>();
            dict.Add(5, "Aleksy");
            dict.Add(3, "John");
            dict.Add(8, "Proxy");
            dict.Add(4, "cat");
            Console.WriteLine("dictionary:");
            foreach (var elem in dict)
                Console.WriteLine(elem);
            Console.WriteLine("keys:");
            foreach (var elem in dict.Keys)
                Console.WriteLine(elem);
            Console.WriteLine("access to one elem by a key (very fast):");
            Console.WriteLine(dict.GetValueOrDefault(8));
            Console.WriteLine(dict.GetValueOrDefault(1));
            Console.WriteLine("Remove() returns:" + dict.Remove(5));
            Console.WriteLine("TryAdd() returns:" + dict.TryAdd(8, "nonproxy"));
        }

        public static void Print<T>(IEnumerable<T> items)
        {
            foreach (T item in items)
                Console.WriteLine(item);
        }
        public static void InitiatorStringCollection()
        {
            List<string> dayOfWeek = [
                "Monday",
                "Tuesday",
                "Wednesday",
                "Thursday",
                "Friday",
                "Saturday",
                "Sunday",
            ];
            Print(dayOfWeek);
        }

        public static void InitiatorDictionary()
        {
            // before C# 6.0
            Dictionary<string, int> personAgeMapOld = new (){
                { "John" , 23 },
                { "Alice", 30 },
                { "Fred", 50 }
            };
            // from C# 6.0
            Dictionary<string, int> personAgeMap = new ()
            {
                ["John"] = 23,
                ["Alice"] = 30,
                ["Fred"] = 50
            };
            Print(personAgeMapOld);
            Console.WriteLine("----------------------");
            Print(personAgeMap);
        }

        public static void CollectionProbe()
        {
            QueueProbe();
            DictionaryProbe();
            InitiatorStringCollection();
            InitiatorDictionary();
        }
        static void Main()
        {
            CollectionProbe();
        }
    }

}
