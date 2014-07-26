using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AndreyTradeProject.Controllers
{
  /// <summary>
  /// Контроллер домашней страницы
  /// </summary>
  public class HomeController : AbstractController
  {
    public ActionResult Index()
    {
      return View();
    }
  }
}