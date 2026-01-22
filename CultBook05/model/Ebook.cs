namespace CultBook05.model;

public class Ebook : Livro
{
    public double TamanhoMB { get; set; }

    public Ebook(
        string isbn,
        string titulo,
        string descricao,
        string autor,
        int estoque,
        double preco,
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
    public override double CalcularPrecoTotal()
    {
        return Preco;
    }

    public override string ToString()
    {
        return "Tipo: E-book\n"
            + base.ToString()
            + $"Tamanho (MB): {TamanhoMB:F2}\n"
            + "------------------------------\n";
    }
}
