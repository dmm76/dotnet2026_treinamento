namespace CultBook07;

using CultBook07.model;

public class Program
{
    public static void Main(string[] args)
    {
        Cliente cliente = new Cliente(
            nome: "Douglas",
            login: "douglas",
            email: "douglas@cultbook.com",
            senha: "123"
        );

        ServicoAutenticacao auth = new ServicoAutenticacao();

        Console.WriteLine("Tentando login com senha correta:");
        auth.RealizarLogin(cliente, "123");

        Console.WriteLine("\nTentando login com senha incorreta:");
        auth.RealizarLogin(cliente, "999");
    }
}
