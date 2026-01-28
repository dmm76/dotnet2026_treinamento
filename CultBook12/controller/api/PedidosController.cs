using CultBook12.model.entities.clientes;
using CultBook12.model.entities.pedidos;
using CultBook12.model.interfaces;
using CultBook12.model.usecases.livros;
using CultBook12.WebApi.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CultBook12.controller.api;

[ApiController]
[Route("api/clientes/{clienteId}/carrinho")]
public class PedidosController : ControllerBase
{
    private readonly IPedidoRepositorio _pedidoRepo;
    private readonly ILivroRepositorio _livroRepo;

    public PedidosController(IPedidoRepositorio pedidoRepo, ILivroRepositorio livroRepo)
    {
        _pedidoRepo = pedidoRepo;
        _livroRepo = livroRepo;
    }

    // GET /api/clientes/{clienteId}/carrinho
    [HttpGet]
    public ActionResult<PedidoResponseDto> GetCarrinho(string clienteId)
    {
        var pedido = _pedidoRepo.ObterAberto(clienteId);
        if (pedido == null)
            return NotFound(new { title = "Carrinho vazio (nenhum pedido em aberto)." });

        return Ok(MapearPedido(pedido));
    }

    // POST /api/clientes/{clienteId}/carrinho/itens
    [HttpPost("itens")]
    public ActionResult<PedidoResponseDto> InserirItem(
        string clienteId,
        [FromBody] ItemPedidoRequest req
    )
    {
        try
        {
            if (req == null)
                return BadRequest(new { title = "Body é obrigatório." });

            var clienteLogadoFake = CriarClienteLogado(clienteId);
            var pedidoAtual = _pedidoRepo.ObterAberto(clienteId);

            var uc = new InserirLivroCarrinhoUseCase(_livroRepo);

            var pedidoNovo = uc.Executar(clienteLogadoFake, pedidoAtual, req.Isbn, req.Quantidade);

            _pedidoRepo.SalvarAberto(clienteId, pedidoNovo);

            return Ok(MapearPedido(pedidoNovo));
        }
        catch (Exception ex)
        {
            return BadRequest(new { title = ex.Message });
        }
    }

    // DELETE /api/clientes/{clienteId}/carrinho/itens/{isbn}?quantidade=1
    [HttpDelete("itens/{isbn}")]
    public ActionResult RemoverItem(string clienteId, string isbn, [FromQuery] int quantidade = 1)
    {
        try
        {
            var pedido = _pedidoRepo.ObterAberto(clienteId);
            if (pedido == null)
                return NotFound(new { title = "Carrinho vazio (nenhum pedido em aberto)." });

            if (quantidade <= 0)
                quantidade = 1;

            for (int i = 0; i < quantidade; i++)
            {
                bool removeu = pedido.RemoverPorIsbn(isbn);
                if (!removeu)
                    return NotFound(new { title = "Livro não encontrado no carrinho." });
            }

            // se zerou, limpa o carrinho
            if (pedido.GetQtdItens() == 0)
            {
                _pedidoRepo.LimparAberto(clienteId);
                return Ok(new { message = "Carrinho ficou vazio e foi limpo." });
            }

            _pedidoRepo.SalvarAberto(clienteId, pedido);
            return Ok(MapearPedido(pedido));
        }
        catch (Exception ex)
        {
            return BadRequest(new { title = ex.Message });
        }
    }

    // POST /api/clientes/{clienteId}/carrinho/finalizar
    [HttpPost("finalizar")]
    public ActionResult<object> Finalizar(string clienteId, [FromBody] FinalizarPedidoRequest req)
    {
        try
        {
            var pedido = _pedidoRepo.ObterAberto(clienteId);
            if (pedido == null)
                return NotFound(new { title = "Carrinho vazio (nenhum pedido em aberto)." });

            string forma = (req?.FormaPagamento ?? "Não informado").Trim();
            pedido.FinalizarCompra(forma);

            _pedidoRepo.LimparAberto(clienteId);

            return Ok(
                new { message = "Compra finalizada com sucesso!", total = pedido.GetValorTotal() }
            );
        }
        catch (Exception ex)
        {
            return BadRequest(new { title = ex.Message });
        }
    }

    // -----------------------
    // Helpers
    // -----------------------

    private static Cliente CriarClienteLogado(string clienteId)
    {
        var id = (clienteId ?? "").Trim();
        if (string.IsNullOrWhiteSpace(id))
            throw new Exception("clienteId inválido.");

        return new Cliente(
            nome: id,
            login: id,
            senha: "api",
            email: id + "@local",
            fone: "0000-0000",
            logado: true
        );
    }

    private static PedidoResponseDto MapearPedido(Pedido p)
    {
        if (p == null)
        {
            return new PedidoResponseDto
            {
                Total = 0m,
                QtdItens = 0,
                Itens = new List<PedidoItemResponseDto>(),
            };
        }

        var dto = new PedidoResponseDto
        {
            Total = p.GetValorTotal(),
            QtdItens = p.GetQtdItens(),
            Itens = new List<PedidoItemResponseDto>(),
        };

        var itens = p.GetItens();

        for (int i = 0; i < itens.Count; i++)
        {
            var it = itens[i];

            dto.Itens.Add(
                new PedidoItemResponseDto
                {
                    Isbn = it.Livro.Isbn,
                    Titulo = it.Livro.Titulo,
                    Quantidade = it.Quantidade,
                    PrecoUnitario = it.Preco,
                    Subtotal = it.Preco * it.Quantidade,
                }
            );
        }

        return dto;
    }
}
