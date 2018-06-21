using System;
using System.Collections.Generic;
using System.Text;
using Csla;

namespace mcmmodels
{
  [Serializable]
  public class Schools : ReadOnlyListBase<Schools, SchoolInfo>
  {
    private void DataPortal_Fetch()
    {
      var dal = new Dal.Schools();
      IsReadOnly = false;
      foreach (var item in dal.Get())
        Add(DataPortal.FetchChild<SchoolInfo>(item.Id, item.Name));
      IsReadOnly = true;
    }
  }

  [Serializable]
  public class SchoolInfo : ReadOnlyBase<SchoolInfo>
  {
    public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
    public int Id
    {
      get { return GetProperty(IdProperty); }
      private set { LoadProperty(IdProperty, value); }
    }

    public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);
    public string Name
    {
      get { return GetProperty(NameProperty); }
      private set { LoadProperty(NameProperty, value); }
    }

    private void Child_Fetch(int id, string name)
    {
      Id = id;
      Name = name;
    }
  }
}
