using CP2.Infrastructure.Persistence.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CP2.Infrastructure.Persistence.Mappings;

public class TurmaMapping : IEntityTypeConfiguration<Turma>
{
    public void Configure(EntityTypeBuilder<Turma> b)
    {
        b.ToTable("C2NET_TURMA");
        
        // PK
        b.HasKey(x => x.Id);
        
        b.Property(x => x.Id)
            .ValueGeneratedNever();
        
        b.Property(x => x.Identificador)
            .IsRequired()
            .HasMaxLength(20);
        
        b.HasIndex(x => x.Identificador)
            .IsUnique();
        
        b.Property(x => x.Semestre)
            .IsRequired();
        
        b.Property(x => x.Turno)
            .IsRequired()
            .HasMaxLength(20);
        
        // 1..N
        b.HasMany(x => x.Alunos)
            .WithOne(x => x.Turma)
            .HasForeignKey(x => x.TurmaId);
        
        // 1..N
        b.HasMany(x => x.Professors)
            .WithOne(x => x.Turma)
            .HasForeignKey(x => x.TurmaId);

    }
}