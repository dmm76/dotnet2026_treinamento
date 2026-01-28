using CultBook12.model.dtos;
using CultBook12.model.entities.livros;
using CultBook12.model.interfaces;
using CultBook12.model.usecases.livros;
using Microsoft.AspNetCore.Mvc;

namespace CultBook12.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LivrosController : ControllerBase
{
    private readonly ILivroRepositorio _repo;

    public LivrosController(ILivroRepositorio repo)
    {
        _repo = repo;
    }

    // GET /api/livros
    [HttpGet]
    public ActionResult<List<LivroDetalheDto>> GetAll()
    {
        var uc = new ListarLivrosUseCase(_repo);
        return Ok(uc.Executar());
    }

    // GET /api/livros/{isbn}
    [HttpGet("{isbn}")]
    public ActionResult<LivroDetalheDto> GetByIsbn(string isbn)
    {
        isbn = (isbn ?? "").Trim();
        if (string.IsNullOrWhiteSpace(isbn))
            return BadRequest(new { title = "ISBN é obrigatório." });

        var livro = _repo.BuscarPorIsbn(isbn);
        if (livro == null)
            return NotFound(new { title = "Livro não encontrado." });

        return Ok(MapearDetalhe(livro));
    }

    // POST /api/livros
    [HttpPost]
    public IActionResult Create([FromBody] LivroDetalheDto dto)
    {
        try
        {
            var uc = new CadastrarLivroUseCase(_repo);
            var entidade = CriarEntidade(dto);

            uc.Executar(entidade);

            return CreatedAtAction(
                nameof(GetByIsbn),
                new { isbn = entidade.Isbn },
                MapearDetalhe(entidade)
            );
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { title = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { title = ex.Message });
        }
        catch (NotSupportedException ex)
        {
            return BadRequest(new { title = ex.Message });
        }
    }

    // PUT /api/livros/{isbn}
    [HttpPut("{isbn}")]
    public IActionResult Update(string isbn, [FromBody] LivroDetalheDto dto)
    {
        try
        {
            isbn = (isbn ?? "").Trim();
            if (string.IsNullOrWhiteSpace(isbn))
                return BadRequest(new { title = "ISBN é obrigatório." });

            // força o ISBN da rota (evita inconsistência)
            dto.Isbn = isbn;

            var uc = new AtualizarLivroUseCase(_repo);
            var entidade = CriarEntidade(dto);

            uc.Executar(entidade);

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { title = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            // pode ser "não encontrado" ou "tipo mudou"
            // se quiser diferenciar depois, a gente faz
            return Conflict(new { title = ex.Message });
        }
        catch (NotSupportedException ex)
        {
            return BadRequest(new { title = ex.Message });
        }
    }

    // DELETE /api/livros/{isbn}
    [HttpDelete("{isbn}")]
    public IActionResult Delete(string isbn)
    {
        try
        {
            var uc = new RemoverLivroUseCase(_repo);
            uc.Executar(isbn);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { title = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { title = ex.Message });
        }
    }

    // -------------------------
    // Helpers (DTO <-> Entidade)
    // -------------------------

    private static Livro CriarEntidade(LivroDetalheDto dto)
    {
        if (dto == null)
            throw new ArgumentException("Body é obrigatório.");

        string tipo = (dto.Tipo ?? "").Trim().ToLowerInvariant();
        string isbn = (dto.Isbn ?? "").Trim();

        // aceita os nomes que você já usa no console + variações comuns
        return tipo switch
        {
            "livro físico" or "livro fisico" or "fisico" => new LivroFisico(
                isbn,
                dto.Titulo,
                dto.Descricao,
                dto.Autor,
                dto.Estoque,
                dto.Preco,
                dto.Figura,
                dto.DataCadastro,
                dto.Categoria,
                peso: dto.Peso ?? 0,
                valorFrete: dto.ValorFrete ?? 0
            ),

            "e-book" or "ebook" => new Ebook(
                isbn,
                dto.Titulo,
                dto.Descricao,
                dto.Autor,
                dto.Estoque,
                dto.Preco,
                dto.Figura,
                dto.DataCadastro,
                dto.Categoria,
                tamanhoMB: dto.TamanhoMB ?? 0
            ),

            "áudiolivro" or "audiolivro" or "audio" => new AudioLivro(
                isbn,
                dto.Titulo,
                dto.Descricao,
                dto.Autor,
                dto.Estoque,
                dto.Preco,
                dto.Figura,
                dto.DataCadastro,
                dto.Categoria,
                tempoDuracao: dto.TempoDuracao ?? 0,
                narrador: dto.Narrador ?? ""
            ),

            _ => throw new NotSupportedException(
                "Tipo inválido. Use: Livro Físico | E-book | ÁudioLivro."
            ),
        };
    }

    private static LivroDetalheDto MapearDetalhe(Livro l)
    {
        var dto = new LivroDetalheDto
        {
            Isbn = l.Isbn,
            Titulo = l.Titulo,
            Descricao = l.Descricao,
            Autor = l.Autor,
            Estoque = l.Estoque,
            Preco = l.Preco,
            Figura = l.Figura,
            DataCadastro = l.DataCadastro,
            Categoria = l.Categoria,
        };

        switch (l)
        {
            case Ebook e:
                dto.Tipo = "E-book";
                dto.TamanhoMB = e.TamanhoMB;
                break;

            case AudioLivro a:
                dto.Tipo = "ÁudioLivro";
                dto.TempoDuracao = a.TempoDuracao;
                dto.Narrador = a.Narrador;
                break;

            case LivroFisico f:
                dto.Tipo = "Livro Físico";
                dto.Peso = f.Peso;
                dto.ValorFrete = f.ValorFrete;
                break;

            default:
                throw new NotSupportedException($"Tipo de livro não mapeado: {l.GetType().Name}");
        }

        return dto;
    }
}
