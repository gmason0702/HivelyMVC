using System;
using System.Collections.Generic;
using System.Text;
using HivelyCoreMVC.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HivelyCoreMVC.Models.HiveModels;
using HivelyCoreMVC.Models.ImageUploadModels;

namespace HivelyCoreMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
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
        public DbSet<HivelyCoreMVC.Models.HiveModels.HiveListItem> HiveListItem { get; set; }
        public DbSet<ImageUpload> Images { get; set; }
        
    }
}
