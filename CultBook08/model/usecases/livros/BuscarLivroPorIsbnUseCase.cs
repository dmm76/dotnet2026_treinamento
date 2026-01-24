using CultBook08.model.dtos;
using CultBook08.model.interfaces;

namespace CultBook08.model.usecases.livros;

public class BuscarLivroPorIsbnUseCase
{
    private readonly ILivroRepositorio _repo;

    public BuscarLivroPorIsbnUseCase(ILivroRepositorio repo)
    {
        _repo = repo;
    }

    public LivroDto? Executar(string isbn)
    {
        if (string.IsNullOrWhiteSpace(isbn))
            return null;

        var livro = _repo.BuscarPorIsbn(isbn);
        if (livro == null)
            return null;

        return new LivroDto
        {
            Isbn = livro.Isbn,
            Titulo = livro.Titulo,
            Autor = livro.Autor,
            Preco = livro.Preco,
            Estoque = livro.Estoque,
            Categoria = livro.Categoria,
        };
    }
}
