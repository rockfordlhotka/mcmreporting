using System;
using System.Collections.Generic;
using System.Text;
using Csla;

namespace mcmmodels
{
  [Serializable]
  public class LawEnforcement : ReadOnlyListBase<LawEnforcement, LawEnforcementInfo>
  {
    private void DataPortal_Fetch()
    {
      var dal = new Dal.LawEnforcement();
      IsReadOnly = false;
      foreach (var item in dal.Get())
        Add(DataPortal.FetchChild<LawEnforcementInfo>(item.Id, item.Agency));
      IsReadOnly = true;
    }
  }

  [Serializable]
  public class LawEnforcementInfo : ReadOnlyBase<LawEnforcementInfo>
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
