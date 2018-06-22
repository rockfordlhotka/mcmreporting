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

    public static readonly PropertyInfo<string> AgencyProperty = RegisterProperty<string>(c => c.Agency);
    public string Agency
    {
      get { return GetProperty(AgencyProperty); }
      private set { LoadProperty(AgencyProperty, value); }
    }

    private void Child_Fetch(int id, string agency)
    {
      Id = id;
      Agency = agency;
    }
  }
}
