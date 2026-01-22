namespace CultBook05.model;

public class LivroFisico : Livro
{
    public double Peso { get; set; }
    public double ValorFrete { get; set; }

    public LivroFisico(
        string isbn,
        string titulo,
        string descricao,
        string autor,
        int estoque,
        double preco,
        string figura,
        int dataCadastro,
        string categoria,
        double peso,
        double valorFrete
    )
        : base(isbn, titulo, descricao, autor, estoque, preco, figura, dataCadastro, categoria)
    {
        Peso = peso;
        ValorFrete = valorFrete;
    }

    public override double CalcularPrecoTotal()
    {
        return Preco + ValorFrete;
    }

    public override string ToString()
    {
        return "Tipo: Livro Físico\n"
            + base.ToString()
            + $"Peso: {Peso:F2}\n"
            + $"Frete: {ValorFrete:F2}\n"
            + "------------------------------\n";
    }
}
