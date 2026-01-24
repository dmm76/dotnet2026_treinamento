namespace CultBook09.model.entities.pedidos;

using System.Text;
using CultBook09.model.entities.clientes;

public class Pedido
{
    private string Numero { get; set; }

    private string DataEmissao { get; set; }

    private string FormaPagamento { get; set; }

    private decimal ValorTotal { get; set; }

    private string Situacao { get; set; }

    //Atualizacao pra projeto Lab04
    public Cliente? Cliente { get; set; }
    public Endereco? EnderecoEntrega { get; set; }

    public ItemDePedido[] Itens { get; private set; }
    private int _qtdItens;

    public Pedido(
        string numero,
        string dataEmissao,
        string formapagamento,
        string situacao,
        ItemDePedido item
    )
    {
        Numero = numero;
        DataEmissao = dataEmissao;
        FormaPagamento = formapagamento;
        Situacao = situacao;

        Itens = new ItemDePedido[10]; //limite de 10 itens por pedido
        ValorTotal = 0;
        _qtdItens = 0;

        if (item == null)
            throw new ArgumentNullException(nameof(item), "Pedido precisa de pelo menos 1 item.");
        InserirItem(item);
    }

    //novos metodo para o lab04
    public bool InserirItem(ItemDePedido item)
    {
        //evita overflow do array
        if (_qtdItens >= Itens.Length)
        {
            Console.WriteLine("Carrinho cheio (limite de 10 itens).");
            return false;
        }
        Itens[_qtdItens] = item;
        _qtdItens++;

        ValorTotal += item.Preco * item.Quantidade;
        return true;
    }

    public ItemDePedido[] GetItens() => Itens;

    public int GetQtdItens() => _qtdItens;

    public decimal GetValorTotal() => ValorTotal;

    public void RecalcularTotal()
    {
        decimal total = 0;

        for (int i = 0; i < _qtdItens; i++)
        {
            if (Itens[i] != null)
                total += Itens[i].Preco * Itens[i].Quantidade;
        }

        ValorTotal = total;
    }

    public bool SomarQuantidadePorIsbn(string isbn, int quantidade)
    {
        if (string.IsNullOrWhiteSpace(isbn) || quantidade <= 0)
            return false;

        isbn = isbn.Trim();

        for (int i = 0; i < _qtdItens; i++)
        {
            if (Itens[i] != null && Itens[i].Livro.Isbn == isbn)
            {
                Itens[i].Quantidade += quantidade;
                RecalcularTotal();
                return true;
            }
        }
        return false;
    }

    //Remover item do pedido pelo ISBN do livro
    // public bool RemoverItemPorIsbn(string isbn)
    // {
    //     if (string.IsNullOrWhiteSpace(isbn))
    //         return false;

    //     isbn = isbn.Trim();

    //     int idx = -1;

    //     for (int i = 0; i < _qtdItens; i++)
    //     {
    //         if (Itens[i] != null && Itens[i].Livro.Isbn == isbn)
    //         {
    //             idx = i;
    //             break;
    //         }
    //     }

    //     if (idx == -1)
    //         return false;

    //     // "Puxa" os itens para não deixar buraco
    //     for (int i = idx; i < _qtdItens - 1; i++)
    //     {
    //         Itens[i] = Itens[i + 1];
    //     }

    //     // limpa a última posição e ajusta contador
    //     Itens[_qtdItens - 1] = null;
    //     _qtdItens--;

    //     RecalcularTotal();
    //     return true;
    // }

    public void Mostrar()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Número: {Numero}");
        sb.AppendLine($"Data Emissão: {DataEmissao}");
        sb.AppendLine($"Forma Pagamento: {FormaPagamento}");
        sb.AppendLine($"Valor Total: {ValorTotal}");
        sb.AppendLine($"Situação: {Situacao}");
        sb.AppendLine("Endereço:");
        sb.AppendLine(EnderecoEntrega != null ? EnderecoEntrega.ToString() : "Sem endereço");
        sb.AppendLine("Itens:");
        for (int i = 0; i < _qtdItens; i++)
        {
            if (Itens[i] != null)
            {
                sb.AppendLine($"--- Item {i + 1} ---");
                sb.AppendLine(Itens[i].ToString());
            }
        }
        Console.WriteLine(sb.ToString());
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Número: {Numero}");
        sb.AppendLine($"Data Emissão: {DataEmissao}");
        sb.AppendLine($"Forma Pagamento: {FormaPagamento}");
        sb.AppendLine($"Valor Total: {ValorTotal:F2}");
        sb.AppendLine($"Situação: {Situacao}");
        sb.AppendLine("Endereço:");
        sb.AppendLine(EnderecoEntrega != null ? EnderecoEntrega.ToString() : "Sem endereço");
        sb.AppendLine("Itens:");
        for (int i = 0; i < _qtdItens; i++)
        {
            if (Itens[i] != null)
            {
                sb.AppendLine($"--- Item {i + 1} ---");
                sb.AppendLine(Itens[i].ToString());
            }
        }
        return sb.ToString();
    }
}
