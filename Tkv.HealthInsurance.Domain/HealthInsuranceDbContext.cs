using Microsoft.EntityFrameworkCore;
using Tkv.HealthInsurance.Domain.Domains.Definition;
using Tkv.HealthInsurance.Domain.Domains.RequestLog;

namespace Tkv.HealthInsurance.Domain
{
    public class HealthInsuranceDbContext : DbContext
    {


        #region [Properties]



        #region [Definition]


        public virtual DbSet<Coverage> Coverage { get; set; }


        #endregion



        #region [RequestLog]


        public virtual DbSet<RequestLogDetail> RequestLogDetail { get; set; }
        public virtual DbSet<RequestLogMaster> RequestLogMaster { get; set; }


        #endregion



        #endregion




        #region [ctor]


        public HealthInsuranceDbContext(DbContextOptions<HealthInsuranceDbContext> options) : base(options)
        {
        }


        #endregion



        #region [Methods]

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);



            #region [Definition]



            #region [Coverage]


            builder.Entity<Coverage>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Title).IsRequired().HasMaxLength(100);
            });


            //Seed is better to be in another file but 1 hour is not much time.

            #region [Coverage seed]


            var now = DateTime.Now;
            builder.Entity<Coverage>().HasData(new Coverage
            {
                Id = 1, 
                InternationalCode = 100,
                Title = "Surgery",
                MinimumDeposit = 5000,
                MaximumDeposit = 500000000,
                PriceMultiplier = 0.0052,
                InsertedDateTime = now,
                LastModifiedDateTime = now
            });

            builder.Entity<Coverage>().HasData(new Coverage
            {
                Id = 2, 
                InternationalCode = 200,
                Title = "Dentistry",
                MinimumDeposit = 4000,
                MaximumDeposit = 400000000,
                PriceMultiplier = 0.0042,
                InsertedDateTime = now,
                LastModifiedDateTime = now
            });

            builder.Entity<Coverage>().HasData(new Coverage
            {
                Id = 3, 
                InternationalCode = 300,
                Title = "Hospitalization",
                MinimumDeposit = 2000,
                MaximumDeposit = 200000000,
                PriceMultiplier = 0.005,
                InsertedDateTime = now,
                LastModifiedDateTime = now
            });


            #endregion


            #endregion



            #endregion



            #region [RequestLog]



            #region [RequestLogMaster]


            builder.Entity<RequestLogMaster>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Title).IsRequired().HasMaxLength(100);

            });


            #endregion



            #region [RequestLogDetail]


            builder.Entity<RequestLogDetail>(entity =>
            {
                entity.HasKey(x => x.Id);


                entity.HasOne(x => x.RequestLogMaster)
                    .WithMany(x => x.RequestLogDetails)
                    .HasForeignKey(x => x.RequestLogMasterId);


                entity.HasOne(x => x.Coverage)
                    .WithMany(x => x.RequestLogDetails)
                    .HasForeignKey(x => x.CoverageId);
            });


            #endregion



            #endregion



        }

        #endregion

    }
}
