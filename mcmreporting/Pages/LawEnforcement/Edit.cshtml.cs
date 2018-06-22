using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mcmreporting.Models;
using mcmmodels;
using Csla;

namespace mcmreporting.Pages.LawEnforcement
{
  public class EditModel : PageModel
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

    public async Task<IActionResult> OnPostAsync()
    {
      if (!ModelState.IsValid)
      {
        return Page();
      }

      LawEnforcementEdit = await LawEnforcementEdit.SaveAsync(true);

      return RedirectToPage("./Index");
    }
  }
}
