using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementASPDotNet.Data;
using StudentManagementASPDotNet.Models;
namespace StudentManagementASPDotNet.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
        private readonly UserDataDBContext _dbContext;

    public HomeController(ILogger<HomeController> logger, UserDataDBContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> CreateAccount(UserDetails userDetails)
    {
        try
        {
            var userCount = await _dbContext.userdetails.CountAsync();
            userDetails.id = "USR00" + (userCount + 1);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        return View(userDetails);
    }


    [HttpPost]
    public async Task<IActionResult> SaveAccount(UserDetails userDetails)
    {
        try
        {
            userDetails.role = "USER";
            await _dbContext.userdetails.AddAsync(userDetails);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return View("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Login(UserDetails userDetails)
    {
        var userInfo = await _dbContext.userdetails.AsNoTracking().FirstOrDefaultAsync(e =>
            e.email == userDetails.email && e.password == userDetails.password);
        if (userInfo == null)
        {
            return View("Index");
        }

        return Redirect("/User/UserDashboard");
        
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

