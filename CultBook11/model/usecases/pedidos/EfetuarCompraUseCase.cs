namespace CultBook11.model.usecases.pedidos;

using CultBook11.model.entities.clientes;
using CultBook11.model.entities.pedidos;

public class EfetuarCompraUseCase
{
    public void Executar(Cliente? clienteLogado, Pedido? pedidoAtual, string? formaPagamento = null)
    {
        if (clienteLogado == null || !clienteLogado.Logado)
            throw new Exception("Você precisa estar logado para efetuar a compra.");

        if (pedidoAtual == null)
            throw new Exception("Carrinho vazio (nenhum pedido em aberto).");

        if (pedidoAtual.GetQtdItens() == 0)
            throw new Exception("Carrinho vazio.");

        // opcional: valida forma de pagamento se você quiser
        formaPagamento = (formaPagamento ?? "").Trim();
        if (string.IsNullOrWhiteSpace(formaPagamento))
            formaPagamento = "Não informado";

        // marca dados no pedido (se você tiver setters/métodos depois, ajusta aqui)
        // Como seus campos no Pedido são private set, a forma mais limpa é criar um método no Pedido:
        // pedidoAtual.FinalizarCompra(formaPagamento);
        // Por enquanto, vamos só mudar a situacao via método que vamos criar abaixo.

        pedidoAtual.FinalizarCompra(formaPagamento);

        // associa pedido ao cliente (Cliente agora tem List<Pedido>)
        clienteLogado.InserirPedido(pedidoAtual);
    }
}
