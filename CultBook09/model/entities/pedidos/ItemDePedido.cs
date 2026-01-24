namespace CultBook09.model.entities.pedidos;

using System.Text;
using CultBook09.model.entities.livros;

public class ItemDePedido
{
    public int Quantidade { get; set; }
    public decimal Preco { get; set; }

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
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("======ITEM DE PEDIDO======");
        sb.AppendLine(Livro.ToString());
        sb.AppendLine($"Quantidade: {Quantidade}");
        sb.AppendLine($"Preço Unitário: {Preco:F2}");
        sb.AppendLine($"Subtotal: {Preco * Quantidade:F2}");
        sb.AppendLine("------------------------------");
        Console.WriteLine(sb.ToString());
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("======ITEM DE PEDIDO======");
        sb.AppendLine(Livro.ToString());
        sb.AppendLine($"Quantidade: {Quantidade}");
        sb.AppendLine($"Preço Unitário: {Preco:F2}");
        sb.AppendLine($"Subtotal: {Preco * Quantidade:F2}");
        sb.AppendLine("------------------------------");
        return sb.ToString();
    }
}
