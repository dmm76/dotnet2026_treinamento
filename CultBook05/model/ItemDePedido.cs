namespace CultBook05.model;

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
        //ajuste para o lab06
        //respeita LivroFisico (preco + frete) e Ebook (preco)
        Preco = livro.CalcularPrecoTotal();
    }

    public void Mostrar()
    {
        Console.WriteLine(
            $"Livro: {Livro.Titulo} | Quantidade: {Quantidade} | Preço Unitário: {Preco} | Subtotal: {Preco * Quantidade}"
        );
    }

    public override string ToString()
    {
        return $"Livro:\n{Livro}\n"
            + $"Quantidade: {Quantidade}\n"
            + $"Preço Unitário: {Preco:F2}\n"
            + $"Subtotal: {Preco * Quantidade:F2}";
    }
}
