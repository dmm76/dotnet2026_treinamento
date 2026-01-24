using System.Text.Json;

namespace CultBook08.infra.config;

public static class ConfiguracaoLoader
{
    public static Configuracao CarregarOuPadrao(string caminhoJsonRelativo)
    {
        try
        {
            // 1) tenta pelo caminho relativo direto (normalmente funciona no dotnet run)
            var caminhoJson = Path.GetFullPath(caminhoJsonRelativo);

            if (!File.Exists(caminhoJson))
                return new Configuracao("BR", "pt-BR", "");

            var json = File.ReadAllText(caminhoJson);

            var dto = JsonSerializer.Deserialize<ConfiguracaoJsonDto>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            if (dto == null)
                return new Configuracao("BR", "pt-BR", "");

            // 2) resolve o ajuda.txt relativo ao MESMO diretório do JSON
            // (isso é “pasta do projeto”, como você quer)
            var dirJson = Path.GetDirectoryName(caminhoJson) ?? Directory.GetCurrentDirectory();

            var caminhoAjuda = dto.CaminhoArquivo ?? "";
            if (!string.IsNullOrWhiteSpace(caminhoAjuda) && !Path.IsPathRooted(caminhoAjuda))
            {
                caminhoAjuda = Path.GetFullPath(Path.Combine(dirJson, caminhoAjuda));
            }

            return new Configuracao(dto.Regiao ?? "BR", dto.Idioma ?? "pt-BR", caminhoAjuda);
        }
        catch
        {
            // retorna padrão em caso de erro
            return new Configuracao("BR", "pt-BR", "");
        }
    }

    public static void Salvar(string caminhoJsonRelativo, ConfiguracaoJsonDto dto)
    {
        // salva na pasta do projeto (diretório atual + relativo)
        var caminhoJson = Path.GetFullPath(caminhoJsonRelativo);

        var dir = Path.GetDirectoryName(caminhoJson);
        if (!string.IsNullOrWhiteSpace(dir) && !Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        var json = JsonSerializer.Serialize(
            dto,
            new JsonSerializerOptions { WriteIndented = true }
        );
        File.WriteAllText(caminhoJson, json);
    }
}
