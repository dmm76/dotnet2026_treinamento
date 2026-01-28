using CultBook12.model.entities.pedidos;

namespace CultBook12.model.interfaces;

public interface IPedidoRepositorio
{
    Pedido? ObterAberto(string clienteId);
    void SalvarAberto(string clienteId, Pedido pedido);
    void LimparAberto(string clienteId);
}
