namespace CultBook01.model;
class ItemDePedido
{
    private int Quantidade {get; set;}
    private double Preco {get; set;}

    public ItemDePedido(int quantidade, double preco)
    {
        Quantidade = quantidade;
        Preco = preco;
    }

    public void Mostrar()
    {
        Console.WriteLine("Qtd: {0} Preço: {1}", Quantidade, Preco);
    }
}
