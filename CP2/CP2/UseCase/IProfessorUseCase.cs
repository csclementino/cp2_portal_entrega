using CP2.Infrastructure.Persistence.Entitites;

namespace CP2.UseCase;

public interface IProfessorUseCase
{
    Task<Professor> CreateAsync(Professor professor);
    Task<Professor> UpdateAsync(Professor professor);
    Task<Professor> GetById(Guid id);
    Task<List<Professor>> GetAllAsync();
    Task DeleteAsync(Guid id);
}