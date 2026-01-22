namespace CultBook01.model;
class Pedido
{
    private string Numero {get; set;}

    private string DataEmissao {get; set;}

    private string FormaPagamento {get; set;}

    private double ValorTotal {get; set;}

    private string Situacao {get; set;}

    public Pedido(string numero, string dataEmissao, string formapagamento, double valorTotal, string situacao)
    {
        Numero = numero;
        DataEmissao = dataEmissao;
        FormaPagamento =formapagamento;
        ValorTotal = valorTotal;
        Situacao = situacao;
    }

    public void Mostrar()
    {
        Console.WriteLine(Numero, DataEmissao, FormaPagamento, ValorTotal, Situacao);
    }
}