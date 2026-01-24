namespace CultBook09.infra.config;

public class Ajuda
{
    public string Texto { get; private set; } = "Ajuda não disponível.";

    public Ajuda(string caminhoAjudaTxt)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(caminhoAjudaTxt))
            {
                Texto = "Arquivo de ajuda não configurado.";
                return;
            }

            if (!File.Exists(caminhoAjudaTxt))
            {
                Texto = $"Arquivo de ajuda não encontrado: {caminhoAjudaTxt}";
                return;
            }

            Texto = File.ReadAllText(caminhoAjudaTxt);
        }
        catch (Exception ex)
        {
            Texto = $"Erro ao ler ajuda: {ex.Message}";
        }
    }
}
