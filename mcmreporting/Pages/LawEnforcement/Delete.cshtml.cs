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

namespace mcmreporting.Pages.LawEnforcement
{
  public class DeleteModel : PageModel
  {
    [BindProperty]
    public LawEnforcementEdit LawEnforcementEdit { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      LawEnforcementEdit = await DataPortal.FetchAsync<LawEnforcementEdit>(id);

      if (LawEnforcementEdit == null)
      {
        return NotFound();
      }
      return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      await DataPortal.DeleteAsync<LawEnforcementEdit>(id);

      return RedirectToPage("./Index");
    }
  }
}
