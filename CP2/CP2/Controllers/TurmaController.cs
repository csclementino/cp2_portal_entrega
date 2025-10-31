using System.Net;
using CP2.DTOs;
using CP2.Exceptions;
using CP2.Infrastructure.Persistence.Entitites;
using CP2.UseCase;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CP2.Controllers;

[ApiController]
[Route("[controller]")]
[SwaggerTag("Manipulação dos cadastros das turmas (CRUD). " +
            "Permite criar, atualizar, buscar e deletar os cadastros no sistema.")]

public class TurmaController : ControllerBase
{
    private readonly ITurmaUseCase  _turmaUseCase;

    public TurmaController(ITurmaUseCase turmaUseCase)
    {
        _turmaUseCase = turmaUseCase;
    }
    
    
    [HttpPost]
    [SwaggerOperation(Summary = "Criar cadastro de uma turma", Description = "Adiciona uma nova turma na aplicação")]
    [SwaggerResponse((int)HttpStatusCode.Created, "Cadastro criado com êxito",  typeof(TurmaResponse))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro na requisição")]
    [SwaggerResponse((int)HttpStatusCode.Conflict, "Conflito nas informações de cadastro")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    public async Task<ActionResult<Aluno>> PostTurma(TurmaDTO turmaDto)
    {
        try
        {
            var turmaCriada = await _turmaUseCase.CreateAsync(TurmaDTO.ToEntity(turmaDto));
            return Created("", TurmaResponse.ToResponse(turmaCriada));
        }
        catch (TurmaDuplicadaException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }
    
    [HttpPut]
    [SwaggerOperation(Summary = "Atualizar informações da turma", Description = "Altera todas as informações da turma informada")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Cadastro atualizado com êxito",  typeof(TurmaResponse))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro na requisição")]
    [SwaggerResponse((int)HttpStatusCode.Conflict, "Conflito nas informações de cadastro")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Turma não encontrada")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    public async Task<ActionResult<Aluno>> PutTurma(TurmaDTOUpdate turmaDtoUpdate)
    {
        try
        {
            var turmaAtualizada = await _turmaUseCase.UpdateAsync(TurmaDTOUpdate.ToEntity(turmaDtoUpdate));
            return Ok(TurmaResponse.ToResponse(turmaAtualizada));
        }
        catch (TurmaDuplicadaException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (CadastroNaoEncontradoException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Buscar informações de uma turma", Description = "Encontra o cadastro de uma turma especifica pelo ID")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Cadastro da turma é retornado",  typeof(TurmaResponse))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro na requisição")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Turma não encontrada")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    public async Task<ActionResult<Aluno>> GetTurma(Guid id)
    {
        try
        {
            var turma = await _turmaUseCase.GetById(id);
            return Ok(TurmaResponse.ToResponse(turma));
        }
        catch (CadastroNaoEncontradoException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    [HttpGet]
    [SwaggerOperation(Summary = "Buscar todas as turmas", Description = "Traz todas as turmas cadastrados atualmente")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Lista de clientes retornada",  typeof(List<TurmaResponse>))]
    [SwaggerResponse((int)HttpStatusCode.NoContent, "Sem conteúdo")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    public async Task<ActionResult<Aluno>> GetAll()
    {
        List<Turma> turmas = await _turmaUseCase.GetAllAsync();
        if (!turmas.Any())
        {
            return NoContent();
        }
        var turmasDto = turmas.Select(TurmaResponse.ToResponse).ToList();
        return Ok(turmasDto);
    }
    
    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Deletar uma turma", Description = "Apaga o cadastro de uma turma especifica")]
    [SwaggerResponse((int)HttpStatusCode.NoContent, "Sem conteúdo")]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro na requisição")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Turma não encontrada")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            await _turmaUseCase.DeleteAsync(id);
            return NoContent();
        }
        catch (CadastroNaoEncontradoException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}