using System;
using AcademiaFarsiman.Api.Common.Mensajes;
using AcademiaFarsiman.Api.Features.Personas.Dtos;
using AcademiaFarsiman.Api.Infrastructure.AcademiaFarsiman.Entities;
using Farsiman.Application.Core.Standard.DTOs;

namespace AcademiaFarsiman.Api.Features.Personas.Services
{
    public class PersonaDomain
    {
        public Respuesta<bool> ValidarCreacionPersona(RegistroPersonaDto registroDto)
        {
            if (registroDto is null)
                return Respuesta<bool>.Fault(MensajesPersonas.MSP003);

            if (string.IsNullOrEmpty(registroDto.Nombre?.Trim()))
            {
                return Respuesta<bool>.Fault(MensajesPersonas.MSP008);
            }

            if (string.IsNullOrEmpty(registroDto.Apellido?.Trim()))
            {
                return Respuesta<bool>.Fault(MensajesPersonas.MSP010);
            }

            if (registroDto.UsuarioAgregaId == default)
            {
                return Respuesta<bool>.Fault(MensajesPersonas.MSP013);
            }

            return Respuesta<bool>.Success(true);
        }

        public Respuesta<Persona> CrearPersona(RegistroPersonaDto registroDto)
        {
            var respuestaValidacion = ValidarCreacionPersona(registroDto);

            if (!respuestaValidacion.Ok)
            {
                return Respuesta<Persona>.Fault(respuestaValidacion.Mensaje);
            }

            Persona entidad = new()
            {
                Nombre = registroDto.Nombre.Trim(),
                Apellido = registroDto.Apellido.Trim(),
                UsuarioAgregaId = registroDto.UsuarioAgregaId,
                FechaNacimiento = registroDto.FechaNacimiento,
                FechaAgrega = DateTime.Now,
                Activo = true // asumimos que toda persona nueva estará activa por defecto
            };

            return Respuesta<Persona>.Success(entidad);
        }

        public Respuesta<bool> ValidarEdicionPersona(EdicionPersonaDto dto, Persona entidad)
        {
            if (dto is null || entidad is null)
                return Respuesta<bool>.Fault(MensajesPersonas.MSP006);

            if (dto.Id == default)
            {
                return Respuesta<bool>.Fault(MensajesPersonas.MSP012);
            }

            if (string.IsNullOrEmpty(dto.Nombre?.Trim()))
            {
                return Respuesta<bool>.Fault(MensajesPersonas.MSP008);
            }

            if (string.IsNullOrEmpty(dto.Apellido?.Trim()))
            {
                return Respuesta<bool>.Fault(MensajesPersonas.MSP010);
            }

            if (dto.UsuarioModificaId == default)
            {
                return Respuesta<bool>.Fault(MensajesPersonas.MSP014);
            }

            return Respuesta<bool>.Success(true);
        }

        public Respuesta<Persona> EditarPersona(EdicionPersonaDto dto, Persona entidad)
        {
            var respuestaValidacion = ValidarEdicionPersona(dto, entidad);

            if (!respuestaValidacion.Ok)
            {
                return Respuesta<Persona>.Fault(respuestaValidacion.Mensaje);
            }

            if (entidad is { })
            {
                entidad.Nombre = dto.Nombre.Trim();
                entidad.Apellido = dto.Apellido.Trim();
                entidad.UsuarioModificaId = dto.UsuarioModificaId;
                entidad.FechaModifica = DateTime.Now;
            }

            return Respuesta<Persona>.Success(entidad!);
        }

        public Respuesta<bool> CambiarEstado(int id, int usuarioModificaId, Persona? entidad, bool estado)
        {
            if (entidad is null)
                return Respuesta<bool>.Fault(MensajesPersonas.MSP006);

            if (id == default)
            {
                return Respuesta<bool>.Fault(MensajesPersonas.MSP015);
            }

            if (usuarioModificaId == default)
            {
                return Respuesta<bool>.Fault(MensajesPersonas.MSP016);
            }

            entidad.Activo = estado;
            entidad.UsuarioModificaId = usuarioModificaId;
            entidad.FechaModifica = DateTime.Now;

            return Respuesta<bool>.Success(true);
        }
    }

}

