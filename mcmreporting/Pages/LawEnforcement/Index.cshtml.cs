using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace mcmreporting.Pages
{
  public class LawEnforcementModel : PageModel
  {
    public mcmmodels.LawEnforcement LawEnforcement { get; set; }

    public void OnGet()
    {
      LawEnforcement = Csla.DataPortal.Fetch<mcmmodels.LawEnforcement>();
    }
  }
}