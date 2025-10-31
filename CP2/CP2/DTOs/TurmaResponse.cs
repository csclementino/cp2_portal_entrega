﻿using CP2.Infrastructure.Persistence.Entitites;

namespace CP2.DTOs;

public class TurmaResponse
{
    public string Identificador { get; set; }
    public int Semestre { get; set; }
    public string Turno { get; set; }
    
    public Guid Id { get; set; }
    
    public static TurmaResponse ToResponse(Turma turma)
    {
        return new TurmaResponse
        {
            Id = turma.Id,
            Identificador = turma.Identificador,
            Semestre = turma.Semestre,
            Turno = turma.Turno
        };
    }
}