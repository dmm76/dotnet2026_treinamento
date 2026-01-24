namespace CultBook08.controller;

using System;
using System.Globalization;
//interfaces e usecases
using CultBook08.controller.menus;
using CultBook08.infra.config;
using CultBook08.infra.data.factory;
using CultBook08.infra.data.repositorios;
using CultBook08.infra.repositories;
using CultBook08.model.entities.clientes;
using CultBook08.model.entities.pedidos;
using CultBook08.model.interfaces;
using CultBook08.model.usecases.clientes;
using CultBook08.model.usecases.livros;

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
    const int OP_LOGOUT = 11;
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

    static Cliente? OpcaoLogin()
    {
        try
        {
            Console.WriteLine("\n=== LOGIN ===");

            Console.Write("Login: ");
            var login = Console.ReadLine() ?? "";

            Console.Write("Senha: ");
            var senha = Console.ReadLine() ?? "";

            _loginClienteUc.Executar(login, senha);

            // após autenticar, pega o cliente do repo (já com Logado=true)
            var cliente = _clienteRepo.BuscarPorLogin(login.Trim());

            Console.WriteLine("Login realizado com sucesso!");
            return cliente;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
        finally
        {
            Console.ReadKey();
        }
    }

    static Cliente? OpcaoLogout(Cliente? clienteLogado)
    {
        try
        {
            if (clienteLogado == null || !clienteLogado.Logado)
            {
                Console.WriteLine("Nenhum cliente logado.");
                return clienteLogado;
            }

            _logoutClienteUc.Executar(clienteLogado.Login ?? "");

            Console.WriteLine("Logout realizado com sucesso!");
            return null; // zera o clienteLogado
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return clienteLogado;
        }
        finally
        {
            Console.ReadKey();
        }
    }

    static CultureInfo AplicarConfiguracao(ConfiguracaoUsuario config)
    {
        var cultura = new CultureInfo(config.Idioma);

        CultureInfo.CurrentCulture = cultura;
        CultureInfo.CurrentUICulture = cultura;

        return cultura;
    }

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

        if (clientes.Count == 0)
        {
            Console.WriteLine("Nenhum cliente cadastrado.");
            return;
        }

        foreach (var c in clientes)
        {
            c.Mostrar();
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

        //verifica se ja tem clientes cadastrados no repo fake, se nao tiver, cria alguns
        Cliente? clienteLogado = null;
        if (_clienteRepo.BuscarTodos().Count == 0)
        {
            foreach (var c in FabricaClientes.CriarMock())
                _clienteRepo.Adicionar(c);
        }

        // Carrega do arquivo.json (Regiao/Idioma/CaminhoArquivo)
        var cfg = ConfiguracaoLoader.CarregarOuPadrao(CAMINHO_JSON);

        // configAtual é só a parte que o menu usa
        ConfiguracaoUsuario configAtual = new ConfiguracaoUsuario(cfg.Regiao, cfg.Idioma);

        // aplica cultura
        CultureInfo culturaAtual = AplicarConfiguracao(configAtual);

        do
        {
            Console.Clear();
            string dataFormatada = DateTime.Now.ToString("f", culturaAtual);
            Console.WriteLine($"Bem-vindo à CultBook! {dataFormatada}");
            Console.WriteLine($"Região: {configAtual.Regiao} | Idioma: {configAtual.Idioma}");
            Console.WriteLine();
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
            if (clienteLogado != null && clienteLogado.Logado)
                Console.WriteLine("11 - Logout");
            else
                Console.WriteLine("11 - Logout (desabilitado - ninguém logado)");
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
                    clienteLogado = OpcaoLogin();
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
                    configAtual = ConfiguracaoMenu.EscolherRegiaoEIdioma();
                    culturaAtual = AplicarConfiguracao(configAtual);

                    ConfiguracaoLoader.Salvar(
                        CAMINHO_JSON,
                        new ConfiguracaoJsonDto
                        {
                            Regiao = configAtual.Regiao,
                            Idioma = configAtual.Idioma,
                            // mantém relativo (do jeito que você quer)
                            CaminhoArquivo = "ajuda.txt",
                        }
                    );

                    Console.Clear();
                    Console.WriteLine("=== CONFIGURAÇÃO SALVA E APLICADA ===");
                    Console.WriteLine(configAtual);
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
                case OP_LOGOUT:
                    clienteLogado = OpcaoLogout(clienteLogado);
                    pedidoAtual = null; // limpa o carrinho ao fazer logout
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
