using AcademiaFarsiman.Api.Features.Personas.Dtos;
using AcademiaFarsiman.Api.Infrastructure.AcademiaFarsiman.Entities;
using AutoMapper;

namespace AcademiaFarsiman.Api.Common
{
    public class MapeosDto : Profile
    {
        public MapeosDto()
        {
            CreateMap<PersonaDto, Persona>()
                .ReverseMap();
        }
    }
}

