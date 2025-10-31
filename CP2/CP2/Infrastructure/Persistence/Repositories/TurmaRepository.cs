using CP2.Infrastructure.Persistence.Entitites;
using Microsoft.EntityFrameworkCore;

namespace CP2.Infrastructure.Persistence.Repositories;

public class TurmaRepository(Cp2Context context) : ITurmaRepository
{
    public async Task<Turma> AddAsync(Turma turma)
    {
        context.Turmas.Add(turma);
        await context.SaveChangesAsync();
        return turma;
    }

    public async Task<Turma> GetByIdAsync(Guid id)
    {
        return await context.Turmas.FindAsync(id);
    }

    public async Task DeleteAsync(Turma turma)
    {
        context.Turmas.Remove(turma);
        await context.SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }

    public async Task<List<Turma>> GetAllAsync()
    {
        return await context.Turmas.ToListAsync();
    }
    
    public async Task<string> FindIdentificadorAsync(string identificador, Guid? idToIgnore = null)
    {
        var turma = await context.Turmas
            .FirstOrDefaultAsync(c => c.Identificador == identificador && (idToIgnore == null || c.Id != idToIgnore.Value));
        return turma?.Identificador;
    }
}