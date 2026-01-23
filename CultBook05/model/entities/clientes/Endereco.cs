namespace CultBook05.model.entities.clientes;

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
        var comp = string.IsNullOrWhiteSpace(Complemento) ? "" : $", {Complemento}";
        Console.WriteLine(
            $"Endereço: {Rua}, {Numero}{comp}, {Bairro}, {Cidade} - {Estado}, CEP: {Cep}"
        );
    }

    public override string ToString()
    {
        return $"Rua: {Rua}\n"
            + $"Número: {Numero}\n"
            + $"Complemento: {Complemento}\n"
            + $"Bairro: {Bairro}\n"
            + $"Cidade: {Cidade}\n"
            + $"Estado: {Estado}\n"
            + $"CEP: {Cep}";
    }
}
