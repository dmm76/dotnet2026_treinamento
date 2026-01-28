using CultBook12.model.entities.livros;
using CultBook12.model.interfaces;

namespace CultBook12.model.usecases.livros;

public class CadastrarLivroUseCase
{
    private readonly ILivroRepositorio _repo;

    public CadastrarLivroUseCase(ILivroRepositorio repo)
    {
        _repo = repo;
    }

    public void Executar(Livro livro)
    {
        if (livro == null)
            throw new ArgumentNullException(nameof(livro));

        livro.Isbn = (livro.Isbn ?? "").Trim();

        if (string.IsNullOrWhiteSpace(livro.Isbn))
            throw new ArgumentException("ISBN é obrigatório.");

        if (string.IsNullOrWhiteSpace(livro.Titulo))
            throw new ArgumentException("Título é obrigatório.");

        if (string.IsNullOrWhiteSpace(livro.Autor))
            throw new ArgumentException("Autor é obrigatório.");

        if (string.IsNullOrWhiteSpace(livro.Categoria))
            throw new ArgumentException("Categoria é obrigatória.");

        if (livro.Estoque < 0)
            throw new ArgumentException("Estoque não pode ser negativo.");

        if (livro.Preco < 0)
            throw new ArgumentException("Preço não pode ser negativo.");

        // validações específicas por tipo
        ValidarPorTipo(livro);

        // duplicidade
        if (_repo.BuscarPorIsbn(livro.Isbn) != null)
            throw new InvalidOperationException("Já existe um livro com esse ISBN.");

        _repo.Adicionar(livro);
    }

    private static void ValidarPorTipo(Livro livro)
    {
        switch (livro)
        {
            case LivroFisico f:
                if (f.Peso <= 0)
                    throw new ArgumentException("Peso deve ser maior que 0.");
                if (f.ValorFrete < 0)
                    throw new ArgumentException("Frete não pode ser negativo.");
                break;

            case Ebook e:
                if (e.TamanhoMB <= 0)
                    throw new ArgumentException("Tamanho (MB) deve ser maior que 0.");
                break;

            case AudioLivro a:
                if (a.TempoDuracao <= 0)
                    throw new ArgumentException("Tempo de duração deve ser maior que 0.");
                if (string.IsNullOrWhiteSpace(a.Narrador))
                    throw new ArgumentException("Narrador é obrigatório no ÁudioLivro.");
                break;

            default:
                throw new NotSupportedException(
                    $"Tipo de livro não suportado: {livro.GetType().Name}"
                );
        }
    }
}
