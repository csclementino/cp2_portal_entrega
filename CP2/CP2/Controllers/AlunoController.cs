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
[SwaggerTag("Manipulação dos cadastros de Alunos (CRUD). " +
            "Permite criar, atualizar, buscar e deletar os cadastros no sistema.")]

public class AlunoController : ControllerBase
{
    private readonly IAlunoUseCase  _alunoUseCase;

    public AlunoController(IAlunoUseCase alunoUseCase)
    {
        _alunoUseCase = alunoUseCase;
    }
    
    [HttpPost]
    [SwaggerOperation(Summary = "Criar cadastro do aluno", Description = "Adiciona um novo cadastro do aluno")]
    [SwaggerResponse((int)HttpStatusCode.Created, "Cadastro criado com êxito",  typeof(AlunoResponse))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro na requisição")]
    [SwaggerResponse((int)HttpStatusCode.Conflict, "Conflito nas informações de cadastro")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Turma não encontrada")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    public async Task<ActionResult<Aluno>> PostAluno(AlunoDTO alunoDto)
    {
        try
        {
            var alunoCriado = await _alunoUseCase.CreateAsync(AlunoDTO.ToEntity(alunoDto));
            return Created("", AlunoResponse.ToResponse(alunoCriado));
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
    [SwaggerOperation(Summary = "Atualizar cadastro do aluno", Description = "Altera todo o cadastro do aluno informado")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Cadastro atualizado com êxito",  typeof(AlunoResponse))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro na requisição")]
    [SwaggerResponse((int)HttpStatusCode.Conflict, "Conflito nas informações de cadastro")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Aluno não encontrado")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    public async Task<ActionResult<Aluno>> PutAluno(AlunoDTOUpdate alunoDtoUpdate)
    {
        try
        {
            var alunoAtualizado = await _alunoUseCase.UpdateAsync(AlunoDTOUpdate.ToEntity(alunoDtoUpdate));
            return Ok(AlunoResponse.ToResponse(alunoAtualizado));
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
    [SwaggerOperation(Summary = "Buscar cadastro do aluno", Description = "Encontra o cadastro de um aluno especifico pelo ID")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Cadastro do aluno é retornado",  typeof(AlunoResponse))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro na requisição")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Aluno não encontrado")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    public async Task<ActionResult<Aluno>> GetAluno(Guid id)
    {
        try
        {
            var aluno = await _alunoUseCase.GetById(id);
            return Ok(AlunoResponse.ToResponse(aluno));
        }
        catch (CadastroNaoEncontradoException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    [HttpGet]
    [SwaggerOperation(Summary = "Buscar todos alunos", Description = "Traz todos os alunos cadastrados atualmente")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Lista de alunos retornada",  typeof(List<AlunoResponse>))]
    [SwaggerResponse((int)HttpStatusCode.NoContent, "Sem conteúdo")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    public async Task<ActionResult<Aluno>> GetAll()
    {
        List<Aluno> alunos = await _alunoUseCase.GetAllAsync();
        if (!alunos.Any())
        {
            return NoContent();
        }
        var alunosDto = alunos.Select(AlunoResponse.ToResponse).ToList();
        return Ok(alunosDto);
    }
    
    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Deletar cadastro do aluno", Description = "Apaga o cadastro de um aluno na aplicação")]
    [SwaggerResponse((int)HttpStatusCode.NoContent, "Sem conteúdo")]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro na requisição")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Aluno não encontrado")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            await _alunoUseCase.DeleteAsync(id);
            return NoContent();
        }
        catch (CadastroNaoEncontradoException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
}