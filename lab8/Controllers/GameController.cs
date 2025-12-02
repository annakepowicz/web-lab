using System.Data.Common;
using Microsoft.AspNetCore.Mvc;

public class GameController : Controller
{
    private static Random random = new Random();

    [Route("Set,{n}")]
    public IActionResult Set(int n)
    {
        TempData["n"] = n;
        ViewBag.message = $"Number range set to: 1 to {n}.";
        ViewBag.cssClass = "blue";
        return View();
    }

    [Route("Draw")]
    public IActionResult Draw()
    {
        if (TempData["n"] == null) return RedirectToAction("Set", new { n = 10 });

        int range = (int)TempData["n"];
        int number = random.Next(1, range + 1);
        int n = (int)TempData["n"];
        TempData["attempts"] = 0;
        Console.WriteLine($"[LOG] Wywołano metodę Draw! Wylosowana liczba: {number}");
        Console.WriteLine($"[LOG] Zakres liczb to 1 do {n}");

        ViewBag.message = "A number has been drawn. Make your guess!";
        ViewBag.cssClass = "blue";

        TempData["number"] = number;
        TempData["n"] = n; 

        return View();
    }

    [Route("Guess,{guess}")]
    public IActionResult Guess(int guess)
    {
        if (TempData["number"] == null)
        {
            return Content("Gra nie została rozpoczęta. Użyj /Draw aby zacząć.");
        }

        int number = (int)TempData["number"];
        int attempts = (int)TempData["attempts"];
        Console.WriteLine($"[LOG] Wywołano metodę Guess! Liczba prób przed zmianą: {TempData["attempts"]}");

        if (guess == number)
        {
            ViewBag.message = $"Correct! The number was {number}.";
            ViewBag.cssClass = "green";

            TempData.Remove("number");
            TempData.Remove("attempts");
        }
        else
        {
            attempts++; 
            TempData["attempts"] = attempts;
            ViewBag.cssClass = "red";

            if (guess < number) ViewBag.message = $"Too low! Try again. {attempts} attempts so far.";
            else ViewBag.message = $"Too high! Try again. {attempts} attempts so far.";

            TempData.Keep("number");
            TempData.Keep("n");
        }
        return View();
    }

}