using CultBook12.infra.data.factory;
using CultBook12.model.entities.livros;
using CultBook12.model.interfaces;

namespace CultBook12.infra.repositories;

public class LivroRepositorioFake : ILivroRepositorio
{
    private readonly List<Livro> _livros;

    public LivroRepositorioFake()
    {
        _livros = FabricaLivros.BuscarTodos().ToList();
    }

    private static string NormalizarIsbn(string isbn) => (isbn ?? "").Trim();

    public List<Livro> BuscarTodos() => _livros.ToList();

    public Livro? BuscarPorIsbn(string isbn)
    {
        string chave = NormalizarIsbn(isbn);
        if (string.IsNullOrWhiteSpace(chave))
            return null;

        return _livros.FirstOrDefault(l =>
            string.Equals(l.Isbn, chave, StringComparison.OrdinalIgnoreCase)
        );
    }

    public void Adicionar(Livro livro)
    {
        if (livro == null)
            throw new ArgumentNullException(nameof(livro));

        string chave = NormalizarIsbn(livro.Isbn);
        if (string.IsNullOrWhiteSpace(chave))
            throw new ArgumentException("ISBN inválido.", nameof(livro));

        if (BuscarPorIsbn(chave) != null)
            throw new InvalidOperationException("Já existe um livro com esse ISBN.");

        _livros.Add(livro);
    }

    public void Atualizar(Livro livro)
    {
        if (livro == null)
            throw new ArgumentNullException(nameof(livro));

        string chave = NormalizarIsbn(livro.Isbn);
        if (string.IsNullOrWhiteSpace(chave))
            throw new ArgumentException("ISBN inválido.", nameof(livro));

        int idx = _livros.FindIndex(l =>
            string.Equals(l.Isbn, chave, StringComparison.OrdinalIgnoreCase)
        );

        if (idx < 0)
            return;

        _livros[idx] = livro; // mantém ordem
    }

    public void Remover(string isbn)
    {
        string chave = NormalizarIsbn(isbn);
        if (string.IsNullOrWhiteSpace(chave))
            return;

        var existente = BuscarPorIsbn(chave);
        if (existente == null)
            return;

        _livros.Remove(existente);
    }
}
