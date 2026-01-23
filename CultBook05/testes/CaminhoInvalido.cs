namespace CultBook05.testes;

public class CaminhoInvalido : Exception
{
    //Construtor com mensagem padrão
    public CaminhoInvalido()
        : base("O caminho do arquivo é inválido.") { }

    //Construtor com mensagem personalizada
    public CaminhoInvalido(string mensagem)
        : base(mensagem) { }

    //Construto com mensagem e inner exception
    public CaminhoInvalido(string mensagem, Exception innerException)
        : base(mensagem, innerException) { }
}
