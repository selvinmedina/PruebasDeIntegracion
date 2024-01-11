using System;
using AcademiaFarsiman.Api.Infrastructure.AcademiaFarsiman.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademiaFarsiman.Api.Infrastructure.AcademiaFarsiman.Maps
{
    public class PersonaMap : IEntityTypeConfiguration<Persona>
    {
        public void Configure(EntityTypeBuilder<Persona> builder)
        {
            builder.HasKey(e => e.Id)
                .HasName("PK_Persona");

            builder.Property(e => e.Id)
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Apellido)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.FechaNacimiento)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(e => e.Activo)
                .HasColumnType("bit")
                .HasDefaultValue(1);

            // Campos de Auditoría
            builder.Property(e => e.UsuarioAgregaId)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(e => e.FechaAgrega)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(e => e.UsuarioModificaId)
                .HasColumnType("int");

            builder.Property(e => e.FechaModifica)
                .HasColumnType("datetime");
        }
    }
}

