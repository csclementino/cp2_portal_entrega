using CP2.Infrastructure.Persistence.Entitites;

namespace CP2.DTOs;

public class ProfessorResponse
{
    public string Nome { get; set; }
    public string Pf { get; set; }
    
    public static ProfessorResponse ToResponse(Professor professor)
    {
        return new ProfessorResponse
        {
            Nome = professor.Nome,
            Pf = professor.Pf
        };
    }
}
