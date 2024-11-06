using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using Portfolio.Services;
using System.Threading.Tasks;

namespace Portfolio.Controllers;

public class HomeController : Controller
{
    private readonly EmailService _emailService;

    public HomeController(EmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpPost]
    public async Task<IActionResult> SendEmail(ContactFormModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _emailService.SendEmailAsync(model);
                TempData["SuccessMessage"] = "Your message has been sent. Thank you!";
                return RedirectToAction("Contact");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "There was an error sending your message. Please try again later.";
                Console.WriteLine(ex.Message);
                return View("Contact", model);
            }
        }
        return View("Contact", model);
    }

    
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult About()
    {
        return View();
    }
    public IActionResult Resume()
    {
        return View();
    }
    public IActionResult Projects()
    {
        return View();
    }
    public IActionResult Contact()
    {
        return View();
    }
}