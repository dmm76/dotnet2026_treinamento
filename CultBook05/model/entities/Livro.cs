namespace CultBook05.model.entities;

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
        Console.WriteLine($"ISBN: {Isbn}");
        Console.WriteLine($"Título: {Titulo}");
        Console.WriteLine($"Descrição: {Descricao}");
        Console.WriteLine($"Autor: {Autor}");
        Console.WriteLine($"Estoque: {Estoque}");
        Console.WriteLine($"Preço: R$ {Preco}");
        Console.WriteLine($"Figura: {Figura}");
        Console.WriteLine($"Data de Cadastro: {DataCadastro}");
        Console.WriteLine($"Categoria: {Categoria}");
        Console.WriteLine("------------------------------");
    }

    public override string ToString()
    {
        return $"======LIVRO======\n"
            + $"ISBN: {Isbn}\n"
            + $"Título: {Titulo}\n"
            + $"Descrição: {Descricao}\n"
            + $"Autor: {Autor}\n"
            + $"Estoque: {Estoque}\n"
            + $"Preço: {Preco}\n"
            + $"Figura: {Figura}\n"
            + $"DataCadastro: {DataCadastro}\n"
            + $"Categoria: {Categoria}"
            + $"\n------------------------------\n";
    }
}
