using System;
using AcademiaFarsiman.Api.Infrastructure.AcademiaFarsiman;
using AcademiaFarsiman.Api.Infrastructure.AcademiaFarsiman.Entities;
using AcademiaFarsiman.IntegrationTests.Mocks.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AcademiaFarsiman.IntegrationTests.Mocks
{
    public static class DbContextMocker
    {
        public static AcademiaFarsimanContext GetAppDbContext()
        {
            var options = new DbContextOptionsBuilder<AcademiaFarsimanContext>()
                .UseInMemoryDatabase(databaseName: $"TestDb{System.DateTime.Now.Millisecond}")
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .EnableSensitiveDataLogging()
                .Options;

            var dbContext = new AcademiaFarsimanContext(options);

            dbContext.SaveChanges();

            return dbContext;
        }

        public static void AgregarPersonas(AcademiaFarsimanContext dbContext)
        {
            var personas = dbContext.Set<Persona>();

            if (!personas.Any())
            {
                personas.Add(new Persona()
                {
                    Id = 1,
                    Nombre = "Juan",
                    Apellido = "Perez",
                    FechaNacimiento = new DateTime(1987, 5, 20),
                    Activo = true,
                    UsuarioAgregaId = 1,
                    FechaAgrega = DateTime.Now
                });

                personas.Add(new Persona()
                {
                    Id = 2,
                    Nombre = "Maria",
                    Apellido = "Rodriguez",
                    FechaNacimiento = new DateTime(1992, 10, 15),
                    Activo = true,
                    UsuarioAgregaId = 1,
                    FechaAgrega = DateTime.Now
                });

                dbContext.SaveChanges();
            }
        }
    }
}

