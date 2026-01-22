namespace CultBook05.controller;

using CultBook05.model;
using CultBook05.testes;

class Program
{
    static Pedido? pedidoAtual = null;

    public static void InserirLivro(Cliente? clienteLogado)
    {
        if (clienteLogado == null || clienteLogado.Logado == false)
        {
            Console.WriteLine("Você precisa estar logado para inserir livros no carrinho.");
            return;
        }

        Console.WriteLine("=== Inserir Livro no Carrinho (Pedido) ===");

        Console.Write("Digite o ISBN do livro para compra: ");
        string? isbn = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(isbn))
        {
            Console.WriteLine("ISBN inválido.");
            return;
        }

        isbn = isbn.Trim();

        // Busca o livro pelo ISBN
        Livro? livroSelecionado = null;
        foreach (Livro livro in FabricaLivros.GetLivros())
        {
            if (livro.Isbn == isbn)
            {
                livroSelecionado = livro;
                break;
            }
        }

        if (livroSelecionado == null)
        {
            Console.WriteLine("Livro não encontrado.");
            return;
        }

        Console.Write("Digite a quantidade desejada: ");
        string? qtdStr = Console.ReadLine();

        if (!int.TryParse(qtdStr, out int quantidade) || quantidade <= 0)
        {
            Console.WriteLine("Quantidade inválida.");
            return;
        }

        // ✅ Se já existe um pedido, tenta somar quantidade usando o método do Pedido
        if (pedidoAtual != null && pedidoAtual.SomarQuantidadePorIsbn(isbn, quantidade))
        {
            Console.WriteLine("Livro já estava no carrinho. Quantidade atualizada com sucesso!");
            Console.WriteLine($"Total atualizado: R$ {pedidoAtual.GetValorTotal():F2}");
            return;
        }

        // Se não existia no pedido (ou não existe pedido ainda), cria um novo ItemDePedido
        ItemDePedido novoItem = new ItemDePedido(livroSelecionado, quantidade);

        if (pedidoAtual == null)
        {
            // Primeiro item obrigatório no construtor
            pedidoAtual = new Pedido(
                numero: "001",
                dataEmissao: DateTime.Now.ToString("dd/MM/yyyy"),
                formapagamento: "Não definido",
                situacao: "Em aberto",
                item: novoItem
            );

            pedidoAtual.Cliente = clienteLogado;

            Console.WriteLine("Pedido criado e item inserido com sucesso!");
            Console.WriteLine($"Total atual: R$ {pedidoAtual.GetValorTotal():F2}");
            return;
        }

        if (!pedidoAtual.InserirItem(novoItem))
        {
            // InserirItem já imprime motivo (limite, etc.)
            return;
        }

        Console.WriteLine("Item inserido no pedido com sucesso!");
        Console.WriteLine($"Total atual: R$ {pedidoAtual.GetValorTotal():F2}");

        Console.WriteLine("\n=== ITEM INSERIDO ===");
        Console.WriteLine(novoItem.ToString());
    }

    const int OP_LOGIN = 1;
    const int OP_CADASTRAR = 2;
    const int OP_BUSCAR = 3;
    const int OP_INSERIR = 4;
    const int OP_REMOVER = 5;
    const int OP_CARRINHO = 6;
    const int OP_COMPRA = 7;
    const int OP_SAIR = 0;

    public static void BuscarLivros()
    {
        List<Livro> livros = FabricaLivros.GetLivros();
        foreach (Livro livro in livros)
        {
            Console.WriteLine(livro.ToString());
        }
    }

    static void Main(string[] args)
    {
        bool executando = true;

        Cliente[] clientes = new Cliente[20];
        int qtdClientes = 0;

        Cliente? clienteLogado = null;

        do
        {
            Console.Clear();
            Console.WriteLine("=== CultBook ===");
            Console.WriteLine("1 - Login");
            Console.WriteLine("2 - Cadastrar");
            Console.WriteLine("3 - Buscar Livros - Lista de Livros");
            Console.WriteLine("4 - Inserir Livro - no carrinho");
            Console.WriteLine("5 - Remover Livro - do carrinho");
            Console.WriteLine("6 - Ver Carrinho");
            if (clienteLogado != null && clienteLogado.Logado)
                Console.WriteLine("7 - Efetuar compra");
            else
                Console.WriteLine("7 - Efetuar compra (desabilitado - faça login)");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");

            // int opcao = int.Parse(Console.ReadLine());
            Console.Write("Escolha uma opção: ");
            string? entrada = Console.ReadLine();

            // validação de entrada => se der certo pega o valor de entrada e da saida pra opcao
            if (!int.TryParse(entrada, out int opcao))
            {
                Console.WriteLine("Opção inválida. Digite um número.");
                Console.ReadKey();
                continue; // volta pro menu
            }
            switch (opcao)
            {
                case OP_LOGIN:
                    Console.WriteLine("Login - Dadivos Mock");
                    clienteLogado = new Cliente(
                        "Douglas",
                        "douglas",
                        "123",
                        "douglas@teste.com",
                        "(44) 9 9901-3434"
                    );
                    clienteLogado.VerificarLogin();
                    Console.ReadKey();
                    break;

                case OP_CADASTRAR:
                    Console.WriteLine("Cadastrar - Em construção");
                    Console.ReadKey();
                    break;

                case OP_BUSCAR:
                    Console.WriteLine("Buscar Livros - Dados Mock");
                    FabricaLivros.GetLivros().ForEach(livro => Console.WriteLine(livro.ToString()));
                    Console.ReadKey();
                    break;

                case OP_INSERIR:
                    Console.WriteLine("Inserir Livro no Carrinho");
                    InserirLivro(clienteLogado);
                    Console.ReadKey();
                    break;

                case OP_REMOVER:
                    Console.WriteLine("Remover Livro - Em construção");
                    Console.ReadKey();
                    break;

                case OP_CARRINHO:
                    if (clienteLogado == null || clienteLogado.Logado == false)
                    {
                        Console.WriteLine("Você precisa estar logado para ver o carrinho.");
                        Console.ReadKey();
                        break;
                    }

                    if (pedidoAtual == null)
                    {
                        Console.WriteLine("Carrinho vazio (nenhum pedido em aberto).");
                        Console.ReadKey();
                        break;
                    }

                    pedidoAtual.Mostrar();
                    Console.ReadKey();
                    break;

                case OP_COMPRA:
                    Console.WriteLine("Efetuar Compra - Em construção");
                    Console.ReadKey();
                    break;

                case OP_SAIR:
                    executando = false;
                    break;

                default:
                    Console.WriteLine("Opção inválida");
                    Console.ReadKey();
                    break;
            }
        } while (executando);
    }
}
