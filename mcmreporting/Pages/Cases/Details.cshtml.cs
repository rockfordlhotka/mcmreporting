using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using mcmreporting.Models;
using mcmmodels;
using Csla;

namespace mcmreporting.Pages.Cases
{
  public class DetailsModel : PageModel
  {
    public CaseEdit CaseEdit { get; set; }
    public RaceEthnicities EthnicityList { get; set; }
    public Vulnerabilities Vulnerabilities { get; set; }
    public mcmmodels.LawEnforcement LawEnforcementAgencies { get; set; }
    public string RaceEthnicityText { get; set; }
    public string VulnerabilityText { get; set; }
    public List<CaseLawEnforcementItem> Agencies { get; set; }

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

      EthnicityList = await DataPortal.FetchAsync<RaceEthnicities>();
      Vulnerabilities = await DataPortal.FetchAsync<Vulnerabilities>();
      LawEnforcementAgencies = await DataPortal.FetchAsync<mcmmodels.LawEnforcement>();

      SetRaceEthnicityText();
      SetVulnerabilityText();
      SetAgencyList();

      return Page();
    }

    private void SetAgencyList()
    {
      Agencies = new List<CaseLawEnforcementItem>();
      Agencies.AddRange(CaseEdit.CaseLawEnforcementList.Select(r => new CaseLawEnforcementItem {
          DenialText = r.Value ? "DENIED" : "Not denied",
          AgencyName = LawEnforcementAgencies.Where(i => i.Id == r.Key).FirstOrDefault()?.Agency }));
    }

    private void SetVulnerabilityText()
    {
      foreach (var item in CaseEdit.VulnerabilityList)
        VulnerabilityText += Vulnerabilities.Where(r => r.Id == item).Select(r => r.Name + ", ").First();
      if (!string.IsNullOrWhiteSpace(VulnerabilityText))
        VulnerabilityText = VulnerabilityText.Substring(0, VulnerabilityText.Length - 2);
      if (string.IsNullOrWhiteSpace(VulnerabilityText))
        VulnerabilityText = "n/a";
    }

    private void SetRaceEthnicityText()
    {
      foreach (var item in CaseEdit.RaceEthnicityList)
        RaceEthnicityText += EthnicityList.Where(r => r.Id == item).Select(r => r.Name + ", ").First();
      if (!string.IsNullOrWhiteSpace(RaceEthnicityText))
        RaceEthnicityText = RaceEthnicityText.Substring(0, RaceEthnicityText.Length - 2);
      if (string.IsNullOrWhiteSpace(RaceEthnicityText))
        RaceEthnicityText = "n/a";
    }

    public class CaseLawEnforcementItem
    {
      public string AgencyName { get; set; }
      public string DenialText { get; set; }
    }
  }
}
