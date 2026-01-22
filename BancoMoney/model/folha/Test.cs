namespace BancoMoney.model.folha;

public class Test
{
    public static void Main(string[] args)
    {
        IFolha[] funcionarios = new IFolha[3];

        funcionarios[0] = new Mensalista
        {
            Nome = "Ana",
            CPF = "111.222.333-44",
            SalarioMensal = 3000.00,
        };
        funcionarios[1] = new Horista
        {
            Nome = "Bruno",
            CPF = "555.666.777-88",
            ValorHora = 50.00,
            HorasTrabalhadas = 160,
        };
        funcionarios[2] = new Vendedor
        {
            Nome = "Carla",
            CPF = "999.000.111-22",
            SalarioBase = 2000.00,
            Comissao = 1500.00,
        };
        Console.WriteLine("=== Folha de Pagamento ===");

        foreach (var funcionario in funcionarios)
        {
            // Como todos também são Funcionario, dá pra pegar Nome/CPF
            if (funcionario is Funcionario f)
            {
                Console.WriteLine(
                    $"{f.Nome} ({f.CPF}) - {funcionario.GetType().Name} - Salário: R$ {funcionario.CalcularSalario():F2}"
                );
            }
            else
            {
                Console.WriteLine($"(Sem dados) - Salário: R$ {funcionario.CalcularSalario():F2}");
            }
        }
    }
}
