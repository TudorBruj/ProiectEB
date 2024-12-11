using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProiectEB.Models;

namespace ProiectEB.Data
{
    public class ProiectEBContext : IdentityDbContext<IdentityUser>
    {
        public ProiectEBContext(DbContextOptions<ProiectEBContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Client { get; set; }
        public DbSet<ProiectEB.Models.Comanda> Comanda { get; set; } = default!;
        public DbSet<ProiectEB.Models.Produs> Produs { get; set; } = default!;
        public DbSet<ProiectEB.Models.Recenzie> Recenzie { get; set; } = default!;
        public DbSet<ProiectEB.Models.Stoc> Stoc { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produs>()
                .HasOne(p => p.Stoc) 
                .WithOne(s => s.Produs)       
                .HasForeignKey<Stoc>(s => s.IdProdus) 
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Recenzie>()
                .HasOne(r => r.Client)
                .WithMany()
                .HasForeignKey(r => r.IdClient)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Recenzie>()
                .HasOne(r => r.Produs)
                .WithMany(p => p.Recenzii)
                .HasForeignKey(r => r.IdProdus)
                .OnDelete(DeleteBehavior.Cascade);


            base.OnModelCreating(modelBuilder);
        }
    }
}
