using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
  public sealed class NhibernateConfiguration
  {
    private NhibernateConfiguration()
    {
      var msSqlConfigurator = MsSqlConfiguration.MsSql2008.ShowSql();

      msSqlConfigurator.ConnectionString(c => c.FromConnectionStringWithKey("MsSQL2008"));

      SessionFactory = Fluently.Configure()
        .Database(msSqlConfigurator)
        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<NhibernateConfiguration>())
        .ExposeConfiguration(cfg =>
        {
          cfg.EventListeners.PreInsertEventListeners = new NHibernate.Event.IPreInsertEventListener[] { new PreInsertEvent() };
          cfg.EventListeners.PreUpdateEventListeners = new NHibernate.Event.IPreUpdateEventListener[] { new PreUpdateEvent() };
          new NHibernate.Tool.hbm2ddl.SchemaUpdate(cfg).Execute(true, true);
        })
        .BuildSessionFactory();
    }

    public readonly ISessionFactory SessionFactory;
    private readonly static object _Locker = new { };

    private static NhibernateConfiguration _DefaultObject;

    public static NhibernateConfiguration Default
    {
      get
      {
        if (_DefaultObject == null)
        {
          lock (_Locker)
          {
            _DefaultObject = new NhibernateConfiguration();
          }
        }

        return _DefaultObject;
      }
    }
  }
}
