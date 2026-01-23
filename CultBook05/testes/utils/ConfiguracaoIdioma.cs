namespace CultBook05.testes.utils;

public class ConfiguracaoUsuario
{
    public string Regiao { get; }
    public string Idioma { get; }

    public ConfiguracaoUsuario(string regiao, string idioma)
    {
        Regiao = regiao;
        Idioma = idioma;
    }

    public override string ToString() => $"Região: {Regiao} | Idioma: {Idioma}";
}
