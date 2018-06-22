using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using mcmmodels;
using Csla;
using System.Collections.Generic;

namespace mcmreporting.Pages.Cases
{
  public class EditModel : PageModel
  {
    [BindProperty]
    public CaseEdit CaseEdit { get; set; }

    public Counties CountyList { get; set; }
    public Schools SchoolList { get; set; }
    public States StateList { get; set; }
    public RaceEthnicities EthnicityList { get; set; }
    public DispositionTypes DispositionTypeList { get; set; }
    public ReferralTypes ReferralTypeList { get; set; }
    public CaseTypes CaseTypeList { get; set; }
    public CaseStatusTypes CaseStatusList { get; set; }

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

      CountyList = await DataPortal.FetchAsync<Counties>();
      SchoolList = await DataPortal.FetchAsync<Schools>();
      StateList = await DataPortal.FetchAsync<States>();
      EthnicityList = await DataPortal.FetchAsync<RaceEthnicities>();
      DispositionTypeList = await DataPortal.FetchAsync<DispositionTypes>();
      ReferralTypeList = await DataPortal.FetchAsync<ReferralTypes>();
      CaseTypeList = await DataPortal.FetchAsync<CaseTypes>();
      CaseStatusList = await DataPortal.FetchAsync<CaseStatusTypes>();

      return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
      if (!ModelState.IsValid)
      {
        return Page();
      }

      CaseEdit = await CaseEdit.SaveAsync(true);

      return RedirectToPage("./Index");
    }
  }
}
