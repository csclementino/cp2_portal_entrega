namespace CP2.Infrastructure.Persistence.Entitites;

public class Aluno
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Rm { get; set; }
    public DateTime DataIngresso { get; set; }
    public string Status { get; set; }
    
    // N..1
    public Guid TurmaId { get; set; }
    public Turma Turma { get; set; }
}