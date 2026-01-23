using CultBook05.infra.data.factory;
using CultBook05.model.entities;

namespace CultBook05.infra.repositories;

public class LivroRepositorioFake : ILivroRepositorio
{
    private List<Livro> livros;

    public LivroRepositorioFake()
    {
        livros = new List<Livro>(FabricaLivros.BuscarTodos());
    }

    public List<Livro> BuscarTodos()
    {
        return livros;
    }

    public Livro BuscarPorIsbn(string isbn)
    {
        return livros.FirstOrDefault(l => l.Isbn == isbn);
    }

    public void Adicionar(Livro livro)
    {
        livros.Add(livro);
    }

    public void Atualizar(Livro livro)
    {
        var livroExistente = BuscarPorIsbn(livro.Isbn);
        if (livroExistente != null)
        {
            livros.Remove(livroExistente);
            livros.Add(livro);
        }
    }

    public void Remover(string isbn)
    {
        var livro = BuscarPorIsbn(isbn);
        if (livro != null)
        {
            livros.Remove(livro);
        }
    }
}
