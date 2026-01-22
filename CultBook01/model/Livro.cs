namespace CultBook01.model;
class Livro
{
    private string Isbn {get; set;}
    private string Titulo {get; set;}
    private string Descricao {get; set;}
    private string Autor {get; set;}
    private int Estoque {get; set;}
    private double Preco {get; set;}
    private string Figura {get; set;}
    private int DataCadastro {get; set;}
    private string Categoria {get; set;}

    public Livro(string isbn, string titulo, string descricao, string autor, int estoque, double preco, string figura, int dataCadastro, string categoria)
    {
        Isbn = isbn;
        Titulo = titulo;
        Descricao = descricao;
        Autor = autor;
        Estoque = estoque;
        Preco = preco;
        Figura = figura;
        DataCadastro =dataCadastro;
        Categoria = categoria;
    }

    public void Mostrar()
    {
        Console.WriteLine(Isbn, Titulo, Descricao, Autor, Estoque, Preco, Figura, DataCadastro, Categoria);
    }
    
}