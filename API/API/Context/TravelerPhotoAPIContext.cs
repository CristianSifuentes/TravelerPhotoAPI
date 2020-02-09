using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace API.Context
{
    public class TravelerPhotoAPIContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public TravelerPhotoAPIContext(IConfiguration configuration, DbContextOptions options) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Trips> Trips { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("TravelerPhoto"));
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Trips>()
                .HasData(new {
                    Id = 1,
                    Title = "Seting",
                    Description = "Seting",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    CreationDate = DateTime.Now,
                    Actived = false
                });
        }
    }
}
