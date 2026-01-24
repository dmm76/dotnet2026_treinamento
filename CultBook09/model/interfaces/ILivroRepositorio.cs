using CultBook09.model.entities.livros;

namespace CultBook09.model.interfaces;

public interface ILivroRepositorio
{
    List<Livro> BuscarTodos();
    Livro? BuscarPorIsbn(string isbn);
    void Adicionar(Livro livro);
    void Atualizar(Livro livro);
    void Remover(string isbn);
}
