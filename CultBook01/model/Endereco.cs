namespace CultBook01.model;
class Endereco
{
    private string Rua {get; set;}
    private int Numero {get; set;}
    private string Complemento {get; set;}
    private string Bairro {get; set;}
    private string Cidade {get; set;}
    private string Estado {get; set;}
    private string Cep {get; set;}

    public Endereco(string rua, int numero, string complemento, string bairro, string cidade, string estado, string cep)
    {
        Rua = rua;
        Numero = numero;
        Complemento =complemento;
        Bairro = bairro;
        Cidade = cidade;
        Estado= estado;
        Cep = cep;
    }

    public void Mostrar()
    {
        Console.WriteLine(Rua, Numero, Complemento, Bairro, Cidade, Estado, Cep);
    }
  }
