namespace CultBook09.model.entities.livros;

using System.Text;

public class Ebook : Livro
{
    public double TamanhoMB { get; set; }

    public Ebook(
        string isbn,
        string titulo,
        string descricao,
        string autor,
        int estoque,
        decimal preco,
        string figura,
        int dataCadastro,
        string categoria,
        double tamanhoMB
    )
        : base(isbn, titulo, descricao, autor, estoque, preco, figura, dataCadastro, categoria)
    {
        TamanhoMB = tamanhoMB;
    }

    // Atualizacao Lab06
    public override decimal CalcularPrecoTotal()
    {
        return Preco;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(base.ToString());
        sb.AppendLine($"Tamanho (MB): {TamanhoMB}");
        return sb.ToString();
    }
}
