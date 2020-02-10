using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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
        public DbSet<Photos> Photos { get; set; }
        public DbSet<Comments> Comments { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("TravelerPhoto"));
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<Trips>()
            //    .HasData(new {
            //        Id = 1,
            //        Title = "Seting",
            //        Description = "Seting",
            //        StartDate = DateTime.Now,
            //        EndDate = DateTime.Now,
            //        CreationDate = DateTime.Now,
            //        Actived = false
            //    });
            builder.Entity<Trips>()
                    .Property(p => p.Id);

            base.OnModelCreating(builder);
        }
    }
}
