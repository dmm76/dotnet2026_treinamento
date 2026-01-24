using System.Text.Json;

namespace CultBook08.infra.config;

public static class ConfiguracaoStorage
{
    private const string Arquivo = "config.json";

    public static void Salvar(ConfiguracaoUsuario config)
    {
        var json = JsonSerializer.Serialize(
            config,
            new JsonSerializerOptions { WriteIndented = true }
        );

        File.WriteAllText(Arquivo, json);
    }

    public static ConfiguracaoUsuario CarregarOuPadrao()
    {
        if (!File.Exists(Arquivo))
            return new ConfiguracaoUsuario("BR", "pt-BR");

        var json = File.ReadAllText(Arquivo);

        var config = JsonSerializer.Deserialize<ConfiguracaoUsuario>(json);

        return config ?? new ConfiguracaoUsuario("BR", "pt-BR");
    }
}
