namespace CultBook12.controller.menus;

using CultBook12.model.entities.clientes;
using CultBook12.model.entities.pedidos;
using CultBook12.model.usecases.pedidos;

public static class MenuPedidos
{
    public static Pedido? Abrir(
        Cliente? clienteLogado,
        Pedido? pedidoAtual,
        EfetuarCompraUseCase efetuarCompraUc
    )
    {
        bool executando = true;

        while (executando)
        {
            Console.Clear();
            Console.WriteLine("=== MENU PEDIDOS ===");
            Console.WriteLine("1 - Ver Carrinho (pedido em aberto)");
            Console.WriteLine("2 - Listar Meus Pedidos (histórico)");
            Console.WriteLine("3 - Efetuar Compra");
            Console.WriteLine("0 - Voltar");
            Console.Write("Escolha uma opção: ");

            if (!int.TryParse(Console.ReadLine(), out int opcao))
            {
                Console.WriteLine("Opção inválida.");
                Console.ReadKey();
                continue;
            }

            switch (opcao)
            {
                case 1:
                    VerCarrinho(clienteLogado, pedidoAtual);
                    break;

                case 2:
                    ListarPedidos(clienteLogado);
                    break;

                case 3:
                    pedidoAtual = EfetuarCompra(clienteLogado, pedidoAtual, efetuarCompraUc);
                    break;

                case 0:
                    executando = false;
                    break;

                default:
                    Console.WriteLine("Opção inválida.");
                    Console.ReadKey();
                    break;
            }
        }

        return pedidoAtual;
    }

    private static void VerCarrinho(Cliente? clienteLogado, Pedido? pedidoAtual)
    {
        Console.Clear();

        if (clienteLogado == null || !clienteLogado.Logado)
        {
            Console.WriteLine("Você precisa estar logado.");
            Console.ReadKey();
            return;
        }

        if (pedidoAtual == null || pedidoAtual.GetQtdItens() == 0)
        {
            Console.WriteLine("Carrinho vazio.");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("=== CARRINHO ===");
        pedidoAtual.Mostrar();
        Console.ReadKey();
    }

    private static void ListarPedidos(Cliente? clienteLogado)
    {
        Console.Clear();

        if (clienteLogado == null || !clienteLogado.Logado)
        {
            Console.WriteLine("Você precisa estar logado.");
            Console.ReadKey();
            return;
        }

        var pedidos = clienteLogado.GetPedidos();

        Console.WriteLine("=== MEUS PEDIDOS ===");

        if (pedidos.Count == 0)
        {
            Console.WriteLine("Nenhum pedido no histórico.");
            Console.ReadKey();
            return;
        }

        for (int i = 0; i < pedidos.Count; i++)
        {
            Console.WriteLine($"--- Pedido {i + 1} ---");
            Console.WriteLine(pedidos[i].ToString());
            Console.WriteLine();
        }

        Console.ReadKey();
    }

    private static Pedido? EfetuarCompra(
        Cliente? clienteLogado,
        Pedido? pedidoAtual,
        EfetuarCompraUseCase efetuarCompraUc
    )
    {
        try
        {
            Console.Clear();

            if (clienteLogado == null || !clienteLogado.Logado)
                throw new Exception("Você precisa estar logado.");

            if (pedidoAtual == null || pedidoAtual.GetQtdItens() == 0)
                throw new Exception("Carrinho vazio.");

            Console.WriteLine("=== EFETUAR COMPRA ===");
            pedidoAtual.Mostrar();

            Console.Write("\nForma de pagamento (PIX, Cartão, Dinheiro...): ");
            string? forma = Console.ReadLine();

            Console.Write("Confirmar compra? (S/N): ");
            string conf = (Console.ReadLine() ?? "").Trim().ToUpperInvariant();

            if (conf != "S")
            {
                Console.WriteLine("Compra cancelada.");
                return pedidoAtual;
            }

            efetuarCompraUc.Executar(clienteLogado, pedidoAtual, forma);

            Console.WriteLine("Compra realizada com sucesso!");
            return null; // limpa carrinho
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return pedidoAtual;
        }
        finally
        {
            Console.ReadKey();
        }
    }
}
