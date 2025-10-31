namespace CP2.Infrastructure.Persistence.Entitites;

public class Professor
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Pf { get; set; }
    
    // N..1
    public Guid TurmaId { get; set; }
    public Turma Turma { get; set; }
}