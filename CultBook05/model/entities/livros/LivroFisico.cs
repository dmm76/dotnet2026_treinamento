namespace CultBook05.model.entities.livros;

public class LivroFisico : Livro
{
    public double Peso { get; set; }
    public decimal ValorFrete { get; set; }

    public LivroFisico(
        string isbn,
        string titulo,
        string descricao,
        string autor,
        int estoque,
        decimal preco,
        string figura,
        int dataCadastro,
        string categoria,
        double peso,
        decimal valorFrete
    )
        : base(isbn, titulo, descricao, autor, estoque, preco, figura, dataCadastro, categoria)
    {
        Peso = peso;
        ValorFrete = valorFrete;
    }

    public override decimal CalcularPrecoTotal()
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
