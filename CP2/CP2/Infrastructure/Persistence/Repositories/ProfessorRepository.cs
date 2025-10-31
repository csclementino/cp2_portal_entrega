using CP2.Infrastructure.Persistence.Entitites;
using Microsoft.EntityFrameworkCore;

namespace CP2.Infrastructure.Persistence.Repositories;

public class ProfessorRepository(Cp2Context context) : IProfessorRepository
{
    public async Task<Professor> AddAsync(Professor professor)
    {
        context.Professors.Add(professor);
        await context.SaveChangesAsync();
        return professor;
    }

    public async Task<Professor> GetByIdAsync(Guid id)
    {
        return await context.Professors.FindAsync(id);
    }

    public async Task DeleteAsync(Professor professor)
    {
        context.Professors.Remove(professor);
        await context.SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }

    public async Task<List<Professor>> GetAllAsync()
    {
        return await context.Professors.ToListAsync();
    }
    
    public async Task<string> FindEmailAsync(string email, Guid? idToIgnore = null)
    {
        var professor = await context.Professors
            .FirstOrDefaultAsync(c => c.Email == email && (idToIgnore == null || c.Id != idToIgnore.Value));
        return professor?.Email;
    }
}