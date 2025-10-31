using System.ComponentModel.DataAnnotations;
using CP2.Infrastructure.Persistence.Entitites;

namespace CP2.DTOs;

public class TurmaDTOUpdate 
{
    [Required(ErrorMessage = "O ID é obrigatório")]
    public Guid Id { get; set; }
    
    [Required(ErrorMessage = "O Semestre é obrigatório")]
    [Range(1, 2, ErrorMessage = "Informe um semestre válido (1 ou 2)")]
    public int Semestre { get; set; }
    
    [Required(ErrorMessage = "O Turno é obrigatório")]
    [RegularExpression(@"^(manha|noite)$", ErrorMessage = "O turno deve ser 'manha' ou 'noite'")]
    public string Turno { get; set; }
    
    public static Turma ToEntity(TurmaDTOUpdate dto)
    {
        return new Turma
        {
            Id = dto.Id,
            Semestre = dto.Semestre,
            Turno = dto.Turno,
        };
    }
}