using CultBook12.model.entities.livros;
using CultBook12.model.interfaces;

namespace CultBook12.model.usecases.livros;

public class AtualizarLivroUseCase
{
    private readonly ILivroRepositorio _repo;

    public AtualizarLivroUseCase(ILivroRepositorio repo)
    {
        _repo = repo;
    }

    public void Executar(Livro livroAtualizado)
    {
        if (livroAtualizado == null)
            throw new ArgumentNullException(nameof(livroAtualizado));

        livroAtualizado.Isbn = (livroAtualizado.Isbn ?? "").Trim();

        if (string.IsNullOrWhiteSpace(livroAtualizado.Isbn))
            throw new ArgumentException("ISBN é obrigatório.");

        var existente = _repo.BuscarPorIsbn(livroAtualizado.Isbn);
        if (existente == null)
            throw new InvalidOperationException("Livro não encontrado para atualizar.");

        // regra: não permitir trocar o tipo sem querer
        if (existente.GetType() != livroAtualizado.GetType())
            throw new InvalidOperationException(
                "Não é permitido alterar o tipo do livro na atualização."
            );

        // validações comuns
        if (string.IsNullOrWhiteSpace(livroAtualizado.Titulo))
            throw new ArgumentException("Título é obrigatório.");

        if (string.IsNullOrWhiteSpace(livroAtualizado.Autor))
            throw new ArgumentException("Autor é obrigatório.");

        if (string.IsNullOrWhiteSpace(livroAtualizado.Categoria))
            throw new ArgumentException("Categoria é obrigatória.");

        if (livroAtualizado.Estoque < 0)
            throw new ArgumentException("Estoque não pode ser negativo.");

        if (livroAtualizado.Preco < 0)
            throw new ArgumentException("Preço não pode ser negativo.");

        // validações específicas por tipo
        ValidarPorTipo(livroAtualizado);

        _repo.Atualizar(livroAtualizado);
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
