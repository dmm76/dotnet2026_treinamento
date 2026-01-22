namespace CultBook04.model;

public class Pedido
{
    private string Numero { get; set; }

    private string DataEmissao { get; set; }

    private string FormaPagamento { get; set; }

    private double ValorTotal { get; set; }

    private string Situacao { get; set; }

    //Atualizacao pra projeto Lab04

    private ItemDePedido[] itens;
    private int qtdItens;

    private Endereco endereco;

    public Pedido(
        string numero,
        string dataEmissao,
        string formapagamento,
        double valorTotal,
        string situacao
    )
    {
        Numero = numero;
        DataEmissao = dataEmissao;
        FormaPagamento = formapagamento;
        ValorTotal = valorTotal;
        Situacao = situacao;

        itens = new ItemDePedido[10]; //Tamanho fixo de 10 itens pra o Laboratorio 04
        qtdItens = 0;
    }

    //novos metodo para o lab04
    public void InserirItem(ItemDePedido item)
    {
        //evita overflow do array
        if (qtdItens >= itens.Length)
        {
            Console.WriteLine("Carrinho cheio (limite de 10 itens).");
            return;
        }
        itens[qtdItens] = item;
        qtdItens++;
    }

    public ItemDePedido[] GetItens()
    {
        return itens;
    }

    public void SetEndereco(Endereco endereco)
    {
        this.endereco = endereco;
    }

    public Endereco GetEndereco()
    {
        return endereco;
    }

    public void Mostrar()
    {
        Console.WriteLine(
            $"Número: {Numero} | Data Emissão: {DataEmissao} | Forma Pagamento: {FormaPagamento} | Valor Total: {ValorTotal} | Situação: {Situacao}"
        );

        Console.WriteLine("=== ITENS DO PEDIDO ===");
        for (int i = 0; i < qtdItens; i++)
        {
            //evita erro de referencia nula
            if (itens[i] != null)
                itens[i].Mostrar();
        }
    }
}
