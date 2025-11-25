using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Reflection
{
    class Person
    {
        [DisplayName("Month Salary")]
        public int Salary { get; set; }

        [Obsolete]
        public string Show(string comment, int starsNo)
        {
            string addStr = "";
            for (int i = 0; i < starsNo; i++)
                addStr += '*';
            return comment + addStr + Salary + addStr;
        }
    }

    class Program
    {
        public static void NameDemo()
        {
            Console.WriteLine(nameof(NameDemo));
            object[] values = { "str", false, 100, 3.14, 'z' };
            foreach (var value in values)
                Console.WriteLine("{0} - has type's name {1}, FullName={2}", value,
                                  value.GetType().Name, value.GetType().FullName);
            Console.WriteLine("----------------");
        }

        public static void InvokeDemo()
        {
            Console.WriteLine(nameof(InvokeDemo));
            string str = "big demonstration";
            MethodInfo methodInfo = str.GetType().GetMethod("Substring", new Type[] { typeof(int), typeof(int) });
            // metoda "Substring" z dwoma parametrami typu "int"
            string result = (string)methodInfo.Invoke(str, new object[] { 4, 4 });
            Console.WriteLine($"Result = {result}");
            Console.WriteLine("----------------");
        }



        public static void Demo()
        {
            Console.WriteLine(nameof(Demo));
            string str = "demo";
            Assembly assembly = str.GetType().Assembly;
            Console.WriteLine($" - Assembly GetName() is {assembly.GetName()}");
            Console.WriteLine($" - Assembly FullName is {assembly.FullName}");
            Console.WriteLine($" - Assembly CodeBase is {assembly.CodeBase}");
            Console.WriteLine($" - Assembly ToString() is {assembly.ToString()}");
            Console.WriteLine("----------------");
        }


        public static void DataTypeReflection()
        {
            Console.WriteLine(nameof(DataTypeReflection));
            DateTime dataType = new DateTime();
            Type type = dataType.GetType();
            foreach (System.Reflection.PropertyInfo property in type.GetProperties())
            {
                Console.WriteLine(property.Name);
            }
            Console.WriteLine("----------------");
        }

        public static void BaseInfo()
        {
            Console.WriteLine(nameof(BaseInfo));
            Person person = new Person();
            Type info = person.GetType();
            Console.WriteLine($"Name: {info.Name}");
            Console.WriteLine($"IsPublic: {info.IsPublic}");
            Console.WriteLine($"BaseType : {info.BaseType}");
            var props = info.GetProperties();
            Console.WriteLine($"Properties :");

            foreach (var prop in props)
            {
                Console.WriteLine($"  Name : {prop.Name}");
                Console.WriteLine($"  Name : {prop.PropertyType.Name}");
            }

            Console.WriteLine($"Methods :");
            var meths = info.GetMethods();

            foreach (var meth in meths)
            {
                Console.WriteLine($"  Name : {meth.Name}");
                Console.WriteLine($"  Params amount : {meth.GetParameters().Length}");  // number of parameter
            }
            Console.WriteLine("----------------");
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Start");
            DataTypeReflection();
            NameDemo();
            BaseInfo();
            Demo(); // ???
            InvokeDemo();

        }
    }
}
