namespace CultBook12.WebApi.Dtos;

public class ItemPedidoRequest
{
    public string Isbn { get; set; } = "";
    public int Quantidade { get; set; }
}

public class FinalizarPedidoRequest
{
    public string FormaPagamento { get; set; } = "Não informado";
}

public class PedidoItemResponseDto
{
    public string Isbn { get; set; } = "";
    public string Titulo { get; set; } = "";
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
    public decimal Subtotal { get; set; }
}

public class PedidoResponseDto
{
    // Como Numero/DataEmissao/Situacao são privados no seu Pedido,
    // aqui a API devolve só o que dá pra ler com segurança sem "quebrar" sua entidade.
    public decimal Total { get; set; }
    public int QtdItens { get; set; }
    public List<PedidoItemResponseDto> Itens { get; set; } = new();
}
