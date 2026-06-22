using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace auth_app.Data;

public class AppDbContextFactory
    : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder =
            new DbContextOptionsBuilder<AppDbContext>();

        optionsBuilder.UseSqlServer(
            @"Data Source=VVSPL1\SQLEXPRESS;
              Initial Catalog=authncrud;
              Integrated Security=True;
              TrustServerCertificate=True");

        return new AppDbContext(
            optionsBuilder.Options);
    }
}