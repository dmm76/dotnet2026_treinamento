namespace CultBook11.model.entities.livros;

using System.Text;

public abstract class Livro
{
    public string Isbn { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public string Autor { get; set; }
    public int Estoque { get; set; }
    public decimal Preco { get; set; }
    public string Figura { get; set; }
    public int DataCadastro { get; set; }
    public string Categoria { get; set; }

    public Livro(
        string isbn,
        string titulo,
        string descricao,
        string autor,
        int estoque,
        decimal preco,
        string figura,
        int dataCadastro,
        string categoria
    )
    {
        Isbn = isbn;
        Titulo = titulo;
        Descricao = descricao;
        Autor = autor;
        Estoque = estoque;
        Preco = preco;
        Figura = figura;
        DataCadastro = dataCadastro;
        Categoria = categoria;
    }

    // Atualizacao Lab06
    public abstract decimal CalcularPrecoTotal();

    public void Mostrar()
    {
        Console.WriteLine(ToString());
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("======LIVRO======");
        sb.AppendLine($"ISBN: {Isbn}");
        sb.AppendLine($"Título: {Titulo}");
        sb.AppendLine($"Descrição: {Descricao}");
        sb.AppendLine($"Autor: {Autor}");
        sb.AppendLine($"Estoque: {Estoque}");
        sb.AppendLine($"Preço: {Preco:C}");
        sb.AppendLine($"Figura: {Figura}");
        sb.AppendLine($"DataCadastro: {DataCadastro}");
        sb.AppendLine($"Categoria: {Categoria}");
        sb.AppendLine("------------------------------");
        return sb.ToString();
    }
}
