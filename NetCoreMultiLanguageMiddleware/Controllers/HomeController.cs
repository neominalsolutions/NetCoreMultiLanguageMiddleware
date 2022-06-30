using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using NetCoreMultiLanguageMiddleware.Models;
using System.Diagnostics;

namespace NetCoreMultiLanguageMiddleware.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private readonly IStringLocalizer<HomeController> _localization;

    public HomeController(ILogger<HomeController> logger, IStringLocalizer<HomeController> localizer)
    {
      _logger = logger;
      _localization = localizer;
    }

    public IActionResult Index()
    {
      ViewBag.Hello = _localization.GetString("Hello");

      return View();
    }

    public IActionResult Privacy()
    {

       throw new Exception("Hata");
  

      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}