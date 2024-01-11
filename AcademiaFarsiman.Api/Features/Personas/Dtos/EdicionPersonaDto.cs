using System;
namespace AcademiaFarsiman.Api.Features.Personas.Dtos
{
    public class EdicionPersonaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public bool Activo { get; set; }
        public int UsuarioModificaId { get; set; }
    }
}

