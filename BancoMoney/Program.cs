using System.Reflection.Metadata;
using BancoMoney.model.entidades;

namespace BancoMoney;

class Program
{
    static void Main(string[] args)
    {
        ContaCorrente conta = new ContaCorrente(5679, "Paula Cristovão", "senha123");
        ContaCorrente conta2 = new ContaEspecial(5678, "Ana Silva", "senha123", 1000.0);
        conta.Depositar(500.0);
        conta.Sacar(200.0);
        conta.Sacar(400.0);
        Console.WriteLine($"Saldo da conta corrente: {conta.ConsultarSaldo()}");
        Console.WriteLine(conta.ToString());

        conta2.Depositar(200.0);
        conta2.Sacar(250.0);
        Console.WriteLine($"Saldo da conta especial: {conta2.ConsultarSaldo()}");

        Console.WriteLine(conta2.ToString());
    }
}
