namespace CultBook01.controller;
using CultBook01.model;

class Program
{
    static void Main(string[] args)
    {
        bool executando = true;

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
                    Cliente cliente = new Cliente("Douglas");
                    cliente.VerificarLogin();
                    Console.ReadKey();
                    break;

                case 2:
                    Console.WriteLine("Cadastrar - Em construção");
                    Console.ReadKey();
                    break;

                case 3:
                    Console.WriteLine("Buscar Livros - Em construção");
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
                    Console.WriteLine("Ver Carrinho - Em construção");
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
