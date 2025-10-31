using CP2.Infrastructure.Persistence.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CP2.Infrastructure.Persistence.Mappings;

public class ProfessorMapping : IEntityTypeConfiguration<Professor>
{
    public void Configure(EntityTypeBuilder<Professor> b)
    {
        b.ToTable("C2NET_PROFESSOR");
        
        // PK
        b.HasKey(x => x.Id);
        
        b.Property(x => x.Id)
            .ValueGeneratedNever();
        
        b.Property(x => x.Nome)
            .IsRequired()
            .HasMaxLength(120);
        
        b.Property(x => x.Pf)
            .IsRequired()
            .HasMaxLength(20);
        
        b.HasIndex(x => x.Email)
            .IsUnique();
        
        b.Property(x=> x.Email)
            .IsRequired()
            .HasMaxLength(120);
        
    }
}