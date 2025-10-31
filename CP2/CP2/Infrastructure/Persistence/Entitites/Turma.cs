namespace CP2.Infrastructure.Persistence.Entitites;

public class Turma
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Identificador { get; set; }
    public int Semestre { get; set; }
    public string Turno { get; set; }
    
    // 1..N
    public List<Aluno> Alunos { get; set; }
    
    // 1..N
    public List<Professor> Professors { get; set; }
}