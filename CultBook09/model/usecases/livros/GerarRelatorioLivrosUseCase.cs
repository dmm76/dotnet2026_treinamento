namespace CultBook09.model.usecases.livros;

using System.Text;
using CultBook09.model.dtos;

public class GerarRelatorioLivrosUseCase
{
    public string Executar(List<LivroDetalheDto> livros)
    {
        var sb = new StringBuilder();
        sb.AppendLine("==== LISTA DE LIVROS ====\n");

        foreach (var livro in livros)
        {
            sb.AppendLine("================================");
            sb.AppendLine($"Tipo:      {livro.Tipo}");
            sb.AppendLine($"ISBN:      {livro.Isbn}");
            sb.AppendLine($"Título:    {livro.Titulo}");
            sb.AppendLine($"Autor:     {livro.Autor}");
            sb.AppendLine($"Preço:     R$ {livro.Preco:F2}");
            sb.AppendLine($"Estoque:   {livro.Estoque}");
            sb.AppendLine($"Categoria: {livro.Categoria}");

            if (livro.TamanhoMB.HasValue)
                sb.AppendLine($"Tamanho:   {livro.TamanhoMB.Value:F2} MB");

            if (livro.TempoDuracao.HasValue)
                sb.AppendLine($"Duração:   {livro.TempoDuracao.Value} min");

            if (!string.IsNullOrWhiteSpace(livro.Narrador))
                sb.AppendLine($"Narrador:  {livro.Narrador}");

            if (livro.Peso.HasValue)
                sb.AppendLine($"Peso:      {livro.Peso.Value:F2} kg");

            if (livro.ValorFrete.HasValue)
                sb.AppendLine($"Frete:     R$ {livro.ValorFrete.Value:F2}");

            sb.AppendLine("================================\n");
        }

        return sb.ToString();
    }
}
