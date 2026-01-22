namespace BancoMoney.model.entidades;

class ContaCorrente
{
    public int Numero { get; set; }
    public string Titular { get; set; }
    public double Saldo { get; private set; }
    private string Senha { get; set; }

    public ContaCorrente(int numero, string titular, string senha)
    {
        Numero = numero;
        Titular = titular;
        Senha = senha;
        Saldo = 0.0;
    }

    public void Sacar(double valor)
    {
        Saldo -= valor;
        Console.WriteLine("Saque realizado com sucesso!");
    }

    public void Depositar(double valor)
    {
        Saldo += valor;
        Console.WriteLine("Depósito realizado com sucesso!");
    }

    public double ConsultarSaldo()
    {
        return Saldo;
    }

    public bool ValidarSenha(string senhaDigitada)
    {
        Console.WriteLine("Validando senha...");
        return Senha == senhaDigitada;
    }
}
