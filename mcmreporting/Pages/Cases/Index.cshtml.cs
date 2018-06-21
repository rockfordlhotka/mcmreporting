using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace mcmreporting.Pages
{
  public class CasesModel : PageModel
  {
    public mcmmodels.Cases Cases { get; set; }

    public void OnGet()
    {
      Cases = Csla.DataPortal.Fetch<mcmmodels.Cases>();
    }
  }
}