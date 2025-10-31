using CP2.Infrastructure.Persistence.Entitites;
using Microsoft.EntityFrameworkCore;

namespace CP2.Infrastructure.Persistence.Repositories;

public class AlunoRepository(Cp2Context context) : IAlunoRepository
{
    public async Task<Aluno> AddAsync(Aluno aluno)
    {
        context.Alunos.Add(aluno);
        await context.SaveChangesAsync();
        return aluno;
    }

    public async Task<Aluno> GetByIdAsync(Guid id)
    {
        return await context.Alunos.FindAsync(id);
    }

    public async Task DeleteAsync(Aluno aluno)
    {
        context.Alunos.Remove(aluno);
        await context.SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }

    public async Task<List<Aluno>> GetAllAsync()
    {
        return await context.Alunos.ToListAsync();
    }
    
    public async Task<string> FindEmailAsync(string email, Guid? idToIgnore = null)
    {
        var aluno = await context.Alunos
            .FirstOrDefaultAsync(c => c.Email == email && (idToIgnore == null || c.Id != idToIgnore.Value));
        return aluno?.Email;
    }
}