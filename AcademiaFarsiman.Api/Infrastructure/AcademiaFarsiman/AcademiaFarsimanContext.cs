using AcademiaFarsiman.Api.Infrastructure.AcademiaFarsiman.Maps;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFarsiman.Api.Infrastructure.AcademiaFarsiman
{
    public partial class AcademiaFarsimanContext : DbContext
    {
        public AcademiaFarsimanContext()
        {
        }

        public AcademiaFarsimanContext(DbContextOptions<AcademiaFarsimanContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonaMap());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


    }
}

