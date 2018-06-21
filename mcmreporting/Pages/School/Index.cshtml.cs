using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace mcmreporting.Pages
{
  public class SchoolsModel : PageModel
  {
    public mcmmodels.Schools Schools { get; set; }

    public void OnGet()
    {
      Schools = Csla.DataPortal.Fetch<mcmmodels.Schools>();
    }
  }
}