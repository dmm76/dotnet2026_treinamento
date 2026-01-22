namespace BancoMoney.model.entidades;

class ContaCorrente
{
    public int Numero { get; set; }
    public string Titular { get; set; }
    public double Saldo { get; set; }
    public string Senha { get; set; }

    public ContaCorrente()
        : this(0, "", "") { }

    public ContaCorrente(int numero, string titular, string senha)
    {
        Numero = numero;
        Titular = titular;
        Senha = senha;
        Saldo = 0.0;
    }

    public virtual void Sacar(double valor)
    {
        if (valor <= Saldo)
        {
            Saldo -= valor;
            Console.WriteLine("Saque realizado com sucesso!");
        }
        else
        {
            Console.WriteLine("Saldo insuficiente.");
        }
    }

    public void Depositar(double valor)
    {
        Saldo += valor;
        Console.WriteLine("Depósito realizado com sucesso!");
    }

    public virtual double ConsultarSaldo()
    {
        return Saldo;
    }

    public bool ValidarSenha(string senhaDigitada)
    {
        Console.WriteLine("Validando senha...");
        return Senha == senhaDigitada;
    }

    public override string ToString()
    {
        return $"ContaCorrente [Número: {Numero}, Titular: {Titular}, Saldo: {Saldo}]";
    }
}
