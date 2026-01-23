namespace CultBook05.infra.config;

public class CaminhoInvalidoException : Exception
{
    //Construtor com mensagem padrão
    public CaminhoInvalidoException()
        : base("O caminho do arquivo é inválido.") { }

    //Construtor com mensagem personalizada
    public CaminhoInvalidoException(string mensagem)
        : base(mensagem) { }

    //Construto com mensagem e inner exception
    public CaminhoInvalidoException(string mensagem, Exception innerException)
        : base(mensagem, innerException) { }
}
