using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Web.Data
{
    public class EvolveContext :DbContext
    {
        public EvolveContext(DbContextOptions<EvolveContext> options):base(options)
        {

        }
        public DbSet<CalculationHeader> CalculationHeaders { get; set; }
        public DbSet<CalculationResult> CalculationResults { get; set; }

    }
}
