using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using mcmreporting.Models;
using mcmmodels;
using Csla;
using System.ComponentModel.DataAnnotations;

namespace mcmreporting.Pages.Cases
{
  public class CreateModel : PageModel
  {
    [BindProperty]
    public CaseEdit CaseEdit { get; set; }
    [BindProperty]
    public List<RaceEthnicityItem> RaceEthnicityList { get; set; } = new List<RaceEthnicityItem>();
    [BindProperty]
    public List<VulnerabilityItem> VulnerabilityList { get; set; } = new List<VulnerabilityItem>();
    [BindProperty]
    public List<CaseLawEnforcementItem> CaseLawEnforcementItemList { get; set; } = new List<CaseLawEnforcementItem>();

    public Counties CountyList { get; set; }
    public Schools SchoolList { get; set; }
    public States StateList { get; set; }
    public RaceEthnicities EthnicityList { get; set; }
    public Vulnerabilities Vulnerabilities { get; set; }
    public mcmmodels.LawEnforcement LawEnforcementAgencies { get; set; }
    public DispositionTypes DispositionTypeList { get; set; }
    public ReferralTypes ReferralTypeList { get; set; }
    public CaseTypes CaseTypeList { get; set; }
    public CaseStatusTypes CaseStatusList { get; set; }
    public GenderTypes GenderTypeList { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
      CountyList = await DataPortal.FetchAsync<Counties>();
      SchoolList = await DataPortal.FetchAsync<Schools>();
      StateList = await DataPortal.FetchAsync<States>();
      EthnicityList = await DataPortal.FetchAsync<RaceEthnicities>();
      Vulnerabilities = await DataPortal.FetchAsync<Vulnerabilities>();
      DispositionTypeList = await DataPortal.FetchAsync<DispositionTypes>();
      ReferralTypeList = await DataPortal.FetchAsync<ReferralTypes>();
      CaseTypeList = await DataPortal.FetchAsync<CaseTypes>();
      CaseStatusList = await DataPortal.FetchAsync<CaseStatusTypes>();
      GenderTypeList = await DataPortal.FetchAsync<GenderTypes>();
      LawEnforcementAgencies = await DataPortal.FetchAsync<mcmmodels.LawEnforcement>();

      foreach (var item in EthnicityList)
        RaceEthnicityList.Add(
          new RaceEthnicityItem { Id = item.Id, Name = item.Name });
      foreach (var item in Vulnerabilities)
        VulnerabilityList.Add(
          new VulnerabilityItem { Id = item.Id, Name = item.Name });
      for (int i = 0; i < 3; i++)
        CaseLawEnforcementItemList.Add(
          new CaseLawEnforcementItem { AgencyId = 0, Denial = false });

      return Page();
    }


    public async Task<IActionResult> OnPostAsync()
    {
      if (!ModelState.IsValid)
      {
        return Page();
      }

      CaseEdit.RaceEthnicityList.AddRange(RaceEthnicityList.Where(item => item.IsChecked).Select(item => item.Id).ToList());
      CaseEdit.VulnerabilityList.AddRange(VulnerabilityList.Where(item => item.IsChecked).Select(item => item.Id).ToList());
      CaseEdit.CaseLawEnforcementList.AddRange(CaseLawEnforcementItemList.Where(item => item.AgencyId > 0).Select(item => new NameValueListBase<int, bool>.NameValuePair(item.AgencyId, item.Denial)));

      CaseEdit = await CaseEdit.SaveAsync();

      return RedirectToPage("./Index");
    }

