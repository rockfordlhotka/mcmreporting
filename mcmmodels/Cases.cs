﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Csla;

namespace mcmmodels
{
  [Serializable]
  public class Cases : ReadOnlyListBase<Cases,CaseInfo>
  {
    private void DataPortal_Fetch()
    {
      var dal = new Dal.Cases();
      IsReadOnly = false;
      foreach (var item in dal.Get())
        Add(DataPortal.FetchChild<CaseInfo>(item));
      IsReadOnly = true;
    }
  }

  [Serializable]
  public class CaseInfo : ReadOnlyBase<CaseInfo>
  {
    public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
    public int Id
    {
      get { return GetProperty(IdProperty); }
      private set { LoadProperty(IdProperty, value); }
    }

    public static readonly PropertyInfo<string> MCMNumberProperty = RegisterProperty<string>(c => c.MCMNumber);
    public string MCMNumber
    {
      get { return GetProperty(MCMNumberProperty); }
      private set { LoadProperty(MCMNumberProperty, value); }
    }

    public static readonly PropertyInfo<DateTime> IntakeDateProperty = RegisterProperty<DateTime>(c => c.IntakeDate);
    [Display(Name = "Intake date")]
    [DataType(DataType.Date)]
    public DateTime IntakeDate
    {
      get { return GetProperty(IntakeDateProperty); }
      private set { LoadProperty(IntakeDateProperty, value); }
    }

    public static readonly PropertyInfo<DateTime> LastSeenProperty = RegisterProperty<DateTime>(c => c.LastSeen);
    [Display(Name = "Last seen date")]
    [DataType(DataType.Date)]
    public DateTime LastSeen
    {
      get { return GetProperty(LastSeenProperty); }
      private set { LoadProperty(LastSeenProperty, value); }
    }

    public static readonly PropertyInfo<DateTime> ReportedMissingProperty = RegisterProperty<DateTime>(c => c.ReportedMissing);
    [Display(Name = "Reported missing date")]
    [DataType(DataType.Date)]
    public DateTime ReportedMissing
    {
      get { return GetProperty(ReportedMissingProperty); }
      private set { LoadProperty(ReportedMissingProperty, value); }
    }

    public static readonly PropertyInfo<int> AgeProperty = RegisterProperty<int>(c => c.Age);
    public int Age
    {
      get { return GetProperty(AgeProperty); }
      private set { LoadProperty(AgeProperty, value); }
    }

    public static readonly PropertyInfo<string> GenderProperty = RegisterProperty<string>(c => c.Gender);
    public string Gender
    {
      get { return GetProperty(GenderProperty); }
      private set { LoadProperty(GenderProperty, value); }
    }

    public static readonly PropertyInfo<int> PeopleServedProperty = RegisterProperty<int>(c => c.PeopleServed);
    [Display(Name = "People served")]
    public int PeopleServed
    {
      get { return GetProperty(PeopleServedProperty); }
      private set { LoadProperty(PeopleServedProperty, value); }
    }

    public static readonly PropertyInfo<string> CityProperty = RegisterProperty<string>(c => c.City);
    public string City
    {
      get { return GetProperty(CityProperty); }
      private set { LoadProperty(CityProperty, value); }
    }

    public static readonly PropertyInfo<int> CountyIdProperty = RegisterProperty<int>(c => c.CountyId);
    public int CountyId
    {
      get { return GetProperty(CountyIdProperty); }
      private set { LoadProperty(CountyIdProperty, value); }
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

    public static readonly PropertyInfo<string> StateProperty = RegisterProperty<string>(c => c.State);
    public string State
    {
      get { return GetProperty(StateProperty); }
      private set { LoadProperty(StateProperty, value); }
    }

    public static readonly PropertyInfo<string> StartCaseTypeProperty = RegisterProperty<string>(c => c.StartCaseType);
    [Display(Name = "Start case type")]
    public string StartCaseType
    {
      get { return GetProperty(StartCaseTypeProperty); }
      private set { LoadProperty(StartCaseTypeProperty, value); }
    }

    public static readonly PropertyInfo<string> EndCaseTypeProperty = RegisterProperty<string>(c => c.EndCaseType);
    [Display(Name = "End case type")]
    public string EndCaseType
    {
      get { return GetProperty(EndCaseTypeProperty); }
      private set { LoadProperty(EndCaseTypeProperty, value); }
    }

    public static readonly PropertyInfo<string> DispositionProperty = RegisterProperty<string>(c => c.Disposition);
    public string Disposition
    {
      get { return GetProperty(DispositionProperty); }
      private set { LoadProperty(DispositionProperty, value); }
    }

    public static readonly PropertyInfo<DateTime> CloseDateProperty = RegisterProperty<DateTime>(c => c.CloseDate);
    [Display(Name = "Close date")]
    [DataType(DataType.Date)]
    public DateTime CloseDate
    {
      get { return GetProperty(CloseDateProperty); }
      private set { LoadProperty(CloseDateProperty, value); }
    }

    public static readonly PropertyInfo<string> ReferralTypeProperty = RegisterProperty<string>(c => c.ReferralType);
    [Display(Name = "Referral type")]
    public string ReferralType
    {
      get { return GetProperty(ReferralTypeProperty); }
      private set { LoadProperty(ReferralTypeProperty, value); }
    }

    public static readonly PropertyInfo<string> CaseStatusProperty = RegisterProperty<string>(c => c.CaseStatus);
    [Display(Name = "Case status")]
    public string CaseStatus
    {
      get { return GetProperty(CaseStatusProperty); }
      private set { LoadProperty(CaseStatusProperty, value); }
    }

    public static readonly PropertyInfo<int> SchoolIdProperty = RegisterProperty<int>(c => c.SchoolId);
    public int SchoolId
    {
      get { return GetProperty(SchoolIdProperty); }
      private set { LoadProperty(SchoolIdProperty, value); }
    }

    private void Child_Fetch(Dal.CaseDal data)
    {
      Id = data.Id;
      MCMNumber = data.MCMNumber;
      IntakeDate = data.IntakeDate;
      LastSeen = data.LastSeen;
      ReportedMissing = data.ReportedMissing;
      Age = data.Age;
      Gender = data.Gender;
      PeopleServed = data.PeopleServed;
      City = data.City;
      CountyId = data.CountyId;
      State = data.State;
      StartCaseType = data.StartCaseType;
      EndCaseType = data.EndCaseType;
      Disposition = data.Disposition;
      CloseDate = data.CloseDate;
      ReferralType = data.ReferralType;
      CaseStatus = data.CaseStatus;
      SchoolId = data.SchoolId;

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
  }
}
