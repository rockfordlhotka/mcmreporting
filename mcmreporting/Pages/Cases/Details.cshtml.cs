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
    public string RaceEthnicityText { get; set; }

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

      foreach (var item in CaseEdit.RaceEthnicityList)
        RaceEthnicityText += EthnicityList.Where(r=>r.Id == item).Select(r => r.Name + ", ").First();
      if (!string.IsNullOrWhiteSpace(RaceEthnicityText))
        RaceEthnicityText = RaceEthnicityText.Substring(0, RaceEthnicityText.Length - 2);

      return Page();
    }
  }
}
