using System;

namespace CultBook08.infra.config;

public sealed class CaminhoInvalidoException : Exception
{
    public CaminhoInvalidoException()
        : base("O caminho do arquivo é inválido.") { }

    public CaminhoInvalidoException(string mensagem)
        : base(mensagem) { }

    public CaminhoInvalidoException(string mensagem, Exception innerException)
        : base(mensagem, innerException) { }
}
