namespace CultBook04.model;

public class ItemDePedido
{
    public int Quantidade { get; set; }
    public double Preco { get; set; }

    //ajustes para o lab04

    public Livro Livro { get; }

    public ItemDePedido(Livro livro, int quantidade)
    {
        Livro = livro;
        Quantidade = quantidade;
        Preco = livro.Preco;
    }

    public void Mostrar()
    {
        Console.WriteLine(
            $"Livro: {Livro.Titulo} | Quantidade: {Quantidade} | Preço Unitário: {Preco}"
        );
    }
}
