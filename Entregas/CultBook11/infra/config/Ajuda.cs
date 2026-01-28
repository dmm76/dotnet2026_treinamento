using System.Globalization;

namespace CultBook11.infra.config;

public class Ajuda
{
    private readonly string _caminhoBase; // ex: C:\...\infra\config\utils\ajuda.txt

    public Ajuda(string caminhoBase)
    {
        _caminhoBase = caminhoBase;
    }

    public string Texto => Ler();

    private string Ler()
    {
        try
        {
            // Se o arquivo base nem existir, já retorna algo visível
            if (string.IsNullOrWhiteSpace(_caminhoBase))
                return "[Ajuda] Caminho base vazio.";

            var pasta = Path.GetDirectoryName(_caminhoBase) ?? "";
            var nomeBase = Path.GetFileNameWithoutExtension(_caminhoBase); // ajuda
            var ext = Path.GetExtension(_caminhoBase); // .txt

            var ui = CultureInfo.CurrentUICulture.Name; // pt-BR
            var lang = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName; // pt

            // 1) ajuda.pt-BR.txt
            var c1 = Path.Combine(pasta, $"{nomeBase}.{ui}{ext}");
            // 2) ajuda.pt.txt
            var c2 = Path.Combine(pasta, $"{nomeBase}.{lang}{ext}");
            // 3) ajuda.txt (base)
            var c3 = _caminhoBase;

            var caminhoFinal =
                File.Exists(c1) ? c1
                : File.Exists(c2) ? c2
                : c3;

            if (!File.Exists(caminhoFinal))
                return $"[Ajuda] Arquivo não encontrado: {caminhoFinal}";

            var texto = File.ReadAllText(caminhoFinal);

            // Se por algum motivo veio vazio, deixa explícito
            if (string.IsNullOrWhiteSpace(texto))
                return $"[Ajuda] Arquivo lido mas está vazio: {caminhoFinal}";

            return texto;
        }
        catch (Exception ex)
        {
            // NÃO engole o erro e retorna vazio. Mostra uma mensagem útil.
            return $"[Ajuda] Erro ao ler ajuda: {ex.Message}";
        }
    }
}
