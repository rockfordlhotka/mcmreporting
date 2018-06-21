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

namespace mcmreporting.Pages.School
{
  public class DetailsModel : PageModel
  {
    public SchoolEdit SchoolEdit { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      SchoolEdit = await DataPortal.FetchAsync<SchoolEdit>(id);

      if (SchoolEdit == null)
      {
        return NotFound();
      }
      return Page();
    }
  }
}
