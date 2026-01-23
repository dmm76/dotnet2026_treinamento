namespace CultBook05.model.usecases.livros;

using CultBook05.model.entities.clientes;
using CultBook05.model.entities.pedidos;
using CultBook05.model.interfaces;

public class InserirLivroCarrinhoUseCase
{
    private readonly ILivroRepositorio _repo;

    public InserirLivroCarrinhoUseCase(ILivroRepositorio repo)
    {
        _repo = repo;
    }

    public Pedido Executar(Cliente clienteLogado, Pedido? pedidoAtual, string isbn, int quantidade)
    {
        if (!clienteLogado.Logado)
            throw new Exception("Usuário não logado");

        var livro = _repo.BuscarPorIsbn(isbn);
        if (livro == null)
            throw new Exception("Livro não encontrado");

        if (quantidade <= 0)
            throw new Exception("Quantidade inválida");

        // se já existe pedido e o livro já estava lá
        if (pedidoAtual != null && pedidoAtual.SomarQuantidadePorIsbn(isbn, quantidade))
        {
            return pedidoAtual;
        }

        var item = new ItemDePedido(livro, quantidade);

        if (pedidoAtual == null)
        {
            pedidoAtual = new Pedido(
                "001",
                DateTime.Now.ToString("dd/MM/yyyy"),
                "Não definido",
                "Em aberto",
                item
            );
            pedidoAtual.Cliente = clienteLogado;
            return pedidoAtual;
        }

        pedidoAtual.InserirItem(item);
        return pedidoAtual;
    }
}
