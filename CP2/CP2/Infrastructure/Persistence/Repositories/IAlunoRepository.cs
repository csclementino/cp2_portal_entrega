using CP2.Infrastructure.Persistence.Entitites;

namespace CP2.Infrastructure.Persistence.Repositories;

public interface IAlunoRepository
{
    Task<Aluno> AddAsync(Aluno aluno);
    Task<Aluno> GetByIdAsync(Guid id);
    Task DeleteAsync(Aluno aluno);
    Task SaveChangesAsync();
    Task<List<Aluno>> GetAllAsync();
    Task<string> FindEmailAsync(string email, Guid? idToIgnore = null);
}