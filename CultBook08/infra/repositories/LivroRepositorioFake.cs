using CultBook08.infra.data.factory;
using CultBook08.model.entities.livros;
using CultBook08.model.interfaces;

namespace CultBook08.infra.repositories;

public class LivroRepositorioFake : ILivroRepositorio
{
    private readonly List<Livro> _livros;

    public LivroRepositorioFake()
    {
        _livros = FabricaLivros.BuscarTodos().ToList();
    }

    public List<Livro> BuscarTodos() => _livros.ToList();

    public Livro? BuscarPorIsbn(string isbn) => _livros.FirstOrDefault(l => l.Isbn == isbn);

    public void Adicionar(Livro livro) => _livros.Add(livro);

    public void Atualizar(Livro livro)
    {
        var existente = BuscarPorIsbn(livro.Isbn);
        if (existente == null)
            return;

        _livros.Remove(existente);
        _livros.Add(livro);
    }

    public void Remover(string isbn)
    {
        var existente = BuscarPorIsbn(isbn);
        if (existente == null)
            return;

        _livros.Remove(existente);
    }
}
