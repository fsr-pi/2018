using Microsoft.AspNetCore.Mvc;

namespace Firma.Mvc.ViewComponents
{
  public class NavigationViewComponent : ViewComponent
  {
    public IViewComponentResult Invoke()
    {
      ViewBag.Controller = RouteData?.Values["controller"];
      return View();
    }
  }
}
