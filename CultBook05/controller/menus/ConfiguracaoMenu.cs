using CultBook05.infra.config;

namespace CultBook05.controller.menus;

public static class ConfiguracaoMenu
{
    public static ConfiguracaoUsuario EscolherRegiaoEIdioma()
    {
        string regiao = EscolherOpcao(
            "=== Escolha a Região ===",
            new Dictionary<int, string>
            {
                { 1, "BR" },
                { 2, "US" },
                { 3, "PT" },
            }
        );

        string idioma = EscolherOpcao(
            "=== Escolha o Idioma ===",
            new Dictionary<int, string>
            {
                { 1, "pt-BR" },
                { 2, "en-US" },
                { 3, "pt-PT" },
            }
        );

        return new ConfiguracaoUsuario(regiao, idioma);
    }

    private static string EscolherOpcao(string titulo, Dictionary<int, string> opcoes)
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine(titulo);

            foreach (var kv in opcoes)
                Console.WriteLine($"{kv.Key} - {kv.Value}");

            Console.Write("Digite a opção: ");
            string? entrada = Console.ReadLine();

            if (!int.TryParse(entrada, out int opcao))
            {
                Console.WriteLine("Opção inválida: digite um número.");
                continue;
            }

            if (!opcoes.ContainsKey(opcao))
            {
                Console.WriteLine("Opção inválida: escolha uma das opções do menu.");
                continue;
            }

            return opcoes[opcao];
        }
    }
}
