namespace CultBook05.testes;

using System.IO;

public class Configuracao
{
    public string Regiao { get; }
    public string Idioma { get; }
    public string CaminhoArquivo { get; }

    public Configuracao(string regiao, string idioma, string caminhoArquivo)
    {
        // Defaults pra não ficar null/vazio
        Regiao = string.IsNullOrWhiteSpace(regiao) ? "BR" : regiao;
        Idioma = string.IsNullOrWhiteSpace(idioma) ? "PT-BR" : idioma;

        // Valida o caminho do arquivo de ajuda
        if (string.IsNullOrWhiteSpace(caminhoArquivo))
            throw new CaminhoInvalido("O caminho do arquivo de ajuda veio vazio no JSON.");

        // Remove espaços acidentais no começo/fim
        caminhoArquivo = caminhoArquivo.Trim();

        // Valida caracteres inválidos no caminho
        if (caminhoArquivo.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
            throw new CaminhoInvalido($"O caminho contém caracteres inválidos: {caminhoArquivo}");

        // Valida existência do arquivo
        if (!File.Exists(caminhoArquivo))
            throw new CaminhoInvalido($"Arquivo de ajuda não encontrado: {caminhoArquivo}");

        CaminhoArquivo = caminhoArquivo;
    }

    public override string ToString() =>
        $"Região: {Regiao} | Idioma: {Idioma} | Caminho do Arquivo: {CaminhoArquivo}";
}
