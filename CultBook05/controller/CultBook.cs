namespace CultBook05.controller;

using System;
using CultBook05.controller.menus;
//interfaces e usecases
using CultBook05.infra.repositories;
using CultBook05.model.entities.clientes;
using CultBook05.model.entities.pedidos;
using CultBook05.model.interfaces;
using CultBook05.model.usecases.clientes;
using CultBook05.model.usecases.livros;

class Program
{
    static Pedido? pedidoAtual = null;
    const int OP_LOGIN = 1;
    const int OP_CADASTRAR = 2;
    const int OP_BUSCAR = 3;
    const int OP_INSERIR = 4;
    const int OP_REMOVER = 5;
    const int OP_CARRINHO = 6;
    const int OP_COMPRA = 7;
    const int OPATIVAR_IDIOMA = 8;
    const int OP_CADASTRAR_CLIENTE = 9;
    const int OP_LISTAR_CLIENTES = 10;
    const int OP_SAIR = 0;

    //injecao de dependencia simples
    static readonly ILivroRepositorio _livroRepo = new LivroRepositorioFake();
    static readonly ListarLivrosUseCase _listarLivros = new ListarLivrosUseCase(_livroRepo);

    static readonly GerarRelatorioLivrosUseCase _relatorioLivros =
        new GerarRelatorioLivrosUseCase();
    static readonly InserirLivroCarrinhoUseCase _inserirLivroCarrinho =
        new InserirLivroCarrinhoUseCase(_livroRepo);
    static readonly CadastrarClienteUseCase _cadastrarCliente = new CadastrarClienteUseCase();
    static readonly ListarClientesUseCase _listarClientesUc = new ListarClientesUseCase();

    static void OpcaoCadastrarCliente()
    {
        try
        {
            Console.WriteLine("\n=== CADASTRO DE CLIENTE ===");

            Console.Write("Nome: ");
            string nome = Console.ReadLine() ?? "";

            Console.Write("Login: ");
            string login = Console.ReadLine() ?? "";

            Console.Write("Senha: ");
            string senha = Console.ReadLine() ?? "";

            Console.Write("Email: ");
            string email = Console.ReadLine() ?? "";

            Console.Write("Fone: ");
            string fone = Console.ReadLine() ?? "";

            _cadastrarCliente.Executar(nome, login, senha, email, fone);

            Console.WriteLine("Cliente cadastrado com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    static void OpcaoListarClientes()
    {
        Console.WriteLine("\n=== CLIENTES CADASTRADOS ===");

        var clientes = _listarClientesUc.Executar();
        int qtd = _listarClientesUc.GetQtd();

        for (int i = 0; i < qtd; i++)
        {
            clientes[i].Mostrar();
            Console.WriteLine("------------------------");
        }
    }

    static Pedido? OpcaoInserirLivro(Cliente? clienteLogado, Pedido? pedidoAtual)
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

            pedidoAtual = _inserirLivroCarrinho.Executar(clienteLogado, pedidoAtual, isbn, qtd);

            Console.WriteLine("Livro inserido com sucesso!");
            Console.WriteLine($"Total: R$ {pedidoAtual.GetValorTotal():F2}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Console.ReadKey();
        return pedidoAtual;
    }

    static void Main(string[] args)
    {
        bool executando = true;

        //Cliente[] clientes = new Cliente[20];
        // int qtdClientes = 0;

        Cliente? clienteLogado = null;

        do
        {
            Console.Clear();
            string dataFormatada = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            Console.WriteLine($"Bem-vindo à CultBook! {dataFormatada}");
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
            Console.WriteLine("8 - Ativar Idioma e Região");
            Console.WriteLine("9 - Cadastrar Cliente");
            Console.WriteLine("10 - Listar Clientes Cadastrados");
            Console.WriteLine("0 - Sair");
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
                    Console.WriteLine("Login - Dados Mock");
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
                    var livros = _listarLivros.Executar();
                    var relatorio = _relatorioLivros.Executar(livros);
                    Console.WriteLine(relatorio);
                    Console.ReadKey();
                    break;

                case OP_INSERIR:
                    pedidoAtual = OpcaoInserirLivro(clienteLogado, pedidoAtual);
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
                case OPATIVAR_IDIOMA:
                    var config = ConfiguracaoMenu.EscolherRegiaoEIdioma();
                    Console.WriteLine($"Configuração selecionada: {config}");
                    Console.ReadKey();
                    break;
                case OP_CADASTRAR_CLIENTE:
                    OpcaoCadastrarCliente();
                    Console.ReadKey();
                    break;
                case OP_LISTAR_CLIENTES:
                    OpcaoListarClientes();
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
