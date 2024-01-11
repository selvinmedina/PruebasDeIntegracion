using System;
namespace AcademiaFarsiman.Api.Features.Personas.Dtos
{
    public class PersonaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaAgrega { get; set; }
        public int UsuarioAgregaId { get; set; }
        public DateTime FechaModifica { get; set; }
        public int UsuarioModificaId { get; set; }
    }

}

