using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProiectEB.Models;

namespace ProiectEB.Data
{
    public class ProiectEBContext : DbContext
    {
        public ProiectEBContext (DbContextOptions<ProiectEBContext> options)
            : base(options)
        {
        }

        public DbSet<ProiectEB.Models.Client> Client { get; set; } = default!;
        public DbSet<ProiectEB.Models.Comanda> Comanda { get; set; } = default!;
        public DbSet<ProiectEB.Models.Produs> Produs { get; set; } = default!;
        public DbSet<ProiectEB.Models.Recenzie> Recenzie { get; set; } = default!;
        public DbSet<ProiectEB.Models.Stoc> Stoc { get; set; } = default!;
    }
}
