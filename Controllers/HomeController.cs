using Adventure_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AdventureEventDbContext _dbContext;

    public HomeController(ILogger<HomeController> logger, AdventureEventDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }
    public IActionResult Index()
    {
        return View();
    }
    [HttpGet]
    public IActionResult Register()
    {
        var viewModel = new RegistrationDTO();

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Register(RegistrationDTO registrationDto)
    {
        if (ModelState.IsValid)
        {
            // Check if the username or email is already registered
            if (_dbContext.Users.Any(u => u.Username == registrationDto.Username || u.Email == registrationDto.Email))
            {
                ModelState.AddModelError("RegistrationError", "Username or email already exists.");
                return View("Register");
            }

            // Create a new User from the RegistrationDTO
            var newUser = new User
            {
                Username = registrationDto.Username,
                Email = registrationDto.Email,
                Mobile = registrationDto.Mobile,
                Password = registrationDto.Password // For security, use a secure password hashing method here
            };

            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();

            // Redirect to login or wherever you want after successful registration
            return RedirectToAction("Login");
        }

        return View("Register");
    }


    [HttpGet]
    public IActionResult Login()
    {
        var viewModel = new LoginDTO();

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Login(LoginDTO loginDto)
    {
        // Check if the user exists in the database
        var user = _dbContext.Users.FirstOrDefault(u => u.Username == loginDto.Username && u.Password == loginDto.Password);

        if (user != null)
        {
            // Redirect to the main page or wherever you want after successful login
            return RedirectToAction("Index");
        }

        // If login fails, return to the login page with an error message
        ModelState.AddModelError("LoginError", "Invalid username or password.");
        return View("Login");
    }


    public IActionResult Events()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}


