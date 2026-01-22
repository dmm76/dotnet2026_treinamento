namespace BancoMoney.model.folha;

public class Vendedor : Funcionario, IFolha
{
    public double SalarioBase { get; set; }
    public double Comissao { get; set; }

    public double CalcularSalario()
    {
        return (double)(SalarioBase + Comissao);
    }
}
