using AndreyTradeProject.Models;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AndreyTradeProject.Controllers
{
  public class AutorizeController : AbstractController
  {
    public ActionResult Index(UserModel model)
    {
      model = model ?? new UserModel();

      if (Request.HttpMethod == "POST" && ModelState.IsValid)
      {
        D_User d_user = new D_User
        {
          Name = model.Name,
          Patronimic = model.Patronimic,
          Surname = model.Surname
        };

        _NhibernateSession.Save(d_user);

        #region Авторизация
        HttpContext.Session.Add("Id", d_user.Id);
        #endregion

        if (Request.UrlReferrer != null)
          return Redirect(Request.UrlReferrer.ToString());
      }

      return View(model);
    }
  }
}