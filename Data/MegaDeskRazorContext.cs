using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MegaDeskRazor.Models;

namespace MegaDeskRazor.Models
{
    public class MegaDeskRazorContext : DbContext
    {
        public MegaDeskRazorContext (DbContextOptions<MegaDeskRazorContext> options)
            : base(options)
        {
        }

        public DbSet<MegaDeskRazor.Models.DeskQuote> DeskQuote { get; set; }

        public DbSet<Delivery> Delivery { get; set; }

        public DbSet<Desk> Desk { get; set; }

        public DbSet<DesktopMaterial> DesktopMaterial { get; set; }
    }
}
