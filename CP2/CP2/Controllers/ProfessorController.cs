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
[SwaggerTag("Manipulação dos cadastros de Professores (CRUD). " +
            "Permite criar, atualizar, buscar e deletar os cadastros no sistema.")]

public class ProfessorController : ControllerBase
{
    private readonly IProfessorUseCase _ProfessorUseCase;

    public ProfessorController(IProfessorUseCase professorUseCase)
    {
        _ProfessorUseCase = professorUseCase;
    }
    
    [HttpPost]
    [SwaggerOperation(Summary = "Criar cadastro do professor", Description = "Adiciona um novo cadastro de professor")]
    [SwaggerResponse((int)HttpStatusCode.Created, "Cadastro criado com êxito",  typeof(ProfessorResponse))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro na requisição")]
    [SwaggerResponse((int)HttpStatusCode.Conflict, "Conflito nas informações de cadastro")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Turma não encontrada")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    public async Task<ActionResult<Aluno>> PostProfessor(ProfessorDTO professorDto)
    {
        try
        {
            var professorCriado = await _ProfessorUseCase.CreateAsync(ProfessorDTO.ToEntity(professorDto));
            return Created("", ProfessorResponse.ToResponse(professorCriado));
        }
        catch (EmailDuplicadoException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (TurmaNaoEncontradaException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    [HttpPut]
    [SwaggerOperation(Summary = "Atualizar cadastro do professor", Description = "Altera todo o cadastro do professor informado")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Cadastro atualizado com êxito",  typeof(ProfessorResponse))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro na requisição")]
    [SwaggerResponse((int)HttpStatusCode.Conflict, "Conflito nas informações de cadastro")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Professor não encontrado")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    public async Task<ActionResult<Aluno>> PutProfessor(ProfessorDTOUpdate professorDtoUpdate)
    {
        try
        {
            var professorAtualizado = await _ProfessorUseCase.UpdateAsync(ProfessorDTOUpdate.ToEntity(professorDtoUpdate));
            return Ok(ProfessorResponse.ToResponse(professorAtualizado));
        }
        catch (EmailDuplicadoException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (CadastroNaoEncontradoException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Buscar cadastro de um professor", Description = "Encontra o cadastro de um professor especifico pelo ID")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Cadastro do professor é retornado",  typeof(ProfessorResponse))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro na requisição")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Professor não encontrado")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    public async Task<ActionResult<Aluno>> GetProfessor(Guid id)
    {
        try
        {
            var professor = await _ProfessorUseCase.GetById(id);
            return Ok(ProfessorResponse.ToResponse(professor));
        }
        catch (CadastroNaoEncontradoException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    [HttpGet]
    [SwaggerOperation(Summary = "Buscar todos professores", Description = "Traz todos os professores cadastrados atualmente")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Lista de professores é retornada",  typeof(List<ProfessorResponse>))]
    [SwaggerResponse((int)HttpStatusCode.NoContent, "Sem conteúdo")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    public async Task<ActionResult<Professor>> GetAll()
    {
        List<Professor> professores = await _ProfessorUseCase.GetAllAsync();
        if (!professores.Any())
        {
            return NoContent();
        }
        var professoresDto = professores.Select(ProfessorResponse.ToResponse).ToList();
        return Ok(professoresDto);
    }
    
    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Deletar cadastro do professor", Description = "Apaga o cadastro de um professor na aplicação")]
    [SwaggerResponse((int)HttpStatusCode.NoContent, "Sem conteúdo")]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro na requisição")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Cliente não encontrado")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            await _ProfessorUseCase.DeleteAsync(id);
            return NoContent();
        }
        catch (CadastroNaoEncontradoException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}