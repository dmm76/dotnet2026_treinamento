namespace CultBook08.infra.config;

using System.IO;
using System.Text;

public class Configuracao
{
    public string Regiao { get; }
    public string Idioma { get; }
    public string CaminhoArquivo { get; } // pode ser relativo ou absoluto

    public Configuracao(string regiao, string idioma, string caminhoArquivo)
    {
        Regiao = string.IsNullOrWhiteSpace(regiao) ? "BR" : regiao.Trim();
        Idioma = string.IsNullOrWhiteSpace(idioma) ? "pt-BR" : idioma.Trim();

        CaminhoArquivo = (caminhoArquivo ?? "").Trim();
    }

    public string LerAjudaOuPadrao()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(CaminhoArquivo))
                return "Arquivo de ajuda não configurado.";

            // tenta ler caminho como veio (relativo ao diretório atual)
            if (File.Exists(CaminhoArquivo))
                return File.ReadAllText(CaminhoArquivo);

            return $"Arquivo de ajuda não encontrado: {CaminhoArquivo}";
        }
        catch (Exception ex)
        {
            return $"Não foi possível ler o arquivo de ajuda. Motivo: {ex.Message}";
        }
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine("======CONFIGURAÇÃO======");
        sb.AppendLine($"Região: {Regiao}");
        sb.AppendLine($"Idioma: {Idioma}");
        sb.AppendLine($"Caminho do Arquivo de Ajuda: {CaminhoArquivo}");
        sb.AppendLine("------------------------------");
        return sb.ToString();
    }
}
