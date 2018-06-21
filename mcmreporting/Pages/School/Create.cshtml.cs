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

namespace mcmreporting.Pages.School
{
  public class CreateModel : PageModel
  {
    public IActionResult OnGet()
    {
      return Page();
    }

    [BindProperty]
    public SchoolEdit SchoolEdit { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
      if (!ModelState.IsValid)
      {
        return Page();
      }

      SchoolEdit = await SchoolEdit.SaveAsync();

      return RedirectToPage("./Index");
    }
  }
}