using CP2.Infrastructure.Persistence.Entitites;

namespace CP2.Infrastructure.Persistence.Repositories;

public interface IProfessorRepository
{
    Task<Professor> AddAsync(Professor professor);
    Task<Professor> GetByIdAsync(Guid id);
    Task DeleteAsync(Professor professor);
    Task SaveChangesAsync();
    Task<List<Professor>> GetAllAsync();
    Task<string> FindEmailAsync(string email, Guid? idToIgnore = null);
}