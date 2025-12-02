using System.Data.Common;
using Microsoft.AspNetCore.Mvc;

public class ToolController : Controller
{
    public static double[] Count(double a, double b, double c)
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

    [Route("Tool/Solve/{a}/{b}/{c}")]
    public IActionResult Solve(double a, double b, double c)
    {
        double[] results = Count(a, b, c);

        double numOfRoots = results[0];
        double x1 = results[1];
        double x2 = results[2];

        string message = "";
        string cssClass = "";

        if (numOfRoots == 0)
        {
            message = "No real solutions.";
            cssClass = "red";
        }
        else if (numOfRoots == 1)
        {
            message = $"One solution: x = {x1:F2}"; 
            cssClass = "blue";
        }
        else if (numOfRoots == 2)
        {
            message = $"Two solutions: x1 = {x1:F2}; x2 = {x2:F2}";
            cssClass = "green";
        }
        else if (numOfRoots == -1)
        {
            message = "Infinite number of solutions.";
            cssClass = "yellow";
        }

        ViewBag.Message = message;
        ViewBag.CssClass = cssClass;
        

        string signB = b >= 0 ? "+" : ""; // Dodajemy plus tylko jak liczba dodatnia
        string signC = c >= 0 ? "+" : "";
        ViewBag.Equation = $"{a}x² {signB}{b}x {signC}{c} = 0";

        return View();
    }
}
