namespace CultBook09.model.entities.clientes;

using System.Text;

public class Endereco
{
    public string Rua { get; set; }
    public int Numero { get; set; }
    public string Complemento { get; set; }
    public string Bairro { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
    public string Cep { get; set; }

    public Endereco(
        string rua,
        int numero,
        string complemento,
        string bairro,
        string cidade,
        string estado,
        string cep
    )
    {
        Rua = rua;
        Numero = numero;
        Complemento = complemento;
        Bairro = bairro;
        Cidade = cidade;
        Estado = estado;
        Cep = cep;
    }

    public void Mostrar()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("======ENDEREÇO======");
        sb.AppendLine($"Rua: {Rua}");
        sb.AppendLine($"Número: {Numero}");
        sb.AppendLine($"Complemento: {Complemento}");
        sb.AppendLine($"Bairro: {Bairro}");
        sb.AppendLine($"Cidade: {Cidade}");
        sb.AppendLine($"Estado: {Estado}");
        sb.AppendLine($"CEP: {Cep}");
        sb.AppendLine("------------------------------");
        Console.WriteLine(sb.ToString());
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Rua: {Rua}");
        sb.AppendLine($"Número: {Numero}");
        sb.AppendLine($"Complemento: {Complemento}");
        sb.AppendLine($"Bairro: {Bairro}");
        sb.AppendLine($"Cidade: {Cidade}");
        sb.AppendLine($"Estado: {Estado}");
        sb.AppendLine($"CEP: {Cep}");
        return sb.ToString();
    }
}
