// namespace CultBook09.testes;

// using System;
// using System.IO;
// using System.Text;
// using System.Text.Json;

// public class Test
// {
//     public static void Main(string[] args)
//     {
//         try
//         {
//             // 1) Lê o JSON
//             string caminhoJson = Path.Combine("testes", "utils", "arquivo.json");
//             caminhoJson = Path.GetFullPath(caminhoJson);

//             if (!File.Exists(caminhoJson))
//                 throw new CaminhoInvalido($"Arquivo JSON não encontrado: {caminhoJson}");

//             string jsonContent = File.ReadAllText(caminhoJson);
//             var root = JsonDocument.Parse(jsonContent).RootElement;

//             string regiao =
//                 (root.TryGetProperty("regiao", out var pRegiao) ? pRegiao.GetString() : null)
//                 ?? "BR";
//             string idioma =
//                 (root.TryGetProperty("idioma", out var pIdioma) ? pIdioma.GetString() : null)
//                 ?? "PT-BR";
//             string caminhoArquivo =
//                 (
//                     root.TryGetProperty("caminhoArquivo", out var pCaminho)
//                         ? pCaminho.GetString()
//                         : null
//                 ) ?? "";

//             // 2) Cria Configuração (aqui dentro você valida e lança CaminhoInvalido)
//             var config = new Configuracao(regiao, idioma, caminhoArquivo);

//             Console.WriteLine("Configuração carregada:");
//             Console.WriteLine(config.ToString());
//             Console.WriteLine();

//             // Debug: mostra caminho absoluto (ajuda muito)
//             Console.WriteLine("Caminho absoluto do arquivo de ajuda:");
//             Console.WriteLine(Path.GetFullPath(config.CaminhoArquivo));
//             Console.WriteLine();

//             // 3) Lê o conteúdo do ajuda.txt
//             // Se tiver acentuação estranha, tente trocar para Encoding.Latin1 ou Encoding.UTF8.
//             string conteudoAjuda = File.ReadAllText(config.CaminhoArquivo, Encoding.UTF8);

//             Console.WriteLine("=== CONTEÚDO DO ARQUIVO DE AJUDA ===");
//             Console.WriteLine(conteudoAjuda);
//         }
//         catch (CaminhoInvalido ex)
//         {
//             Console.WriteLine("Erro: configuração/caminho inválido.");
//             Console.WriteLine(ex.Message);
//         }
//         catch (UnauthorizedAccessException ex)
//         {
//             Console.WriteLine("Erro: sem permissão para acessar o arquivo.");
//             Console.WriteLine(ex.Message);
//         }
//         catch (FileNotFoundException ex)
//         {
//             Console.WriteLine("Erro: arquivo não encontrado.");
//             Console.WriteLine(ex.Message);
//         }
//         catch (DirectoryNotFoundException ex)
//         {
//             Console.WriteLine("Erro: diretório não encontrado.");
//             Console.WriteLine(ex.Message);
//         }
//         catch (IOException ex)
//         {
//             Console.WriteLine("Erro de I/O (arquivo em uso, permissão, etc.).");
//             Console.WriteLine(ex.Message);
//         }
//         catch (JsonException ex)
//         {
//             Console.WriteLine("Erro: JSON inválido.");
//             Console.WriteLine(ex.Message);
//         }
//         catch (Exception ex)
//         {
//             Console.WriteLine("Erro inesperado:");
//             Console.WriteLine(ex);
//         }
//     }
// }
