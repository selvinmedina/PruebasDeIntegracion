using System;
using AcademiaFarsiman.Api.Common.Attributes;
using AcademiaFarsiman.Api.Common.Mensajes;
using AcademiaFarsiman.Api.Features.Personas.Dtos;
using AcademiaFarsiman.Api.Features.Personas.Services;
using Microsoft.AspNetCore.Mvc;

namespace AcademiaFarsiman.Api.Controllers
{
    [Route("api/[controller]")]
    public class PersonasController : ControllerBase
    {
        private readonly PersonasService _personasService;

        public PersonasController(PersonasService personasService)
        {
            this._personasService = personasService;
        }

        [HttpGet("obtener")]
        public async Task<IActionResult> Obtener()
        {
            var result = await _personasService.Obtener();

            if (result.Ok)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Mensaje);
            }
        }

        [HttpPost, Route("crear")]
        public async Task<IActionResult> CrearPersona([FromBody] RegistroPersonaDto registroDto)
        {
            var respuesta = await _personasService.CrearPersona(registroDto);
            return Ok(respuesta);
        }

        [HttpPost, Route("editar")]
        public async Task<IActionResult> EditarPersona([FromBody] EdicionPersonaDto edicionDto)
        {
            var respuesta = await _personasService.EditarPersona(edicionDto);
            return Ok(respuesta);
        }

        [HttpPost, Route("cambiar-estado/{id}")]
        public async Task<IActionResult> CambiarEstado(int id,
            [FromQuery][CampoRequerido(ErrorMessage = MensajesPersonas.MSP016)] int usuarioModificaId,
            [FromQuery][CampoRequerido(ErrorMessage = MensajesPersonas.MSP017)] bool estado)
        {
            var respuesta = await _personasService.CambiarEstado(id, usuarioModificaId, estado);
            return Ok(respuesta);
        }
    }

}

