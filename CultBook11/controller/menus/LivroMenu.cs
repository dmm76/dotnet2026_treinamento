namespace CultBook11.controller.menus;

using System;
using CultBook11.model.entities.clientes;
using CultBook11.model.entities.pedidos;
using CultBook11.model.usecases.livros;

public static class LivroMenu
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
}
