using System;
using System.Collections.Generic;
using System.Text;
using HivelyCoreMVC.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HivelyCoreMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
          
        }


        public DbSet<Queen> Queens { get; set; }
        public DbSet<Hive> Hives { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<WorkerBee> WorkerBees { get; set; }
    }
}
