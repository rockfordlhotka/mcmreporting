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
    public IActionResult OnGet()
    {
      return Page();
    }

    [BindProperty]
    public CaseEdit CaseEdit { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
      if (!ModelState.IsValid)
      {
        return Page();
      }

      CaseEdit = await CaseEdit.SaveAsync();

      return RedirectToPage("./Index");
    }
  }
}