namespace BancoMoney.model.folha;

public class Mensalista : Funcionario, IFolha
{
    public double SalarioMensal { get; set; }

    public double CalcularSalario()
    {
        return (double)SalarioMensal;
    }
}
