//Purpose: This file contains the DesignTimeDbContextFactory class used for creating the AppDbContext at design time.
// It is typically used by Entity Framework Core tools during migrations and scaffolding.

using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;

namespace CarWash.DAL.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;Database=CGCarWashDB;Trusted_Connection=True;",
                x => x.UseNetTopologySuite() // For geospatial support
            );

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}