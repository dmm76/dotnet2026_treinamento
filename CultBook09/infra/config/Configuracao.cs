// Configuracao.cs
namespace CultBook09.infra.config;

using System.Text;

public class Configuracao
{
    public string Regiao { get; }
    public string Idioma { get; }

    // Caminho do arquivo de ajuda (pode vir do arquivo.json)
    public string CaminhoAjuda { get; }

    public Configuracao(string regiao, string idioma, string caminhoAjuda)
    {
        Regiao = string.IsNullOrWhiteSpace(regiao) ? "BR" : regiao.Trim();
        Idioma = string.IsNullOrWhiteSpace(idioma) ? "pt-BR" : idioma.Trim();
        CaminhoAjuda = (caminhoAjuda ?? "").Trim();
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine("======CONFIGURAÇÃO======");
        sb.AppendLine($"Região: {Regiao}");
        sb.AppendLine($"Idioma: {Idioma}");
        sb.AppendLine($"Arquivo de Ajuda: {CaminhoAjuda}");
        sb.AppendLine("------------------------------");
        return sb.ToString();
    }
}
