using CP2.Infrastructure.Persistence.Entitites;

namespace CP2.UseCase;

public interface ITurmaUseCase
{
    Task<Turma> CreateAsync(Turma turma);
    Task<Turma> UpdateAsync(Turma turma);
    Task<Turma> GetById(Guid id);
    Task<List<Turma>> GetAllAsync();
    Task DeleteAsync(Guid id);
}