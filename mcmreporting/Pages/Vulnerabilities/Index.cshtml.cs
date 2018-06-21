﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace mcmreporting.Pages
{
  public class VulnerabilitiesModel : PageModel
  {
    public mcmmodels.Vulnerabilities Vulnerabilities { get; set; }

    public void OnGet()
    {
      Vulnerabilities = Csla.DataPortal.Fetch<mcmmodels.Vulnerabilities>();
    }
  }
}