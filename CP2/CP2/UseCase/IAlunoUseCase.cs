using CP2.Infrastructure.Persistence.Entitites;

namespace CP2.UseCase;

public interface IAlunoUseCase
{
    Task<Aluno> CreateAsync(Aluno aluno);
    Task<Aluno> UpdateAsync(Aluno aluno);
    Task<Aluno> GetById(Guid id);
    Task<List<Aluno>> GetAllAsync();
    Task DeleteAsync(Guid id);
}