namespace CultBook11.controller.menus;

using System;
using CultBook11.model.entities.clientes;
using CultBook11.model.entities.pedidos;
using CultBook11.model.usecases.livros;

public static class MenuLivro
{
    public static void OpcaoBuscarLivros(
        ListarLivrosUseCase listarLivrosUc,
        GerarRelatorioLivrosUseCase relatorioUc
    )
    {
        var livros = listarLivrosUc.Executar();
        var relatorio = relatorioUc.Executar(livros);

        Console.WriteLine(relatorio);
    }

    public static Pedido? OpcaoInserirLivro(
        Cliente? clienteLogado,
        Pedido? pedidoAtual,
        InserirLivroCarrinhoUseCase inserirLivroCarrinhoUc
    )
    {
        try
        {
            if (clienteLogado == null || !clienteLogado.Logado)
            {
                Console.WriteLine("Você precisa estar logado para inserir livros no carrinho.");
                Console.ReadKey();
                return pedidoAtual;
            }

            Console.Write("ISBN: ");
            var isbn = (Console.ReadLine() ?? "").Trim();

            if (string.IsNullOrWhiteSpace(isbn))
            {
                Console.WriteLine("ISBN inválido.");
                Console.ReadKey();
                return pedidoAtual;
            }

            Console.Write("Quantidade: ");
            var qtdStr = Console.ReadLine();

            if (!int.TryParse(qtdStr, out int qtd) || qtd <= 0)
            {
                Console.WriteLine("Quantidade inválida.");
                Console.ReadKey();
                return pedidoAtual;
            }

            pedidoAtual = inserirLivroCarrinhoUc.Executar(clienteLogado, pedidoAtual, isbn, qtd);

            Console.WriteLine("Livro inserido com sucesso!");
            Console.WriteLine($"Total: {pedidoAtual.GetValorTotal():C}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Console.ReadKey();
        return pedidoAtual;
    }

    public static void OpcaoRemoverLivro(
        Cliente? clienteLogado,
        Pedido? pedidoAtual,
        RemoverLivroCarrinhoUseCase removerUc
    )
    {
        try
        {
            if (clienteLogado == null || !clienteLogado.Logado)
                throw new Exception("Você precisa estar logado para remover itens do carrinho.");

            if (pedidoAtual == null || pedidoAtual.GetQtdItens() == 0)
                throw new Exception("Carrinho vazio.");

            Console.Clear();
            Console.WriteLine("=== REMOVER LIVRO DO CARRINHO ===");
            pedidoAtual.Mostrar();

            Console.Write("\nDigite o ISBN do livro para remover (1 unidade): ");
            string? isbn = Console.ReadLine();

            removerUc.Executar(pedidoAtual, isbn);

            Console.WriteLine("Livro removido do carrinho!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            Console.ReadKey();
        }
    }
}
