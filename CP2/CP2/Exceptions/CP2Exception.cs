namespace CP2.Exceptions

{
    public class EmailDuplicadoException : Exception
    {
        public EmailDuplicadoException()
            : base("Email já cadastrado")
        {
        }
    }

    public class CadastroNaoEncontradoException : Exception
    {
        public CadastroNaoEncontradoException()
            : base("Cadastro não encontrado")
        {
        }
    }
    
    public class TurmaDuplicadaException : Exception
    {
        public TurmaDuplicadaException()
            : base("Turma já cadastrada")
        {
        }
    }
    
    public class TurmaNaoEncontradaException : Exception
    {
        public TurmaNaoEncontradaException()
            : base("Turma não encontrada")
        {
        }
    }
}