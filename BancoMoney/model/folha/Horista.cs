namespace BancoMoney.model.folha;

public class Horista : Funcionario, IFolha
{
    public double ValorHora { get; set; }
    public int HorasTrabalhadas { get; set; }

    public double CalcularSalario()
    {
        return (double)(ValorHora * HorasTrabalhadas);
    }
}
