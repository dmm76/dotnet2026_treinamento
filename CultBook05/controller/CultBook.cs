namespace CultBook05.controller;

using System;
using CultBook05.infra.data.factory;
using CultBook05.model.entities;
using CultBook05.testes.utils;

class Program
{
    static Pedido? pedidoAtual = null;

    public static void CadastrarCliente()
    {
        Console.WriteLine("\n=== CADASTRO DE CLIENTE ===");

        Console.Write("Nome: ");
        string? nome = Console.ReadLine();

        Console.Write("Login: ");
        string? login = Console.ReadLine();

        Console.Write("Senha: ");
        string? senha = Console.ReadLine();

        Console.Write("Email: ");
        string? email = Console.ReadLine();

        Console.Write("Fone: ");
        string? fone = Console.ReadLine();

        // validação simples (pra não quebrar)
        if (
            string.IsNullOrWhiteSpace(nome)
            || string.IsNullOrWhiteSpace(login)
            || string.IsNullOrWhiteSpace(senha)
        )
        {
            Console.WriteLine("Nome, Login e Senha são obrigatórios.");
            return;
        }

        // login duplicado?
        if (FabricaClientes.LoginExiste(login))
        {
            Console.WriteLine("Esse login já existe. Escolha outro.");
            return;
        }

        Cliente cliente = new Cliente(
            nome.Trim(),
            login.Trim(),
            senha.Trim(),
            (email ?? "").Trim(),
            (fone ?? "").Trim()
        );

        bool ok = FabricaClientes.Inserir(cliente);

        if (!ok)
        {
            Console.WriteLine("Não foi possível cadastrar: limite de clientes atingido.");
            return;
        }

        Console.WriteLine("✅ Cliente cadastrado com sucesso!");
        cliente.Mostrar();
    }

    public static void ListarClientes()
    {
        Console.WriteLine("\n=== CLIENTES CADASTRADOS ===");
        var clientes = FabricaClientes.GetClientes();
        int qtd = FabricaClientes.GetQtd();

        for (int i = 0; i < qtd; i++)
        {
            clientes[i].Mostrar();
            Console.WriteLine("------------------------");
        }
    }

    public static class ConfiguracaoMenu
    {
        public static ConfiguracaoUsuario EscolherRegiaoEIdioma()
        {
            string regiao = EscolherOpcao(
                "=== Escolha a Região ===",
                new Dictionary<int, string>
                {
                    { 1, "BR" },
                    { 2, "US" },
                    { 3, "PT" },
                }
            );

            string idioma = EscolherOpcao(
                "=== Escolha o Idioma ===",
                new Dictionary<int, string>
                {
                    { 1, "pt-BR" },
                    { 2, "en-US" },
                    { 3, "pt-PT" },
                }
            );

            return new ConfiguracaoUsuario(regiao, idioma);
        }

        private static string EscolherOpcao(string titulo, Dictionary<int, string> opcoes)
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine(titulo);

                foreach (var kv in opcoes)
                    Console.WriteLine($"{kv.Key} - {kv.Value}");

                Console.Write("Digite a opção: ");
                string? entrada = Console.ReadLine();

                if (!int.TryParse(entrada, out int opcao))
                {
                    Console.WriteLine("Opção inválida: digite um número.");
                    continue;
                }

                if (!opcoes.ContainsKey(opcao))
                {
                    Console.WriteLine("Opção inválida: escolha uma das opções do menu.");
                    continue;
                }

                return opcoes[opcao];
            }
        }
    }

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
        foreach (Livro livro in FabricaLivros.BuscarTodos())
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
    const int OPATIVAR_IDIOMA = 8;
    const int OP_CADASTRAR_CLIENTE = 9;
    const int OP_LISTAR_CLIENTES = 10;
    const int OP_SAIR = 0;

    public static void BuscarLivros()
    {
        List<Livro> livros = FabricaLivros.BuscarTodos();
        foreach (Livro livro in livros)
        {
            Console.WriteLine(livro.ToString());
        }
    }

    static void Main(string[] args)
    {
        DateTime dataAtual = DateTime.Now;
        string dataFormatada = dataAtual.ToString("dd/MM/yyyy HH:mm");

        bool executando = true;

        //Cliente[] clientes = new Cliente[20];
        // int qtdClientes = 0;

        Cliente? clienteLogado = null;

        do
        {
            Console.Clear();

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
                    Console.WriteLine("Buscar Livros - Dados Mock");
                    FabricaLivros
                        .BuscarTodos()
                        .ForEach(livro => Console.WriteLine(livro.ToString()));
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
                case OPATIVAR_IDIOMA:
                    Console.WriteLine("Ativar Idioma e Região");
                    ConfiguracaoUsuario config = ConfiguracaoMenu.EscolherRegiaoEIdioma();
                    Console.WriteLine($"Configuração selecionada: {config}");
                    Console.ReadKey();
                    break;
                case OP_CADASTRAR_CLIENTE:
                    CadastrarCliente();
                    break;
                case OP_LISTAR_CLIENTES:
                    Console.WriteLine("Listar Clientes Cadastrados");
                    ListarClientes();
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
