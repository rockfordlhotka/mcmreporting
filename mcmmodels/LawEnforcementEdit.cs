using System;
using System.Collections.Generic;
using System.Text;
using Csla;

namespace mcmmodels
{
  [Serializable]
  public class LawEnforcementEdit : BusinessBase<LawEnforcementEdit>
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
      set { SetProperty(AgencyProperty, value); }
    }

    private void DataPortal_Fetch(int id)
    {
      var dal = new Dal.LawEnforcement();
      var data = dal.Get(id);
      using (BypassPropertyChecks)
      {
        Id = data.Id;
        Agency = data.Agency;
      }
    }

    protected override void DataPortal_Insert()
    {
      var dal = new Dal.LawEnforcement();
      using (BypassPropertyChecks)
      {
        var data = new Dal.AgencyDal { Agency = Agency };
        var newId = dal.Insert(data);
        Id = newId;
      }
    }

    protected override void DataPortal_Update()
    {
      var dal = new Dal.LawEnforcement();
      using (BypassPropertyChecks)
      {
        var data = new Dal.AgencyDal { Id = Id, Agency = Agency };
        dal.Update(data);
      }
    }

    protected override void DataPortal_DeleteSelf()
    {
      DataPortal_Delete(ReadProperty(IdProperty));
    }

    private void DataPortal_Delete(int id)
    {
      var dal = new Dal.LawEnforcement();
      using (BypassPropertyChecks)
      {
        dal.Delete(id);
      }
    }
  }
}
