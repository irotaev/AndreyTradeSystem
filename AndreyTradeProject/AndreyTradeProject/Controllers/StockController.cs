using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AndreyTradeProject.Controllers
{
  [AndreyTradeProject.Lib.Authorize]
  public class StockController : AbstractController
  {
    public ActionResult Index()
    {
      return View();
    }
  }
}