using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RBApplicationCore80.Models;
namespace RBApplicationCore80.Data
{
    //public class ApplicationDbContext:DbContext
    //{
    //    public ApplicationDbContext(DbContextOptions options) : base(options)
    //    {
    //    }
    //    public DbSet<SalesLeadEntity> SalesLead { get; set; }
    //}

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<SalesLeadEntity> SalesLead { get; set; }
    //public class ApplicationDbContext:DbContext
public DbSet<RBApplicationCore80.Models.Employee> Employee { get; set; } = default!;
    //public class ApplicationDbContext:DbContext
public DbSet<RBApplicationCore80.Models.Occupation> Occupation { get; set; } = default!;
    }


}
