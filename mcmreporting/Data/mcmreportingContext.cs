using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using mcmreporting.Pages.School;

namespace mcmreporting.Models
{
    public class mcmreportingContext : DbContext
    {
        public mcmreportingContext (DbContextOptions<mcmreportingContext> options)
            : base(options)
        {
        }

    }
}
