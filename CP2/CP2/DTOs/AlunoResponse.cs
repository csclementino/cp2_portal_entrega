using CP2.Infrastructure.Persistence.Entitites;

namespace CP2.DTOs;

public class AlunoResponse
{
    public string Nome { get; set; }
    public string Rm { get; set; }
    
    public Guid Id { get; set; }
    
    public static AlunoResponse ToResponse(Aluno aluno)
    {
        return new AlunoResponse
        {
            Id = aluno.Id,
            Nome = aluno.Nome,
            Rm = aluno.Rm
        };
    }
}