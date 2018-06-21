using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace mcmreporting.Pages
{
  public class CountiesModel : PageModel
  {
    public mcmmodels.Counties Counties { get; set; }

    public void OnGet()
    {
      Counties = Csla.DataPortal.Fetch<mcmmodels.Counties>();
    }
  }
}