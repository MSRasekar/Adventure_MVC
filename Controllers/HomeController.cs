

     

// Controllers/HomeController.cs
using Adventure_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AdventureEventDbContext _dbContext;

    public HomeController(ILogger<HomeController> logger, AdventureEventDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    // Other actions...

    [HttpPost]
    public IActionResult Register([FromBody] User newUser)
    {
        if (ModelState.IsValid)
        {
            // Check if the username or email is already registered
            if (_dbContext.Users.Any(u => u.Username == newUser.Username || u.Email == newUser.Email))
            {
                ModelState.AddModelError("RegistrationError", "Username or email already exists.");
                return View("Registration");
            }

            // Hash the password before storing it (you should use a secure password hashing library)
            // For simplicity, I'm using a plain text password here.
            newUser.Password = newUser.Password;

            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();

            // Redirect to login or wherever you want after successful registration
            return RedirectToAction("Login");
        }

        return View("Registration", newUser);
    }

    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        // Check if the user exists in the database
        var user = _dbContext.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

        if (user != null)
        {
            // Redirect to the main page or wherever you want after successful login
            return RedirectToAction("Index");
        }

        // If login fails, return to the login page with an error message
        ModelState.AddModelError("LoginError", "Invalid username or password.");
        return View("Login");
    }



    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Events()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }
    public IActionResult Registration()
    {
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}


