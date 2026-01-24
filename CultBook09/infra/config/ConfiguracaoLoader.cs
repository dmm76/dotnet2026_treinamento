using System.Text.Json;

namespace CultBook09.infra.config;

public static class ConfiguracaoLoader
{
    // DTO interno -> não “vaza” pro resto do projeto
    private class ConfigDto
    {
        public string? Regiao { get; set; }
        public string? Idioma { get; set; }

        // no seu JSON a chave é "caminhoArquivo"
        public string? CaminhoArquivo { get; set; }
    }

    public static Configuracao CarregarOuPadrao(string caminhoJsonRelativo)
    {
        try
        {
            var caminhoJson = Path.GetFullPath(caminhoJsonRelativo);

            if (!File.Exists(caminhoJson))
                return new Configuracao("BR", "pt-BR", "");

            var json = File.ReadAllText(caminhoJson);

            var dto = JsonSerializer.Deserialize<ConfigDto>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            if (dto == null)
                return new Configuracao("BR", "pt-BR", "");

            // resolve ajuda.txt relativo ao diretório do JSON
            var dirJson = Path.GetDirectoryName(caminhoJson) ?? Directory.GetCurrentDirectory();

            var caminhoAjuda = (dto.CaminhoArquivo ?? "").Trim();

            if (!string.IsNullOrWhiteSpace(caminhoAjuda) && !Path.IsPathRooted(caminhoAjuda))
            {
                caminhoAjuda = Path.GetFullPath(Path.Combine(dirJson, caminhoAjuda));
            }

            return new Configuracao(dto.Regiao ?? "BR", dto.Idioma ?? "pt-BR", caminhoAjuda);
        }
        catch
        {
            return new Configuracao("BR", "pt-BR", "");
        }
    }

    public static void Salvar(
        string caminhoJsonRelativo,
        string regiao,
        string idioma,
        string caminhoArquivo // normalmente "ajuda.txt"
    )
    {
        var caminhoJson = Path.GetFullPath(caminhoJsonRelativo);

        var dir = Path.GetDirectoryName(caminhoJson);
        if (!string.IsNullOrWhiteSpace(dir) && !Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        var dto = new ConfigDto
        {
            Regiao = regiao,
            Idioma = idioma,
            CaminhoArquivo = caminhoArquivo,
        };

        var json = JsonSerializer.Serialize(
            dto,
            new JsonSerializerOptions { WriteIndented = true }
        );
        File.WriteAllText(caminhoJson, json);
    }
}
