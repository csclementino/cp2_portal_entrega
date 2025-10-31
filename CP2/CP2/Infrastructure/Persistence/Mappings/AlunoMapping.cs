using CP2.Infrastructure.Persistence.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CP2.Infrastructure.Persistence.Mappings;

public class AlunoMapping : IEntityTypeConfiguration<Aluno>
{
    public void Configure(EntityTypeBuilder<Aluno> b)
    {
        b.ToTable("C2NET_ALUNO");
        
        // PK
        b.HasKey(x => x.Id);
        
        b.Property(x => x.Id)
            .ValueGeneratedNever();
        
        b.Property(x => x.Nome)
            .IsRequired()
            .HasMaxLength(120);
        
        b.Property(x => x.Rm)
            .IsRequired()
            .HasMaxLength(20);
        
        b.HasIndex(x => x.Email)
            .IsUnique();
        
        b.Property(x=> x.Email)
            .IsRequired()
            .HasMaxLength(120);

        b.Property(x => x.DataIngresso)
            .IsRequired();
        
        b.Property(x => x.Status)
            .HasMaxLength(20);
        
    }
}