    public class CaseDisplayData
    {
      [Display(Name = "Name of missing person")]
      public string Name { get; set; }
      [Display(Name = "Location last seen")]
      public string LocationLastSeenCrossSt { get; set; }
      [Display(Name = "Family members searching")]
      public string FamilyMembersInvolvedInSearch { get; set; }
      public string Height { get; set; }
      public string Weight { get; set; }
      [Display(Name = "Hair color")]
      public string HairColor { get; set; }
      [Display(Name = "Hair style")]
      public string HairStyle { get; set; }
      [Display(Name = "Eye color")]
      public string EyeColor { get; set; }
      [Display(Name = "Facial hair")]
      public string FacialHair { get; set; }
      [Display(Name = "Eyebrow features")]
      public string EyebrowFeatures { get; set; }
      [Display(Name = "Glasses or contacts")]
      public string GlassOrContacts { get; set; }
      [Display(Name = "Tattoos")]
      public string Tattoos { get; set; }
      [Display(Name = "Dental characteristics")]
      public string DentalCharacteristics { get; set; }
      [Display(Name = "Scars/Birthmarks")]
      public string ScarsOrBirthmarks { get; set; }
      [Display(Name = "Characteristics notes")]
      public string CharacteristicsNotes { get; set; }
      [Display(Name = "With others")]
      public string WithOthers { get; set; }
      [Display(Name = "High risk activities")]
      public string HighRisk { get; set; }
      [Display(Name = "Disturbing situation")]
      public string DisturbingSituation { get; set; }
      [Display(Name = "Specific questions")]
      public string SpecificQuestions { get; set; }
      [Display(Name = "Jacket/Coat")]
      public string JacketCoat { get; set; }
      [Display(Name = "Shirt/Blouse")]
      public string ShirtBlouse { get; set; }
      [Display(Name = "Pants/Skirt")]
      public string PantsSkirt { get; set; }
      [Display(Name = "Jewelry")]
      public string Jewelry { get; set; }
      [Display(Name = "Backpack/Purse")]
      public string BackpackPurse { get; set; }
      [Display(Name = "Hat/Headwear")]
      public string HatOrHeadwear { get; set; }
      [Display(Name = "Other clothing")]
      public string OtherClothing { get; set; }
      [Display(Name = "Vehicle year")]
      public string VehicleYear { get; set; }
      [Display(Name = "Vehicle make")]
      public string VehicleMake { get; set; }
      [Display(Name = "Vehicle model")]
      public string VehicleModel { get; set; }
      [Display(Name = "Vehicle color")]
      public string VehicleColor { get; set; }
      [Display(Name = "Vehicle license")]
      public string VehicleLicense { get; set; }
      [Display(Name = "Photo available")]
      public bool PhotoAvailable { get; set; }
      [Display(Name = "Left before")]
      public string LeftBefore { get; set; }
      [Display(Name = "Previously found")]
      public string PreviouslyFound { get; set; }
      [Display(Name = "Known companions")]
      public string KnownCompanions { get; set; }
      [Display(Name = "Disability/Mental problems")]
      public string DisabilityMental { get; set; }
      [Display(Name = "Life threatening conditions")]
      public string LifeThreatening { get; set; }
      public string Medications { get; set; }
      public string Abuse { get; set; }
      [Display(Name = "Other history")]
      public string OtherHistory { get; set; }
      [Display(Name = "Name")]
      public string PRAName { get; set; }
      [Display(Name = "Street address")]
      public string PRAStreetAddress { get; set; }
      [Display(Name = "City")]
      public string PRACity { get; set; }
      [Display(Name = "State")]
      public string PRAState { get; set; }
      [Display(Name = "Zipcode")]
      public string PRAZipCode { get; set; }
      [Display(Name = "Home phone")]
      public string PRAHomePhone { get; set; }
      [Display(Name = "Cell phone")]
      public string PRACellPhone { get; set; }
      [Display(Name = "EMail address")]
      public string PRAEmail { get; set; }
    }

    public class RaceEthnicityItem
    {
      public int Id { get; set; }
      public string Name { get; set; }
      public bool IsChecked { get; set; }
    }

    public class VulnerabilityItem
    {
      public int Id { get; set; }
      public string Name { get; set; }
      public bool IsChecked { get; set; }
    }

    public class CaseLawEnforcementItem
    {
      public int AgencyId { get; set; }
      public bool Denial { get; set; }
      public string CaseNumber { get; set; }
      public string LeoName { get; set; }
      public string LeoPhoneNumber { get; set; }
      public string LeoStreetAddress { get; set; }
    }
  }
}