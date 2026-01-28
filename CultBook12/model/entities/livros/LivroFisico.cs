namespace CultBook12.model.entities.livros;

using System.Text;

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
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(base.ToString());
        sb.AppendLine($"Peso: {Peso}");
        sb.AppendLine($"Valor Frete: {ValorFrete:F2}");
        return sb.ToString();
    }
}
