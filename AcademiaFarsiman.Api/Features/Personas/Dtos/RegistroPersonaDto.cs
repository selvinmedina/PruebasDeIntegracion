using System;
namespace AcademiaFarsiman.Api.Features.Personas.Dtos
{
    public class RegistroPersonaDto
    {
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public bool Activo { get; set; }
        public int UsuarioAgregaId { get; set; }
    }

}

