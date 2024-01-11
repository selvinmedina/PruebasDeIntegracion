using AcademiaFarsiman.Api;
using AcademiaFarsiman.Api.Features.Personas.Dtos;
using AcademiaFarsiman.IntegrationTests.Helpers;
using Farsiman.Application.Core.Standard.DTOs;
using FluentAssertions;
using RestSharp;
using Xunit;

namespace AcademiaFarsiman.IntegrationTests.Tests
{
    public class PersonasTests : IClassFixture<TestFixture<Startup>>
    {
        private readonly RestClient Client;
        const string ENDPOINT_OBTENER = "/api/Personas/obtener";
        const string ENDPOINT_CREAR = "/api/Personas/crear";

        public PersonasTests(TestFixture<Startup> fixture)
        {
            Client = fixture.Client;
        }

        #region Obtener
        [Fact]
        public async Task Obtener_Personas_Escenario_Ideal()
        {
            // Arrange
            var request = new RestRequest(ENDPOINT_OBTENER, Method.Get);

            // Aqui se tiene que crear en la base de datos al menos una persona
            // Propongan 3 soluciones de donde seria mejor crearla, en el constructor, en un metodo aparte, etc.


            // Act
            var response = await Client.GetAsync<Respuesta<List<PersonaDto>>>(request);

            // Assert
            response.Should().NotBeNull();
            response.Ok.Should().BeTrue();

            // Verifica que se devuelva al menos una persona.
            // Puede que quieras ajustar esta parte según tu lógica de negocio y tus datos de prueba.
            Assert.True(response.Data.Count > 0);
        }
        #endregion


        #region Creación
        [Fact]
        public async Task Crear_Persona_Escenario_Ideal()
        {
            // Arrange
            var request = new RestRequest(ENDPOINT_CREAR, Method.Post);

            request.AddJsonBody(new RegistroPersonaDto
            {
                Nombre = "John Doe",
                Apellido = "Smith",
                FechaNacimiento = DateTime.Now.AddYears(-30),
                Activo = true,
                UsuarioAgregaId = 1
            });

            // Act
            var response = await Client.PostAsync<Respuesta<PersonaDto>>(request);

            // Assert
            response.Should().NotBeNull();
            response.Ok.Should().BeTrue();

            Assert.True(response.Data.Id > 0);
        }

        // Agrega otros escenarios de creación, ejemplo, cuando el nombgre sea vacío...
        #endregion

        // Agrega escenarios de edición

        // Agrega escenarios para inactivar o activar
    }
}

