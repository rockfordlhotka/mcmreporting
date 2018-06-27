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
    }
  }
}