using CultBook05.model.dtos;
using CultBook05.model.interfaces;

namespace CultBook05.model.usecases.livros;

public class ListarLivrosUseCase
{
    private readonly ILivroRepositorio _repo;

    public ListarLivrosUseCase(ILivroRepositorio repo)
    {
        _repo = repo;
    }

    public List<LivroDto> Executar()
    {
        var livros = _repo.BuscarTodos();

        return livros
            .Select(l => new LivroDto
            {
                Isbn = l.Isbn,
                Titulo = l.Titulo,
                Autor = l.Autor,
                Preco = l.Preco,
                Estoque = l.Estoque,
                Categoria = l.Categoria,
            })
            .ToList();
    }
}
