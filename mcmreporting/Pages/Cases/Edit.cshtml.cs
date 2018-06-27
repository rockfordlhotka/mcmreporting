using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using mcmmodels;
using Csla;
using System.Collections.Generic;
using System.Linq;

namespace mcmreporting.Pages.Cases
{
  public class EditModel : PageModel
  {
    [BindProperty]
    public CaseEdit CaseEdit { get; set; }
    [BindProperty]
    public List<RaceEthnicityItem> RaceEthnicityList { get; set; } = new List<RaceEthnicityItem>();
    [BindProperty]
    public List<VulnerabilityItem> VulnerabilityList { get; set; } = new List<VulnerabilityItem>();
    [BindProperty]
    public List<CaseLawEnforcementItem> CaseLawEnforcementItemList { get; set; } = new List<CaseLawEnforcementItem>();

    public Counties Counties { get; set; }
    public Schools Schools { get; set; }
    public States States { get; set; }
    public RaceEthnicities RaceEthnicities { get; set; }
    public Vulnerabilities Vulnerabilities { get; set; }
    public mcmmodels.LawEnforcement LawEnforcementAgencies { get; set; }
    public DispositionTypes DispositionTypes { get; set; }
    public ReferralTypes ReferralType { get; set; }
    public CaseTypes CaseTypes { get; set; }
    public CaseStatusTypes CaseStatuses { get; set; }
    public GenderTypes GenderTypes { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      CaseEdit = await DataPortal.FetchAsync<CaseEdit>(id);

      if (CaseEdit == null)
      {
        return NotFound();
      }

      Counties = await DataPortal.FetchAsync<Counties>();
      Schools = await DataPortal.FetchAsync<Schools>();
      States = await DataPortal.FetchAsync<States>();
      RaceEthnicities = await DataPortal.FetchAsync<RaceEthnicities>();
      Vulnerabilities = await DataPortal.FetchAsync<Vulnerabilities>();
      DispositionTypes = await DataPortal.FetchAsync<DispositionTypes>();
      ReferralType = await DataPortal.FetchAsync<ReferralTypes>();
      CaseTypes = await DataPortal.FetchAsync<CaseTypes>();
      CaseStatuses = await DataPortal.FetchAsync<CaseStatusTypes>();
      GenderTypes = await DataPortal.FetchAsync<GenderTypes>();
      LawEnforcementAgencies = await DataPortal.FetchAsync<mcmmodels.LawEnforcement>();

      foreach (var item in RaceEthnicities)
        RaceEthnicityList.Add(
          new RaceEthnicityItem { Id = item.Id, Name = item.Name, IsChecked = CaseEdit.RaceEthnicityList.Count(_ => _ == item.Id) > 0 });
      foreach (var item in Vulnerabilities)
        VulnerabilityList.Add(
          new VulnerabilityItem { Id = item.Id, Name = item.Name, IsChecked = CaseEdit.VulnerabilityList.Count(_ => _ == item.Id) > 0 });
      foreach (var item in CaseEdit.CaseLawEnforcementList)
        CaseLawEnforcementItemList.Add(
          new CaseLawEnforcementItem { AgencyId = item.Key, Denial = item.Value });
      for (int i = CaseEdit.CaseLawEnforcementList.Count; i < 3; i++)
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

      CaseEdit = await CaseEdit.SaveAsync(true);

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
