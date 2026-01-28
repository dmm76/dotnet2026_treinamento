namespace CultBook12.infra.config;

using System.Text;

public class ConfiguracaoUsuario
{
    public string Regiao { get; set; } = "BR";
    public string Idioma { get; set; } = "pt-BR";

    public ConfiguracaoUsuario() { }

    public ConfiguracaoUsuario(string regiao, string idioma)
    {
        Regiao = regiao;
        Idioma = idioma;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Configuração selecionada:");
        sb.AppendLine($"Região: {Regiao}");
        sb.AppendLine($"Idioma: {Idioma}");
        return sb.ToString();
    }
}
