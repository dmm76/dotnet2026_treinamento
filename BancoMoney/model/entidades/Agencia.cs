namespace BancoMoney.model.entidades;

class Agencia
{
    ContaCorrente[] contas = new ContaCorrente[100];

    public void AbrirConta(ContaCorrente conta)
    {
        for (int i = 0; i < contas.Length; i++)
        {
            if (contas[i] == null)
            {
                contas[i] = conta;
                Console.WriteLine("Conta aberta com sucesso!");
                return;
            }
        }
        Console.WriteLine("Não há mais espaço para abrir novas contas.");
    }

    // public void AbrirContaCorrente(ContaCorrente conta)
    // {
    //     for (int i = 0; i < contas.Length; i++)
    //     {
    //         if (contas[i] == null)
    //         {
    //             contas[i] = conta;
    //             Console.WriteLine("Conta aberta com sucesso!");
    //             return;
    //         }
    //     }
    //     Console.WriteLine("Não há mais espaço para abrir novas contas.");
    // }

    // public void abrirContaEspecial(ContaEspecial conta)
    // {
    //     for (int i = 0; i < contas.Length; i++)
    //     {
    //         if (contas[i] == null)
    //         {
    //             contas[i] = conta;
    //             Console.WriteLine("Conta especial aberta com sucesso!");
    //             return;
    //         }
    //     }
    //     Console.WriteLine("Não há mais espaço para abrir novas contas.");
    // }

    public double CalcularTotalGeral()
    {
        double total = 0.0;
        foreach (var conta in contas)
        {
            if (conta != null)
            {
                total += conta.ConsultarSaldo();
            }
        }
        return total;
    }
}
