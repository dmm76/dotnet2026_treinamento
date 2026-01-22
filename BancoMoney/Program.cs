using System.Reflection.Metadata;
using BancoMoney.model.entidades;

namespace BancoMoney;

class Program
{
    static void Main(string[] args)
    {
        ContaCorrente conta = new ContaCorrente();
        ContaCorrente conta2 = new ContaCorrente();
        conta2.Depositar(200.0);

        conta.Numero = 1234;
        conta.Titular = "Lindolfo Pires";
        conta.Senha = "minhaSenhaSegura";

        conta.Depositar(500.0);
        conta.Sacar(200.0);
        conta.Sacar(70.0);
        Console.WriteLine("Saldo inicial: " + conta.ConsultarSaldo());
    }
}
