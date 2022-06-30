using Microsoft.AspNetCore.Mvc;
using NetCoreMultiLanguageMiddleware.Services;

namespace NetCoreMultiLanguageMiddleware.Controllers
{
  public class ServiceController : Controller
  {

    private TransientService t1;
    private TransientService t2;
    private ScopeService s1;
    private ScopeService s2;
    private SingletonService st1;
    private SingletonService st2;

    public ServiceController(TransientService t1, TransientService t2, ScopeService s1, ScopeService s2, SingletonService st1, SingletonService st2)
    {
      this.t1 = t1;
      this.t2 = t2;
      this.s1 = s1;
      this.s2 = s2;
      this.st1 = st1;
      this.st2 = st2;
    }

    public IActionResult Index()
    {

      ViewBag.s1 = s1.Id;
      ViewBag.s2 = s2.Id;

      ViewBag.t1 = t1.Id;
      ViewBag.t2 = t2.Id;

      ViewBag.st1 = st1.Id;
      ViewBag.st2 = st2.Id;

      return View();
    }
  }
}
