using CP2.Infrastructure.Persistence.Entitites;

namespace CP2.Infrastructure.Persistence.Repositories;

public interface ITurmaRepository
{
    Task<Turma> AddAsync(Turma turma);
    Task<Turma> GetByIdAsync(Guid id);
    Task DeleteAsync(Turma turma);
    Task SaveChangesAsync();
    Task<List<Turma>> GetAllAsync();
    Task<string> FindIdentificadorAsync(string identificador, Guid? idToIgnore = null);
}