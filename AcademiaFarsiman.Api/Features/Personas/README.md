# Feature - Personas

Esta característica se encarga de gestionar las personas en la base de datos de la Academia Farsiman.

## Detalles de Implementación

La característica Personas contiene los siguientes componentes principales:

### Dtos

Los Dtos (Data Transfer Objects) son utilizados para transferir datos entre procesos. Aquí, se definen tres Dtos:

- `PersonaDto`: Utilizado para mostrar la información de la persona.
- `RegistroPersonaDto`: Utilizado cuando se necesita registrar una nueva persona.
- `EdicionPersonaDto`: Utilizado cuando se está actualizando la información de una persona existente.

### Services

Los servicios contienen la lógica principal de la característica. Aquí se encuentra:

- `PersonaService`: Este servicio contiene los métodos necesarios para realizar operaciones CRUD en personas.
- `PersonaDomain`: Esta clase contiene la lógica de negocio relacionada con las personas.

### Domain Requirements

Los requerimientos de dominio son reglas o condiciones que deben cumplirse en la lógica de negocio. Aquí se encuentra:

- `PersonaDomainRequirement`: Esta clase contiene las reglas de negocio específicas para las personas.