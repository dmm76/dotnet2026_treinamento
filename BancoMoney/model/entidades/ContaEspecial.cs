namespace BancoMoney.model.entidades;

class ContaEspecial : ContaCorrente
{
    public double Limite { get; set; }

    public ContaEspecial()
        : this(0, "", "", 0.0) { }

    public ContaEspecial(int numero, string titular, string senha, double limite)
        : base(numero, titular, senha)
    {
        Limite = limite;
    }

    public override void Sacar(double valor)
    {
        if (valor <= Saldo + Limite)
        {
            Saldo -= valor;
            Console.WriteLine("Saque realizado com sucesso!");
        }
        else
        {
            Console.WriteLine("Saldo insuficiente, mesmo com o limite especial.");
        }
    }

    public override string ToString()
    {
        return $"ContaEspecial [Número: {Numero}, Titular: {Titular}, Saldo: {Saldo}, Limite: {Limite}]";
    }
}
