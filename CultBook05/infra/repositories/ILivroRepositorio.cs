using CultBook05.model.entities;

namespace CultBook05.infra.repositories;

interface ILivroRepositorio
{
    List<Livro> BuscarTodos();
    Livro BuscarPorIsbn(string isbn);
    void Adicionar(Livro livro);
    void Atualizar(Livro livro);
    void Remover(string isbn);
}
