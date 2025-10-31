using CP2.Exceptions;
using CP2.Infrastructure.Persistence.Entitites;
using CP2.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CP2.UseCase;

public class TurmaUseCase : ITurmaUseCase
{
    private readonly ITurmaRepository _turmaRepository;
    
    public TurmaUseCase(ITurmaRepository turmaRepository)
    {
        _turmaRepository = turmaRepository;
    }
    
    public async Task<Turma> CreateAsync(Turma turma)
    {
        var existe = await _turmaRepository.FindIdentificadorAsync(turma.Identificador);
        if (existe != null)
            throw new TurmaDuplicadaException();
        try
        {
            await _turmaRepository.AddAsync(turma);
        }
        catch (DbUpdateException ex) 
            when (ex.InnerException?.Message.Contains("UNIQUE") == true)
        {
            throw new TurmaDuplicadaException();
        }
        return turma;
    }

    public async Task<Turma> UpdateAsync(Turma turma)
    {
        var turmaExistente = await GetById(turma.Id);
        var result = await _turmaRepository.FindIdentificadorAsync(turma.Identificador,turma.Id);
        if (result != null)
            throw new TurmaDuplicadaException();
        
        turmaExistente.Semestre = turma.Semestre;
        turmaExistente.Turno = turma.Turno;
        
        try
        {
            await _turmaRepository.SaveChangesAsync();
        }
        catch (DbUpdateException ex) 
            when (ex.InnerException?.Message.Contains("UNIQUE") == true)
        {
            throw new TurmaDuplicadaException();
        }
        return turmaExistente;
    }

    public async Task<Turma> GetById(Guid id)
    {
        var turma = await _turmaRepository.GetByIdAsync(id);
        if (turma == null)
            throw new CadastroNaoEncontradoException();
        return turma;
    }

    public async Task<List<Turma>> GetAllAsync()
    {
        return await _turmaRepository.GetAllAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var turma = await GetById(id);
        await _turmaRepository.DeleteAsync(turma);
    }
}