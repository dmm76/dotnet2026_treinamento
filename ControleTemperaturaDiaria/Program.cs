using System.Net.Http.Headers;

namespace ControleTemperaturaDiaria;

class Program
{
    public static double[] LeituraDeTemperaturas(int qtd)
    {
        int x = 0;        
        double[] temperaturas = new double[qtd];
        while (x!=qtd)
        {
            Console.Write($"Digite a temperatura do dia {x + 1}: ");
            temperaturas[x] = Convert.ToDouble(Console.ReadLine());
            x++;
        }       
        return temperaturas;
    }

    public static double CalculaMedia(double[] temperaturas)
    {          
        double soma =0;
        double media = 0;
        foreach(var t in temperaturas)
        {
           soma += t;
           media = soma /temperaturas.Length;
        }
        return media;
    }

    public static void ImprimirRelatorio()
    {
        Console.WriteLine("*** Sistema de Leitura, Média e Relatório de Tempetaturas ***");
        Console.Write("Informe a quantidade de dias que deseja registrar: ");
        int qtdDias = Convert.ToInt32(Console.ReadLine());
        double [] temp = LeituraDeTemperaturas(qtdDias);
        double media = CalculaMedia(temp);
        
        int qtdAcimaOuIgual = 0;
        foreach(var t in temp)
        {
            if(t >= media)
            {
                qtdAcimaOuIgual++;
            }
        }
        Console.WriteLine("\n=== Relatório Informativo ===");
        Console.WriteLine($"Temperatura média: {media:F2}");
        Console.WriteLine($"Dias acima ou igual à média: {qtdAcimaOuIgual} dia(s)");
    }    
    static void Main(string[] args)
    {           
        ImprimirRelatorio();
    }
}