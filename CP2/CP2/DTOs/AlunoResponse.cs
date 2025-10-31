using CP2.Infrastructure.Persistence.Entitites;

namespace CP2.DTOs;

public class AlunoResponse
{
    public string Nome { get; set; }
    public string Rm { get; set; }
    
    
    public static AlunoResponse ToResponse(Aluno aluno)
    {
        return new AlunoResponse
        {
            Nome = aluno.Nome,
            Rm = aluno.Rm
        };
    }
}
