using Microsoft.AspNetCore.Mvc;

public class GameController : Controller
{
    private static int min = 0;
    private static int n = 10;
    private static int randValue = 0;
    private static int attempts = 0;
    private static Random random = new Random();

    [Route("Set,{min},{n}")]
    public IActionResult Set(int min, int n)
    {
        if (min > n)
        {
            ViewBag.message = "ERROR: minimum value cannot be greater than maximum";
            ViewBag.cssClass = "red";
            return View();
        }

        GameController.n = n;
        GameController.min = min;
        
        ViewBag.message = $"Number range set to: {min} to {n}.";
        ViewBag.cssClass = "blue";
        
        Console.WriteLine($"[LOG] Number range set to: {min} to " + n);

        return View();
    }

    [Route("Draw")]
    public IActionResult Draw()
    {
        randValue = random.Next(min, n+1);
        attempts = 0;

        ViewBag.message = "A number has been drawn. Make your guess!";
        ViewBag.cssClass = "blue";

        Console.WriteLine("[LOG] Number drawn between " + min + " and " + n + ": " + randValue);
        
        return View();
    }

    [Route("Guess,{guess}")]
    public IActionResult Guess(int guess)
    {
        attempts++;
        Console.WriteLine("[LOG] Guess made, attempts: " + attempts);

        if (guess == randValue)
        {
            ViewBag.message = $"Correct! The number was {randValue}. You made {attempts} attempts.";
            ViewBag.cssClass = "green";
        }
        else
        {
            ViewBag.cssClass = "red";

            if (guess < randValue)
            {
                ViewBag.message = $"Too low! Try again. {attempts} attempts so far.";
            }
            else
            {
                ViewBag.message = $"Too high! Try again. {attempts} attempts so far.";
            }
        }
        
        return View();
    }
}