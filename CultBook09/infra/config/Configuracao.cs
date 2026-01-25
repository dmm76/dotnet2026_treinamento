namespace CultBook09.infra.config;

using System.Text;

public class Configuracao
{
    private const string BASE_CONFIG = "infra/config/utils";

    public string Regiao { get; }
    public string Idioma { get; }

    // o que vem do JSON (ex: "ajuda.txt")
    public string CaminhoAjuda { get; }

    // caminho completo (ex: "infra/config/utils/ajuda.txt")
    public string CaminhoAjudaCompleto => Path.Combine(BASE_CONFIG, CaminhoAjuda);

    public Configuracao(string regiao, string idioma, string caminhoAjuda)
    {
        Regiao = string.IsNullOrWhiteSpace(regiao) ? "BR" : regiao.Trim();
        Idioma = string.IsNullOrWhiteSpace(idioma) ? "pt-BR" : idioma.Trim();

        // fallback pro ajuda.txt se vier vazio
        CaminhoAjuda = string.IsNullOrWhiteSpace(caminhoAjuda) ? "ajuda.txt" : caminhoAjuda.Trim();
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine("======CONFIGURAÇÃO======");
        sb.AppendLine($"Região: {Regiao}");
        sb.AppendLine($"Idioma: {Idioma}");
        sb.AppendLine($"Arquivo de Ajuda: {CaminhoAjuda}");
        sb.AppendLine($"Ajuda (completo): {CaminhoAjudaCompleto}");
        sb.AppendLine("------------------------------");
        return sb.ToString();
    }
}
