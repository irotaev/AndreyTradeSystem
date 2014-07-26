using ClosedXML.Excel;
using Data;
using NHibernate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace AndreyTradeProject.Lib
{
  public class ExcelFile
  {
    public ExcelFile(Stream stream)
    {
      if (stream == null)
        throw new ArgumentNullException("stream");

      _ExcelStream = stream;
    }

    private readonly Stream _ExcelStream;
    private List<D_Stock> _D_Stocks = new List<D_Stock>();

    public bool IsParsed { get; private set; }
    public IEnumerable<D_Stock> D_Stocks { get { return _D_Stocks.ToList(); } }

    /// <summary>
    /// Спарсить файл.
    /// </summary>
    public void Parse()
    {
      if (IsParsed)
        return;

      using(XLWorkbook workbook = new XLWorkbook(_ExcelStream))
      {
        uint rowIndex = 1;
        string cellValue = String.Format("{0}", workbook.Worksheet(1).Row(1).Cell(1).Value);

        while(!String.IsNullOrEmpty(cellValue))
        {
          cellValue = String.Format("{0}", workbook.Worksheet(1).Row((int)rowIndex++).Cell(1).Value);

          if (!String.IsNullOrEmpty(cellValue))
          {
            D_Stock d_stock = new D_Stock
            {
              Number = cellValue
            };

            _D_Stocks.Add(d_stock);
          }
        }
      }

      IsParsed = true;
    }

    /// <summary>
    /// Сохранить данные из Excel.
    /// </summary>
    public void Save(ISession session)
    {
      if (session == null)
        throw new ArgumentNullException("session");

      if (!IsParsed)
        throw new ApplicationException("Data not parsed");

      foreach(var d_stock in _D_Stocks)
      {
        session.Save(d_stock);
      }
    }
  }
}