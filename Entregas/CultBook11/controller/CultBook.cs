namespace CultBook11.controller;

using System;
using System.Globalization;
using System.Text;
//interfaces e usecases
using CultBook11.controller.menus;
using CultBook11.infra.config;
using CultBook11.infra.data.factory;
using CultBook11.infra.data.repositorios;
using CultBook11.infra.repositories;
using CultBook11.model.entities.clientes;
using CultBook11.model.entities.pedidos;
using CultBook11.model.interfaces;
using CultBook11.model.usecases.clientes;
using CultBook11.model.usecases.livros;
using CultBook11.model.usecases.pedidos;

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
    const int OP_LOGOUT = 9;
    const int OP_AJUDA = 10;
    const int OP_SAIR = 0;
    const string CAMINHO_JSON = "infra/config/utils/arquivo.json";

    //injecao de dependencia simples
    static readonly ILivroRepositorio _livroRepo = new LivroRepositorioFake();
    static readonly ListarLivrosUseCase _listarLivros = new ListarLivrosUseCase(_livroRepo);

    static readonly GerarRelatorioLivrosUseCase _relatorioLivros =
        new GerarRelatorioLivrosUseCase();
    static readonly InserirLivroCarrinhoUseCase _inserirLivroCarrinho =
        new InserirLivroCarrinhoUseCase(_livroRepo);

    static readonly IClienteRepositorio _clienteRepo = new ClienteRepositorioFake();

    static readonly CadastrarClienteUseCase _cadastrarCliente = new CadastrarClienteUseCase(
        _clienteRepo
    );
    static readonly ListarClientesUseCase _listarClientesUc = new ListarClientesUseCase(
        _clienteRepo
    );
    static readonly LoginClienteUseCase _loginClienteUc = new LoginClienteUseCase(_clienteRepo);

    static readonly LogoutClienteUseCase _logoutClienteUc = new LogoutClienteUseCase(_clienteRepo);

    static readonly RemoverLivroCarrinhoUseCase _removerLivroCarrinho =
        new RemoverLivroCarrinhoUseCase();

    static readonly EfetuarCompraUseCase _efetuarCompra = new EfetuarCompraUseCase();

    static CultureInfo AplicarConfiguracao(ConfiguracaoUsuario config)
    {
        var cultura = new CultureInfo(config.Idioma);

        CultureInfo.CurrentCulture = cultura;
        CultureInfo.CurrentUICulture = cultura;

        return cultura;
    }

    static void MenuCadastrar()
    {
        bool executando = true;

        do
        {
            Console.Clear();
            Console.WriteLine("=== MENU CADASTRO ===");
            Console.WriteLine("1 - Cadastrar Cliente");
            Console.WriteLine("2 - Listar Clientes");
            Console.WriteLine("3 - Cadastrar Livro (em construção)");
            Console.WriteLine("0 - Voltar");
            Console.Write("Escolha uma opção: ");

            string? entrada = Console.ReadLine();

            if (!int.TryParse(entrada, out int opcao))
            {
                Console.WriteLine("Opção inválida. Digite um número.");
                Console.ReadKey();
                continue;
            }

            switch (opcao)
            {
                case 1:
                    ClienteMenu.OpcaoCadastrarCliente(_cadastrarCliente);
                    Console.ReadKey();
                    break;

                case 2:
                    ClienteMenu.OpcaoListarClientes(_listarClientesUc);
                    Console.ReadKey();
                    break;

                case 3:
                    Console.WriteLine("Cadastrar Livro - Em construção");
                    Console.ReadKey();
                    break;

                case 0:
                    executando = false;
                    break;

                default:
                    Console.WriteLine("Opção inválida");
                    Console.ReadKey();
                    break;
            }
        } while (executando);
    }

    static void Main(string[] args)
    {
        Console.InputEncoding = Encoding.UTF8;
        Console.OutputEncoding = Encoding.UTF8;

        bool executando = true;

        //verifica se ja tem clientes cadastrados no repo fake, se nao tiver, cria alguns
        Cliente? clienteLogado = null;
        if (_clienteRepo.BuscarTodos().Count == 0)
        {
            foreach (var c in FabricaClientes.CriarMock())
                _clienteRepo.Adicionar(c);
        }

        // Carrega do arquivo.json (Regiao/Idioma/CaminhoAjuda)
        var cfg = Configurador.Carregar(CAMINHO_JSON);
        var ajuda = new Ajuda(cfg.CaminhoAjuda);

        // configAtual é só a parte que o menu usa
        var configAtual = new ConfiguracaoUsuario(cfg.Regiao, cfg.Idioma);

        // aplica cultura
        CultureInfo culturaAtual = AplicarConfiguracao(configAtual);
        string msgAjuda = ajuda.Texto.TrimEnd();

        do
        {
            Console.Clear();
            string dataFormatada = DateTime.Now.ToString("f", culturaAtual);
            Console.WriteLine(dataFormatada.ToUpper(culturaAtual));

            Console.WriteLine($"Região: {configAtual.Regiao} | Idioma: {configAtual.Idioma}");
            Console.WriteLine($"** Bem-vindo à CultBook! **");
            Console.WriteLine();
            Console.WriteLine($"{msgAjuda}");
            Console.WriteLine();
            Console.WriteLine("1 - Login");
            Console.WriteLine("2 - Cadastrar (clientes/livros)");
            Console.WriteLine("3 - Buscar Livros - Lista de Livros");
            Console.WriteLine("4 - Inserir Livro - no carrinho");
            Console.WriteLine("5 - Remover Livro - do carrinho");
            if (clienteLogado != null && clienteLogado.Logado)
            {
                Console.WriteLine("6 - Ver Carrinho");
                Console.WriteLine("7 - Efetuar compra");
                Console.WriteLine("8 - Ativar Idioma e Região");
                Console.WriteLine("9 - Logout");
            }
            else
            {
                Console.WriteLine("6 - Ver Carrinho (desabilitado - faça login)");
                Console.WriteLine("7 - Efetuar compra (desabilitado - faça login)");
                Console.WriteLine("8 - Ativar Idioma e Região");
                Console.WriteLine("9 - Logout (ninguém logado)");
            }
            Console.WriteLine("10 - Ajuda");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");
            string? entrada = Console.ReadLine();

            // validação de entrada => se der certo pega o valor de entrada e dá saida pra opcao
            if (!int.TryParse(entrada, out int opcao))
            {
                Console.WriteLine("Opção inválida. Digite um número.");
                Console.ReadKey();
                continue; // volta pro menu
            }
            switch (opcao)
            {
                case OP_LOGIN:
                    clienteLogado = ClienteMenu.OpcaoLogin(
                        clienteLogado,
                        _clienteRepo,
                        _loginClienteUc
                    );
                    break;

                case OP_CADASTRAR:
                    MenuCadastrar();
                    break;

                case OP_BUSCAR:
                    Console.Clear();
                    MenuLivro.OpcaoBuscarLivros(_listarLivros, _relatorioLivros);
                    Console.ReadKey();
                    break;

                case OP_INSERIR:
                    pedidoAtual = MenuLivro.OpcaoInserirLivro(
                        clienteLogado,
                        pedidoAtual,
                        _inserirLivroCarrinho
                    );
                    break;

                case OP_REMOVER:
                    MenuLivro.OpcaoRemoverLivro(clienteLogado, pedidoAtual, _removerLivroCarrinho);
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
                    pedidoAtual = MenuPedidos.Abrir(clienteLogado, pedidoAtual, _efetuarCompra);
                    break;
                case OPATIVAR_IDIOMA:
                    configAtual = ConfiguracaoMenu.EscolherRegiaoEIdioma();
                    culturaAtual = AplicarConfiguracao(configAtual);

                    // recarrega a ajuda no idioma novo
                    msgAjuda = ajuda.Texto.TrimEnd();

                    ConfiguracaoLoader.Salvar(
                        CAMINHO_JSON,
                        configAtual.Regiao,
                        configAtual.Idioma,
                        "ajuda.txt"
                    );

                    Console.Clear();
                    Console.WriteLine("=== CONFIGURAÇÃO SALVA E APLICADA ===");
                    Console.WriteLine(configAtual);
                    Console.ReadKey();
                    break;

                case OP_LOGOUT:
                    clienteLogado = ClienteMenu.OpcaoLogout(clienteLogado, _logoutClienteUc);
                    pedidoAtual = null; // limpa o carrinho ao fazer logout
                    break;
                case OP_AJUDA:
                    Console.Clear();
                    Console.WriteLine("=== AJUDA ===");
                    Console.WriteLine(msgAjuda);
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
