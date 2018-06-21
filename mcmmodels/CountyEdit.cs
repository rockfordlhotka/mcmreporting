using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Csla;

namespace mcmmodels
{
  [Serializable]
  public class CountyEdit : BusinessBase<CountyEdit>
  {
    public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
    public int Id
    {
      get { return GetProperty(IdProperty); }
      private set { LoadProperty(IdProperty, value); }
    }

    public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);
    [Required]
    public string Name
    {
      get { return GetProperty(NameProperty); }
      set { SetProperty(NameProperty, value); }
    }


    private void DataPortal_Fetch(int id)
    {
      var dal = new Dal.Counties();
      var data = dal.Get(id);
      using (BypassPropertyChecks)
      {
        Id = data.Id;
        Name = data.Name;
      }
    }

    protected override void DataPortal_Insert()
    {
      var dal = new Dal.Counties();
      using (BypassPropertyChecks)
      {
        Id = dal.Insert(new Dal.CountyDal { Name = Name });
      }
    }

    protected override void DataPortal_Update()
    {
      var dal = new Dal.Counties();
      using (BypassPropertyChecks)
      {
        dal.Update(new Dal.CountyDal { Id = Id, Name = Name });
      }
    }

    protected override void DataPortal_DeleteSelf()
    {
      DataPortal_Delete(ReadProperty(IdProperty));
    }

    private void DataPortal_Delete(int id)
    {
      var dal = new Dal.Counties();
      dal.Delete(id);
    }

  }
}
