using CP2.Exceptions;
using CP2.Infrastructure.Persistence.Entitites;
using CP2.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CP2.UseCase;

public class ProfessorUseCase :  IProfessorUseCase
{
    private readonly IProfessorRepository _professorRepository;
    private readonly ITurmaRepository _turmaRepository;
    
    public ProfessorUseCase(IProfessorRepository professorRepository, ITurmaRepository turmaRepository)
    {
        _professorRepository = professorRepository;
        _turmaRepository = turmaRepository;
    }
    
    public async Task<Professor> CreateAsync(Professor professor)
    {
        var existe = await _professorRepository.FindEmailAsync(professor.Email);
        if (existe != null)
            throw new EmailDuplicadoException();

        var turma = await _turmaRepository.GetByIdAsync(professor.TurmaId);
        if (turma == null)
            throw new TurmaNaoEncontradaException();

        try
        {
            await _professorRepository.AddAsync(professor);
        }
        catch (DbUpdateException ex) 
            when (ex.InnerException?.Message.Contains("UNIQUE") == true)
        {
            throw new EmailDuplicadoException();
        }
        return professor;
    }

    public async Task<Professor> UpdateAsync(Professor professor)
    {
        var professorExistente = await GetById(professor.Id);
        var result = await _professorRepository.FindEmailAsync(professor.Email,professor.Id);
        if (result != null)
            throw new EmailDuplicadoException();
        
        professorExistente.Nome = professor.Nome;
        professorExistente.Email = professor.Email;
        
        try
        {
            await _professorRepository.SaveChangesAsync();
        }
        catch (DbUpdateException ex) 
            when (ex.InnerException?.Message.Contains("UNIQUE") == true)
        {
            throw new EmailDuplicadoException();
        }
        return professorExistente;
    }

    public async Task<Professor> GetById(Guid id)
    {
        var professor = await _professorRepository.GetByIdAsync(id);
        if (professor == null)
            throw new CadastroNaoEncontradoException();
        return professor;
    }

    public async Task<List<Professor>> GetAllAsync()
    {
        return await _professorRepository.GetAllAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var professor = await GetById(id);
        await _professorRepository.DeleteAsync(professor);
    }
}