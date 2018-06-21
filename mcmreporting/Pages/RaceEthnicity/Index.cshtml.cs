using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace mcmreporting.Pages
{
  public class RaceEthnicitiesModel : PageModel
  {
    public mcmmodels.RaceEthnicities RaceEthnicities { get; set; }

    public void OnGet()
    {
      RaceEthnicities = Csla.DataPortal.Fetch<mcmmodels.RaceEthnicities>();
    }
  }
}