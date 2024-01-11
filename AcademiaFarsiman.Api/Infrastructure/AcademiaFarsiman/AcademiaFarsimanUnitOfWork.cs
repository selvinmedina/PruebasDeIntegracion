using Farsiman.Infraestructure.Core.Entity.Standard;
using Newtonsoft.Json;

namespace AcademiaFarsiman.Api.Infrastructure.AcademiaFarsiman
{
    public class AcademiaFarsimanUnitOfWork : UnitOfWork
    {
		public AcademiaFarsimanUnitOfWork(AcademiaFarsimanContext dbContext) : base(dbContext)
        {
		}

        protected override void SaveChangesException(Exception ex)
        {
            var excepcion = new
            {
                observacion = "Error",
                info = JsonConvert.SerializeObject(ex)
            };
            Console.WriteLine(excepcion);
        }
    }
}

