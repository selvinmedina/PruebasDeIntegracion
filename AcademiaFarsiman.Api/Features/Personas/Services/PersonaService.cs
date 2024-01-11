using System;
using AcademiaFarsiman.Api.Common.Mensajes;
using AcademiaFarsiman.Api.Features.Personas.Dtos;
using AcademiaFarsiman.Api.Infrastructure.AcademiaFarsiman;
using AcademiaFarsiman.Api.Infrastructure.AcademiaFarsiman.Entities;
using AutoMapper;
using Farsiman.Application.Core.Standard.DTOs;
using Farsiman.Domain.Core.Standard.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFarsiman.Api.Features.Personas.Services
{
    public class PersonasService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PersonaDomain _dominio;
        private readonly IMapper _mapper;

        public PersonasService(AcademiaFarsimanUnitOfWork unitOfWork,
                               PersonaDomain personasDomain,
                               IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._dominio = personasDomain;
            _mapper = mapper;
        }

        public async Task<Respuesta<List<PersonaDto>>> Obtener()
        {
            try
            {
                var personas = await _unitOfWork.Repository<Persona>()
                        .AsQueryable()
                        .AsNoTracking()
                        .ToListAsync();

                if (personas is null || !personas.Any())
                {
                    return Respuesta<List<PersonaDto>>.Fault(MensajesPersonas.MSP001);
                }

                var dtos = _mapper.Map<List<Persona>, List<PersonaDto>>(personas);

                return Respuesta<List<PersonaDto>>.Success(dtos);
            }
            catch (Exception ex)
            {
                return Respuesta<List<PersonaDto>>.Fault(ex.Message);
            }
        }

        private async Task<Respuesta<PersonaDto>> Guardar(Persona entidad)
        {
            _unitOfWork.Repository<Persona>().Add(entidad);

            if (!(await _unitOfWork.SaveChangesAsync()))
            {
                return Respuesta<PersonaDto>.Fault(MensajesPersonas.MSP005);
            }

            var dto = _mapper.Map<Persona, PersonaDto>(entidad);

            return Respuesta<PersonaDto>.Success(dto);
        }

        public async Task<Respuesta<PersonaDto>> CrearPersona(RegistroPersonaDto registroDto)
        {
            try
            {
                var resultadoDominio = _dominio.CrearPersona(registroDto);

                if (!resultadoDominio.Ok)
                {
                    return Respuesta.Fault<PersonaDto>(resultadoDominio.Mensaje);
                }

                var respuesta = await Guardar(resultadoDominio.Data);

                return respuesta;
            }
            catch (Exception ex)
            {
                return Respuesta<PersonaDto>.Fault(ex.Message);
            }
        }

        private Respuesta<PersonaDto?> Editar(Persona? entidad)
        {
            if (!_unitOfWork.SaveChanges())
            {
                return Respuesta<PersonaDto?>.Fault(MensajesPersonas.MSP011);
            }

            PersonaDto? dto = _mapper.Map<Persona?, PersonaDto?>(entidad);

            return Respuesta<PersonaDto?>.Success(dto);
        }

        public async Task<Respuesta<PersonaDto?>> EditarPersona(EdicionPersonaDto edicionDto)
        {
            try
            {
                var entidad = await _unitOfWork.Repository<Persona>()
                                               .AsQueryable()
                                               .FirstAsync(x => x.Id == edicionDto.Id);

                var resultadoDominio = _dominio.EditarPersona(edicionDto, entidad);

                if (!resultadoDominio.Ok)
                {
                    return Respuesta.Fault<PersonaDto?>(resultadoDominio.Mensaje);
                }

                var respuesta = Editar(entidad);

                return respuesta;
            }
            catch (Exception ex)
            {
                return Respuesta<PersonaDto?>.Fault(ex.Message);
            }
        }

        public async Task<Respuesta<PersonaDto?>> CambiarEstado(int id, int usuarioModificaId, bool estado)
        {
            try
            {
                var entidad = await _unitOfWork.Repository<Persona>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(x => x.Id == id);

                var resultadoDominio = _dominio.CambiarEstado(id, usuarioModificaId, entidad, estado);

                if (!resultadoDominio.Ok)
                {
                    return Respuesta.Fault<PersonaDto?>(resultadoDominio.Mensaje);
                }

                var respuesta = Editar(entidad);

                return respuesta;
            }
            catch (Exception ex)
            {
                return Respuesta<PersonaDto?>.Fault(ex.Message);
            }
        }
    }

}

