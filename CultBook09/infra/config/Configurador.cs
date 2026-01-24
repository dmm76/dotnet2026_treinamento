// Configurador.cs
namespace CultBook09.infra.config;

using System.Text.Json;

public static class Configurador
{
    // Defaults se o arquivo.json não existir ou der erro
    private const string DEFAULT_REGIAO = "BR";
    private const string DEFAULT_IDIOMA = "pt-BR";
    private const string DEFAULT_AJUDA_RELATIVO = "infra/config/utils/ajuda.txt";

    /// <summary>
    /// Carrega a configuração a partir do seu arquivo JSON (ex: arquivo.json).
    /// Esse JSON aponta para o arquivo de ajuda (ajuda.txt).
    /// </summary>
    public static Configuracao Carregar(string caminhoArquivoJson)
    {
        string regiao = DEFAULT_REGIAO;
        string idioma = DEFAULT_IDIOMA;

        // Por padrão, ajuda.txt relativo ao diretório atual
        string ajuda = DEFAULT_AJUDA_RELATIVO;

        try
        {
            if (!File.Exists(caminhoArquivoJson))
                return new Configuracao(regiao, idioma, ajuda);

            string json = File.ReadAllText(caminhoArquivoJson);

            var dto = JsonSerializer.Deserialize<ConfigDto>(json);
            if (dto == null)
                return new Configuracao(regiao, idioma, ajuda);

            if (!string.IsNullOrWhiteSpace(dto.Regiao))
                regiao = dto.Regiao.Trim();
            if (!string.IsNullOrWhiteSpace(dto.Idioma))
                idioma = dto.Idioma.Trim();

            // resolve caminho do ajuda.txt
            var baseDir =
                Path.GetDirectoryName(Path.GetFullPath(caminhoArquivoJson))
                ?? Directory.GetCurrentDirectory();

            var arqAjuda = (dto.CaminhoArquivo ?? "").Trim(); // no seu JSON é "caminhoArquivo"

            if (string.IsNullOrWhiteSpace(arqAjuda))
            {
                ajuda = Path.GetFullPath(Path.Combine(baseDir, DEFAULT_AJUDA_RELATIVO));
            }
            else if (Path.IsPathRooted(arqAjuda))
            {
                ajuda = arqAjuda;
            }
            else
            {
                ajuda = Path.GetFullPath(Path.Combine(baseDir, arqAjuda));
            }
        }
        catch
        {
            // mantém defaults
        }

        return new Configuracao(regiao, idioma, ajuda);
    }

    private class ConfigDto
    {
        public string? Regiao { get; set; }
        public string? Idioma { get; set; }

        // no seu JSON a chave é "caminhoArquivo"
        public string? CaminhoArquivo { get; set; }
    }
}
