using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Tkv.HealthInsurance.Shared.Statics;

namespace Tkv.HealthInsurance.Domain
{ 
    public class HealthInsuranceDbContextFactory : IDesignTimeDbContextFactory<HealthInsuranceDbContext>
    {
        HealthInsuranceDbContext IDesignTimeDbContextFactory<HealthInsuranceDbContext>.CreateDbContext(string[] args)
        { 
            var builder = new DbContextOptionsBuilder<HealthInsuranceDbContext>()
                .UseSqlServer(Statics.DbConnectionString);

            return new HealthInsuranceDbContext(builder.Options);
        }
    }
}
