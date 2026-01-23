namespace CultBook05.infra.config;

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
