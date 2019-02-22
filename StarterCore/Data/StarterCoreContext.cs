using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace StarterCore.Data
{
    public class StarterCoreContext : DbContext
    {
        public StarterCoreContext(DbContextOptions<StarterCoreContext> options)
            : base(options)
        {
            
        }
    }
}
