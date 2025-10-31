using System.ComponentModel.DataAnnotations;
using CP2.Infrastructure.Persistence.Entitites;

namespace CP2.DTOs;

public class TurmaDTO
{
    [Required(ErrorMessage = "O identificador pra turma é obrigatório")]
    [StringLength(200, MinimumLength = 4, ErrorMessage = "Informe um identificador válido Ex. 2TDSPR")]
    public string Identificador { get; set; }

    [Required(ErrorMessage = "O Semestre é obrigatório")]
    [Range(1, 2, ErrorMessage = "Informe um semestre válido (1 ou 2)")]
    public int Semestre { get; set; }
    
    [Required(ErrorMessage = "O Turno é obrigatório")]
    [RegularExpression(@"^(manha|noite)$", ErrorMessage = "O turno deve ser 'manha' ou 'noite'")]
    public string Turno { get; set; }

    public static Turma ToEntity(TurmaDTO dto)
    {
        return new Turma
        {
            Identificador = dto.Identificador,
            Semestre = dto.Semestre,
            Turno = dto.Turno
        };
    }
}