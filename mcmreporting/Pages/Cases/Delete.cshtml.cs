﻿using System;
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
  public class DeleteModel : PageModel
  {
    [BindProperty]
    public CaseEdit CaseEdit { get; set; }

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
      return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      await DataPortal.DeleteAsync<CaseEdit>(id);

      return RedirectToPage("./Index");
    }
  }
}
