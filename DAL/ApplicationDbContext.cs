using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Domain.ModelsDb;

namespace DAL;

    public class ApplicationDbContext : DbContext
    {

        public DbSet<UserDb> UsersDb { get; set; }

        public DbSet<CategoriesDb> CategoriesDb { get; set; }

        public DbSet<JobsDb> JobsDb { get; set; }
        public DbSet<ApplicationsDb> ApplicationsDb { get; set; }
        public DbSet<CartDb> ArchiveDb { get; set; }

        protected readonly IConfiguration Configuration;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
           
            
        }


    }
