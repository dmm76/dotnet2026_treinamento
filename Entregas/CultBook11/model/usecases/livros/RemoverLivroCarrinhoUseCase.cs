namespace CultBook11.model.usecases.livros;

using CultBook11.model.entities.pedidos;

public class RemoverLivroCarrinhoUseCase
{
    public bool Executar(Pedido? pedidoAtual, string? isbn)
    {
        if (pedidoAtual == null)
            throw new Exception("Carrinho vazio (nenhum pedido em aberto).");

        if (pedidoAtual.GetQtdItens() == 0)
            throw new Exception("Carrinho vazio.");

        if (string.IsNullOrWhiteSpace(isbn))
            throw new Exception("ISBN inválido.");

        isbn = isbn.Trim();

        bool removeu = pedidoAtual.RemoverPorIsbn(isbn);

        if (!removeu)
            throw new Exception("Livro não encontrado no carrinho.");

        return true;
    }
}
