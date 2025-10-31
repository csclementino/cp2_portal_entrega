using System.ComponentModel.DataAnnotations;
using CP2.Infrastructure.Persistence.Entitites;

namespace CP2.DTOs;

public class ProfessorDTO
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(200, MinimumLength = 2, ErrorMessage = "Informe um nome válido")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O email é obrigatório")]
    [EmailAddress(ErrorMessage = "Formato de email inválido")]
    [StringLength(200, ErrorMessage = "Informe um email válido")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "O pf é obrigatório")]
    [RegularExpression(@"^\d{6}$", ErrorMessage = "O PF deve conter exatamente 6 números")]
    public string Pf { get; set; }
    
    [Required(ErrorMessage = "A turma é obrigatória")]
    public Guid TurmaId { get; set; }
    
    public static Professor ToEntity(ProfessorDTO dto)
    {
        return new Professor
        {
            Nome = dto.Nome,
            Email = dto.Email,
            Pf = dto.Pf,
            TurmaId = dto.TurmaId
        };
    }
}