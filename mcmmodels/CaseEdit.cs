using System;
using System.ComponentModel.DataAnnotations;
using Csla;
using Csla.Core;

namespace mcmmodels
{
  [Serializable]
  public class CaseEdit : BusinessBase<CaseEdit>
  {
    public CaseEdit()
    {
      RaceEthnicityList = new MobileList<int>();
    }

    public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
    public int Id
    {
      get { return GetProperty(IdProperty); }
      private set { LoadProperty(IdProperty, value); }
    }

    public static readonly PropertyInfo<string> MCMNumberProperty = RegisterProperty<string>(c => c.MCMNumber);
    [Display(Name="MCM number")]
    [Required]
    public string MCMNumber
    {
      get { return GetProperty(MCMNumberProperty); }
      set { SetProperty(MCMNumberProperty, value); }
    }

    public static readonly PropertyInfo<DateTime> IntakeDateProperty = RegisterProperty<DateTime>(c => c.IntakeDate);
    [Display(Name = "Intake date")]
    [DataType(DataType.Date)]
    [Required]
    public DateTime IntakeDate
    {
      get { return GetProperty(IntakeDateProperty); }
      set { SetProperty(IntakeDateProperty, value); }
    }

    public static readonly PropertyInfo<DateTime> LastSeenProperty = RegisterProperty<DateTime>(c => c.LastSeen);
    [Display(Name = "Last seen date")]
    [DataType(DataType.Date)]
    public DateTime LastSeen
    {
      get { return GetProperty(LastSeenProperty); }
      set { SetProperty(LastSeenProperty, value); }
    }

    public static readonly PropertyInfo<DateTime> ReportedMissingProperty = RegisterProperty<DateTime>(c => c.ReportedMissing);
    [Display(Name = "Reported missing date")]
    [DataType(DataType.Date)]
    public DateTime ReportedMissing
    {
      get { return GetProperty(ReportedMissingProperty); }
      set { SetProperty(ReportedMissingProperty, value); }
    }

    public static readonly PropertyInfo<int> AgeProperty = RegisterProperty<int>(c => c.Age);
    public int Age
    {
      get { return GetProperty(AgeProperty); }
      set { SetProperty(AgeProperty, value); }
    }

    public static readonly PropertyInfo<string> GenderProperty = RegisterProperty<string>(c => c.Gender);
    public string Gender
    {
      get { return GetProperty(GenderProperty); }
      set { SetProperty(GenderProperty, value); }
    }

    public static readonly PropertyInfo<int> PeopleServedProperty = RegisterProperty<int>(c => c.PeopleServed);
    [Display(Name = "People served")]
    public int PeopleServed
    {
      get { return GetProperty(PeopleServedProperty); }
      set { SetProperty(PeopleServedProperty, value); }
    }

    public static readonly PropertyInfo<string> CityProperty = RegisterProperty<string>(c => c.City);
    public string City
    {
      get { return GetProperty(CityProperty); }
      set { SetProperty(CityProperty, value); }
    }

    public static readonly PropertyInfo<int> CountyIdProperty = RegisterProperty<int>(c => c.CountyId);
    public int CountyId
    {
      get { return GetProperty(CountyIdProperty); }
      set { SetProperty(CountyIdProperty, value); }
    }

    public static readonly PropertyInfo<string> StateProperty = RegisterProperty<string>(c => c.State);
    public string State
    {
      get { return GetProperty(StateProperty); }
      set { SetProperty(StateProperty, value); }
    }

    public static readonly PropertyInfo<string> StartCaseTypeProperty = RegisterProperty<string>(c => c.StartCaseType);
    [Display(Name = "Start case type")]
    public string StartCaseType
    {
      get { return GetProperty(StartCaseTypeProperty); }
      set { SetProperty(StartCaseTypeProperty, value); }
    }

    public static readonly PropertyInfo<string> EndCaseTypeProperty = RegisterProperty<string>(c => c.EndCaseType);
    [Display(Name = "End case type")]
    public string EndCaseType
    {
      get { return GetProperty(EndCaseTypeProperty); }
      set { SetProperty(EndCaseTypeProperty, value); }
    }

    public static readonly PropertyInfo<string> DispositionProperty = RegisterProperty<string>(c => c.Disposition);
    public string Disposition
    {
      get { return GetProperty(DispositionProperty); }
      set { SetProperty(DispositionProperty, value); }
    }

