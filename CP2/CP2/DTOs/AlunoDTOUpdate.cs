using System.ComponentModel.DataAnnotations;
using CP2.Infrastructure.Persistence.Entitites;

namespace CP2.DTOs;

public class AlunoDTOUpdate
{
    [Required(ErrorMessage = "O ID é obrigatório")]
    public Guid Id { get; set; }
    
    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(200, MinimumLength = 2, ErrorMessage = "Informe um nome válido")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O email é obrigatório")]
    [EmailAddress(ErrorMessage = "Formato de email inválido")]
    [StringLength(200, ErrorMessage = "Informe um email válido")]
    public string Email { get; set; }
    
    public static Aluno ToEntity(AlunoDTOUpdate dto)
    {
        return new Aluno
        {
            Id = dto.Id,
            Nome = dto.Nome,
            Email = dto.Email
        };
    }
}