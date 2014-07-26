using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AndreyTradeProject.Controllers
{
  public class ExcelController : AbstractController
  {
    public ActionResult Import()
    {
     if (Request.HttpMethod == "POST")
      {
        HttpPostedFileBase file = Request.Files["excelFile"];

        if (file != null)
        {
          try
          {
            var excelFile = new AndreyTradeProject.Lib.ExcelFile(file.InputStream);
            excelFile.Parse();
            excelFile.Save(_NhibernateSession);
          }
          catch (Exception ex)
          {
            throw new ApplicationException(String.Format("Во время парсинга excel файла произошла ошибка {0}", ex));
          }
        }
      }

      return View();
    }
  }
}