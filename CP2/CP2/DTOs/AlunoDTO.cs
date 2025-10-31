using System.ComponentModel.DataAnnotations;
using CP2.Infrastructure.Persistence.Entitites;

namespace CP2.DTOs;

public class AlunoDTO
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(200, MinimumLength = 2, ErrorMessage = "Informe um nome válido")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O email é obrigatório")]
    [EmailAddress(ErrorMessage = "Formato de email inválido")]
    [StringLength(200, ErrorMessage = "Informe um email válido")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "O rm é obrigatório")]
    [RegularExpression(@"^\d{6}$", ErrorMessage = "O RM deve conter exatamente 6 números")]
    public string Rm { get; set; }
    
    [Required(ErrorMessage = "A turma é obrigatória")]
    public Guid TurmaId { get; set; }

    public static Aluno ToEntity(AlunoDTO dto)
    {
        return new Aluno
        {
            Nome = dto.Nome,
            Email = dto.Email,
            Rm = dto.Rm,
            TurmaId = dto.TurmaId,
            DataIngresso = DateTime.Now,
            Status = "ATIVO"
        };
    }
}