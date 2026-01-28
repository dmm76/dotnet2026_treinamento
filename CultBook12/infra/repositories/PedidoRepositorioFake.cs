using System.Collections.Concurrent;
using CultBook12.model.entities.pedidos;
using CultBook12.model.interfaces;

namespace CultBook12.infra.repositories;

public class PedidoRepositorioFake : IPedidoRepositorio
{
    // carrinho por cliente
    private readonly ConcurrentDictionary<string, Pedido> _abertos = new();

    private static string Key(string clienteId) => (clienteId ?? "").Trim().ToLowerInvariant();

    public Pedido? ObterAberto(string clienteId)
    {
        var k = Key(clienteId);
        if (string.IsNullOrWhiteSpace(k))
            return null;

        return _abertos.TryGetValue(k, out var pedido) ? pedido : null;
    }

    public void SalvarAberto(string clienteId, Pedido pedido)
    {
        var k = Key(clienteId);
        if (string.IsNullOrWhiteSpace(k))
            throw new ArgumentException("clienteId inválido.");

        if (pedido == null)
            throw new ArgumentNullException(nameof(pedido));

        _abertos[k] = pedido;
    }

    public void LimparAberto(string clienteId)
    {
        var k = Key(clienteId);
        if (string.IsNullOrWhiteSpace(k))
            return;

        _abertos.TryRemove(k, out _);
    }
}
