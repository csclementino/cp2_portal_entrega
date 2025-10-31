using CP2.Infrastructure.Persistence.Entitites;
using CP2.Infrastructure.Persistence.Mappings;
using Microsoft.EntityFrameworkCore;

namespace CP2;

public class Cp2Context : DbContext
{
    public Cp2Context(DbContextOptions options) : base(options)
    {
        
    }
    
    public DbSet<Turma> Turmas { get; set; }
    public DbSet<Professor> Professors { get; set; }
    public DbSet<Aluno> Alunos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AlunoMapping());
        modelBuilder.ApplyConfiguration(new ProfessorMapping());
        modelBuilder.ApplyConfiguration(new TurmaMapping());
        base.OnModelCreating(modelBuilder);
    }
}