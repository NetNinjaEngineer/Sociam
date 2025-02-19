using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Sociam.Infrastructure.Persistence;

public sealed class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    private readonly IConfiguration? _configuration;

    public ApplicationDbContextFactory() { }

    public ApplicationDbContextFactory(IConfiguration configuration) => _configuration = configuration;

    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseNpgsql(_configuration?.GetConnectionString("PostgresConnection"));
        return new ApplicationDbContext(optionsBuilder.Options);
    }
}