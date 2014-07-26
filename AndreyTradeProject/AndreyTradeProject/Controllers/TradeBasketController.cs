using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AndreyTradeProject.Controllers
{
  public class TradeBasketController : AbstractController
  {
    public ActionResult Browse()
    {
      return View();
    }
  }
}