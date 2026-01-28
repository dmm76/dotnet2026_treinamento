using CultBook12.model.interfaces;

namespace CultBook12.model.usecases.livros;

public class RemoverLivroUseCase
{
    private readonly ILivroRepositorio _repo;

    public RemoverLivroUseCase(ILivroRepositorio repo)
    {
        _repo = repo;
    }

    public void Executar(string isbn)
    {
        isbn = (isbn ?? "").Trim();

        if (string.IsNullOrWhiteSpace(isbn))
            throw new ArgumentException("ISBN é obrigatório.");

        var existente = _repo.BuscarPorIsbn(isbn);
        if (existente == null)
            throw new InvalidOperationException("Livro não encontrado.");

        _repo.Remover(isbn);
    }
}
