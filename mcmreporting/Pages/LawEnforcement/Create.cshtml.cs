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

namespace mcmreporting.Pages.LawEnforcement
{
  public class CreateModel : PageModel
  {
    public IActionResult OnGet()
    {
      return Page();
    }

    [BindProperty]
    public LawEnforcementEdit LawEnforcementEdit { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
      if (!ModelState.IsValid)
      {
        return Page();
      }

      LawEnforcementEdit = await LawEnforcementEdit.SaveAsync();

      return RedirectToPage("./Index");
    }
  }
}