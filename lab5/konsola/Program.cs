using System.Data.Common;

public static class Program
{
    public static double[] count(double a, double b, double c)
    {
        double delta, x1 = 0, x2 = 0, numOfRoots = 0;

        if (a != 0)
        {
            if (b == 0 && c == 0)
            {
                numOfRoots = 1;
                x1 = 0;
                return new double[] { numOfRoots, x1, x2 };
            }
            delta = b * b - 4 * a * c;
            if (delta < 0)
            {
                numOfRoots = 0;
            }
            else if (delta == 0)
            {
                x1 = -b / (2 * a);
                numOfRoots = 1;
            }
            else
            {
                x1 = (-b - Math.Sqrt(delta)) / (2 * a);
                x2 = (-b + Math.Sqrt(delta)) / (2 * a);
                numOfRoots = 2;
            }
        }
        else
        {
            if (b != 0)
            {
                numOfRoots = 1;

                if (c == 0) x1 = 0;
                else x1 = -c / b;
            }
            else
            {
                if( c == 0)
                {
                    // Infinite number of solutions
                    numOfRoots = -1; // Indicate infinite solutions
                }
                else numOfRoots = 0;
            }
        }
        return new double[] { numOfRoots, x1, x2 };
        
    }
    public static void Main(string[] args)
    {
        double a, b, c;
        double[] results = new double[3];
        Console.Write("Input a:");
        a = Convert.ToDouble(Console.ReadLine());
        Console.Write("Input b:");
        b = Convert.ToDouble(Console.ReadLine());
        Console.Write("Input c:");
        c = Convert.ToDouble(Console.ReadLine());

        results = count((int)a, (int)b, (int)c);
        double numOfRoots = results[0];
        double x1 = results[1];
        double x2 = results[2];

        if (numOfRoots == 0)
        {
            Console.WriteLine("No real roots.");
        }
        else if (numOfRoots == 1)
        {
            Console.WriteLine("One real root: x =  {0}", results[1]);
        }
        else if (numOfRoots == 2)
        {
            Console.WriteLine($"Two real roots: x1 = {x1} , x2 = {x2}");
        }
        else
        {
            Console.WriteLine("Infinite number of solutions.");
        }
    }
}
