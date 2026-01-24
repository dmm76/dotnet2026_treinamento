using CultBook08.model.dtos;
using CultBook08.model.entities.livros;
using CultBook08.model.interfaces;

namespace CultBook08.model.usecases.livros;

public class ListarLivrosUseCase
{
    private readonly ILivroRepositorio _repo;

    public ListarLivrosUseCase(ILivroRepositorio repo)
    {
        _repo = repo;
    }

    public List<LivroDetalheDto> Executar()
    {
        var livros = _repo.BuscarTodos();
        return livros.Select(Mapear).ToList();
    }

    static LivroDetalheDto Mapear(Livro l)
    {
        var dto = new LivroDetalheDto
        {
            Isbn = l.Isbn,
            Titulo = l.Titulo,
            Descricao = l.Descricao,
            Autor = l.Autor,
            Estoque = l.Estoque,
            Preco = l.Preco,
            Figura = l.Figura,
            DataCadastro = l.DataCadastro,
            Categoria = l.Categoria,
        };

        switch (l)
        {
            case Ebook e:
                dto.Tipo = "E-book";
                dto.TamanhoMB = e.TamanhoMB;
                break;

            case AudioLivro a:
                dto.Tipo = "ÁudioLivro";
                dto.TempoDuracao = a.TempoDuracao;
                dto.Narrador = a.Narrador;
                break;

            case LivroFisico f:
                dto.Tipo = "Livro Físico";
                dto.Peso = f.Peso;
                dto.ValorFrete = f.ValorFrete;
                break;
            default:
                throw new NotSupportedException(
                    $"Tipo de livro não mapeado no DTO: {l.GetType().Name}"
                );
        }

        return dto;
    }
}
