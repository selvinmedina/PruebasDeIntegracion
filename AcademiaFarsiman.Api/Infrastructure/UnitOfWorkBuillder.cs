using AcademiaFarsiman.Api.Infrastructure.AcademiaFarsiman;
using Farsiman.Domain.Core.Standard.Repositories;
using Farsiman.Infraestructure.Core.Entity.Standard;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFarsiman.Api.Infrastructure
{
    public class UnitOfWorkBuillder
	{
        readonly IServiceProvider serviceProvider;

        public UnitOfWorkBuillder(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IUnitOfWork BuildDbAcademiaFarsiman()
        {
            DbContext dbContext = serviceProvider.GetService<AcademiaFarsimanContext>() ?? throw new NullReferenceException();
            return new UnitOfWork(dbContext);
        }
    }
}

