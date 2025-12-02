using Microsoft.AspNetCore.Mvc;

public class GameController : Controller
{
    private static int n = 10;
    private static int randValue = 0;
    private static int attempts = 0;
    private static Random random = new Random();

    [Route("Set,{n}")]
    public IActionResult Set(int n)
    {
        GameController.n = n;
        
        ViewBag.message = $"Number range set to: 0 to {n}.";
        ViewBag.cssClass = "blue";
        
        Console.WriteLine("[LOG] Number range set to: 0 to " + n);

        return View();
    }

    [Route("Draw")]
    public IActionResult Draw()
    {
        randValue = random.Next(0, n);
        attempts = 0;

        ViewBag.message = "A number has been drawn. Make your guess!";
        ViewBag.cssClass = "blue";

        Console.WriteLine("[LOG] Number drawn between 0 and " + n + ": " + randValue);
        
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