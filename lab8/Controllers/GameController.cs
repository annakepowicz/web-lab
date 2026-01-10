using Microsoft.AspNetCore.Mvc;

public class GameController : Controller
{
    private int min
    {
        get => HttpContext.Session.GetInt32("min") ?? 0;
        set => HttpContext.Session.SetInt32("min", value);
    }

    private int n
    {
        get => HttpContext.Session.GetInt32("n") ?? 10;
        set => HttpContext.Session.SetInt32("n", value);
    }

    private int randValue
    {
        get => HttpContext.Session.GetInt32("randValue") ?? 0;
        set => HttpContext.Session.SetInt32("randValue", value);
    }

    private int attempts
    {
        get => HttpContext.Session.GetInt32("attempts") ?? 0;
        set => HttpContext.Session.SetInt32("attempts", value);
    }
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

        this.n = n;
        this.min = min;
        
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