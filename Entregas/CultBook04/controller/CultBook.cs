namespace CultBook04.controller;

using CultBook04.model;
using CultBook04.testes;

class Program
{
    static void Main(string[] args)
    {
        bool executando = true;

        Cliente[] clientes = new Cliente[20];
        int qtdClientes = 0;

        Cliente clienteLogado = null;

        do
        {
            Console.Clear();
            Console.WriteLine("=== CultBook ===");
            Console.WriteLine("1 - Login");
            Console.WriteLine("2 - Cadastrar");
            Console.WriteLine("3 - Buscar Livros");
            Console.WriteLine("4 - Inserir Livro");
            Console.WriteLine("5 - Remover Livro");
            Console.WriteLine("6 - Ver Carrinho");
            Console.WriteLine("7 - Efetuar Compra");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");

            int opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    Console.WriteLine("Login - Em construção");
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

                case 2:
                    Console.WriteLine("Cadastrar - Em construção");
                    Console.ReadKey();
                    break;

                case 3:
                    Console.WriteLine("Buscar Livros - Dados Mock");
                    FabricaLivros.GetLivros().ForEach(livro => livro.Mostrar());
                    Console.ReadKey();
                    break;

                case 4:
                    Console.WriteLine("Inserir Livro - Em construção");
                    Console.ReadKey();
                    break;

                case 5:
                    Console.WriteLine("Remover Livro - Em construção");
                    Console.ReadKey();
                    break;

                case 6:
                    if (clienteLogado == null || clienteLogado.Logado == false)
                    {
                        Console.WriteLine("Você precisa estar logado para ver o carrinho.");
                        Console.ReadKey();
                        break;
                    }
                    Console.WriteLine("=== CARRINHO ===");
                    // aqui será chamado o pedido do cliente / mostrar pedidos, etc.
                    // por enquanto, só confirmando acesso:
                    Console.WriteLine($"Cliente logado: {clienteLogado.Nome}");
                    Console.ReadKey();
                    break;

                case 7:
                    Console.WriteLine("Efetuar Compra - Em construção");
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
}
