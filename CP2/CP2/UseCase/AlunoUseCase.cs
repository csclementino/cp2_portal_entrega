using CP2.Exceptions;
using CP2.Infrastructure.Persistence.Entitites;
using CP2.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CP2.UseCase;

public class AlunoUseCase : IAlunoUseCase
{
    private readonly IAlunoRepository _alunoRepository;
    private readonly ITurmaRepository _turmaRepository;
    
    public AlunoUseCase(IAlunoRepository alunoRepository, ITurmaRepository turmaRepository)
    {
        _alunoRepository = alunoRepository;
        _turmaRepository = turmaRepository;
    }
    
    public async Task<Aluno> CreateAsync(Aluno aluno)
    {
        var existe = await _alunoRepository.FindEmailAsync(aluno.Email);
        if (existe != null)
            throw new EmailDuplicadoException();
        
        var turma = await _turmaRepository.GetByIdAsync(aluno.TurmaId);
        if (turma == null)
            throw new TurmaNaoEncontradaException();
        
        try
        {
            await _alunoRepository.AddAsync(aluno);
        }
        catch (DbUpdateException ex) 
            when (ex.InnerException?.Message.Contains("UNIQUE") == true)
        {
            throw new EmailDuplicadoException();
        }
        return aluno;
    }

    public async Task<Aluno> UpdateAsync(Aluno aluno)
    {
        var alunoExistente = await GetById(aluno.Id);
        var result = await _alunoRepository.FindEmailAsync(aluno.Email,aluno.Id);
        if (result != null)
            throw new EmailDuplicadoException();
        
        alunoExistente.Nome = aluno.Nome;
        alunoExistente.Email = aluno.Email;
        
        try
        {
            await _alunoRepository.SaveChangesAsync();
        }
        catch (DbUpdateException ex) 
            when (ex.InnerException?.Message.Contains("UNIQUE") == true)
        {
            throw new EmailDuplicadoException();
        }
        return alunoExistente;
    }

    public async Task<Aluno> GetById(Guid id)
    {
        var aluno = await _alunoRepository.GetByIdAsync(id);
        if (aluno == null)
            throw new CadastroNaoEncontradoException();
        return aluno;
    }

    public async Task<List<Aluno>> GetAllAsync()
    {
        return await _alunoRepository.GetAllAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var aluno = await GetById(id);
        await _alunoRepository.DeleteAsync(aluno);
    }
}