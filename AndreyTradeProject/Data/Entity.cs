using FluentNHibernate.Mapping;
using NHibernate.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Proxy;

namespace Data
{
  #region Entity
  public abstract class D_BaseObject
  {
    public virtual long Id { get; set; }
    public virtual DateTime CreationDateTime { get; set; }
  }

  public class D_User : D_BaseObject
  {
    public virtual string Name { get; set; }
    public virtual string Surname { get; set; }
    public virtual string Patronimic { get; set; }
  }

  public class D_Stock : D_BaseObject
  {
    public virtual string Number { get; set; }
  }
  #endregion

  #region Mapping
  public class D_BaseObject_Map : ClassMap<D_BaseObject>
  {
    public D_BaseObject_Map()
    {
      Id(x => x.Id).GeneratedBy.HiLo("10").CustomType<Int64>();
      Map(x => x.CreationDateTime).Not.Nullable();
    }
  }

  public class D_User_Map : SubclassMap<D_User>
  {
    public D_User_Map()
    {
      Map(x => x.Name).Nullable().Length(255);
      Map(x => x.Surname).Nullable().Length(255);
      Map(x => x.Patronimic).Nullable().Length(255);
    }
  }

  public class D_Stock_Map : SubclassMap<D_Stock>
  {
    public D_Stock_Map()
    {
      Map(x => x.Number).Not.Nullable().Length(20);
    }
  }
  #endregion

  #region Event listeners
  public class PreInsertEvent : IPreInsertEventListener
  {
    public bool OnPreInsert(NHibernate.Event.PreInsertEvent @event)
    {
      D_BaseObject baseObject = (@event.Entity as D_BaseObject);

      if (baseObject == null)
        return false;

      #region Задаю время создания
      int createdDateTimeIndex = Array.IndexOf(@event.Persister.PropertyNames, "CreationDateTime");

      DateTime creationDate = DateTime.UtcNow;
      @event.State[createdDateTimeIndex] = creationDate;
      baseObject.CreationDateTime = creationDate;
      #endregion

      return false;
    }
  }

  public class PreUpdateEvent : IPreUpdateEventListener
  {
    public bool OnPreUpdate(NHibernate.Event.PreUpdateEvent @event)
    {
      D_BaseObject baseObject = (@event.Entity as D_BaseObject);

      if (baseObject == null)
        return false;

      return false;
    }
  }
  #endregion
}