    public static readonly PropertyInfo<DateTime> CloseDateProperty = RegisterProperty<DateTime>(c => c.CloseDate);
    [Display(Name = "Close date")]
    [DataType(DataType.Date)]
    public DateTime CloseDate
    {
      get { return GetProperty(CloseDateProperty); }
      set { SetProperty(CloseDateProperty, value); }
    }

    public static readonly PropertyInfo<string> ReferralTypeProperty = RegisterProperty<string>(c => c.ReferralType);
    [Display(Name = "Referral type")]
    public string ReferralType
    {
      get { return GetProperty(ReferralTypeProperty); }
      set { SetProperty(ReferralTypeProperty, value); }
    }

    public static readonly PropertyInfo<string> CaseStatusProperty = RegisterProperty<string>(c => c.CaseStatus);
    [Display(Name = "Case status")]
    public string CaseStatus
    {
      get { return GetProperty(CaseStatusProperty); }
      set { SetProperty(CaseStatusProperty, value); }
    }

    public static readonly PropertyInfo<int> SchoolIdProperty = RegisterProperty<int>(c => c.SchoolId);
    public int SchoolId
    {
      get { return GetProperty(SchoolIdProperty); }
      set { SetProperty(SchoolIdProperty, value); }
    }

    public static readonly PropertyInfo<string> CountyProperty = RegisterProperty<string>(c => c.County);
    public string County
    {
      get { return GetProperty(CountyProperty); }
      private set { LoadProperty(CountyProperty, value); }
    }

    public static readonly PropertyInfo<string> SchoolProperty = RegisterProperty<string>(c => c.School);
    public string School
    {
      get { return GetProperty(SchoolProperty); }
      private set { LoadProperty(SchoolProperty, value); }
    }

    public static readonly PropertyInfo<MobileList<int>> RaceEthnicityListProperty = RegisterProperty<MobileList<int>>(c => c.RaceEthnicityList);
    public MobileList<int> RaceEthnicityList
    {
      get { return GetProperty(RaceEthnicityListProperty); }
      private set { LoadProperty(RaceEthnicityListProperty, value); }
    }

    private void DataPortal_Fetch(int id)
    {
      var dal = new Dal.Cases();
      var data = dal.Get(id);
      using (BypassPropertyChecks)
      {
        Csla.Data.DataMapper.Map(data, this);
        RaceEthnicityList.Clear();
        var raceEthnicityList = dal.GetRaceEthnicity(id);
        if (raceEthnicityList != null)
          RaceEthnicityList.AddRange(raceEthnicityList);
      }

      var cdal = new Dal.Counties();
      var c = cdal.Get(CountyId);
      if (c != null)
        County = c.Name;
      else
        County = "n/a";

      var sdal = new Dal.Schools();
      var s = sdal.Get(SchoolId);
      if (s != null)
        School = s.Name;
      else
        School = "n/a";
    }

    private static readonly string[] ignoreList = new string[] { "RaceEthnicityList", "County", "School", "Parent", "BrokenRulesCollection",
      "IsValid", "IsSelfValid", "IsNew", "IsDirty", "IsDeleted", "IsSelfDirty", "IsBusy", "IsSelfBusy", "IsSavable", "IsChild" };

    protected override void DataPortal_Insert()
    {
      var dal = new Dal.Cases();
      using (BypassPropertyChecks)
      {
        var data = new Dal.CaseDal();
        Csla.Data.DataMapper.Map(this, data, ignoreList);
        var newId = dal.Insert(data, RaceEthnicityList);
        Id = newId;
      }
    }

    protected override void DataPortal_Update()
    {
      var dal = new Dal.Cases();
      using (BypassPropertyChecks)
      {
        var data = new Dal.CaseDal();
        Csla.Data.DataMapper.Map(this, data, ignoreList);
        dal.Update(data, RaceEthnicityList);
      }
    }

    protected override void DataPortal_DeleteSelf()
    {
      DataPortal_Delete(ReadProperty(IdProperty));
    }

    private void DataPortal_Delete(int id)
    {
      var dal = new Dal.Cases();
      using (BypassPropertyChecks)
      {
        dal.Delete(id);
      }
    }
  }
}
