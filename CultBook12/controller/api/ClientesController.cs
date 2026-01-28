using CultBook12.model.dtos;
using CultBook12.model.usecases.clientes;
using Microsoft.AspNetCore.Mvc;

namespace CultBook12.Controllers;

[ApiController]
[Route("api/clientes")]
public class ClientesController : ControllerBase
{
    private readonly ListarClientesUseCase _listar;
    private readonly CadastrarClienteUseCase _cadastrar;

    private readonly AtualizarClienteUseCase _atualizar;

    private readonly RemoverClienteUseCase _remover;

    public ClientesController(
        ListarClientesUseCase listar,
        CadastrarClienteUseCase cadastrar,
        AtualizarClienteUseCase atualizar,
        RemoverClienteUseCase remover
    )
    {
        _listar = listar;
        _cadastrar = cadastrar;
        _atualizar = atualizar;
        _remover = remover;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var clientes = _listar
            .Executar()
            .Select(c => new
            {
                c.Nome,
                c.Login,
                c.Email,
                c.Fone,
                c.Logado,
            })
            .ToList();

        return Ok(clientes);
    }

    [HttpPost]
    public IActionResult Create([FromBody] ClienteCreateDto dto)
    {
        try
        {
            var senhaGerada = _cadastrar.Executar(
                dto.Nome,
                dto.Login,
                dto.Senha,
                dto.Email,
                dto.Fone
            );

            // não retorna senha digitada; só retorna senha gerada (se houver)
            return Created(
                "",
                new
                {
                    message = "Cliente cadastrado com sucesso",
                    cliente = new
                    {
                        nome = dto.Nome.Trim(),
                        login = dto.Login.Trim(),
                        email = (dto.Email ?? "").Trim(),
                        fone = (dto.Fone ?? "").Trim(),
                    },
                    senhaGerada = senhaGerada, // null se o usuário informou senha
                    senhaTemporaria = senhaGerada != null,
                }
            );
        }
        catch (Exception e)
        {
            return BadRequest(new { title = e.Message, status = 400 });
        }
    }

    [HttpPut("{login}")]
    public IActionResult Update(string login, [FromBody] ClienteUpdateDto dto)
    {
        try
        {
            _atualizar.Executar(login, dto.Nome, dto.Email, dto.Fone);

            return Ok(
                new
                {
                    message = "Cliente atualizado com sucesso",
                    login = login.Trim(),
                    cliente = new
                    {
                        nome = dto.Nome.Trim(),
                        email = (dto.Email ?? "").Trim(),
                        fone = (dto.Fone ?? "").Trim(),
                    },
                }
            );
        }
        catch (Exception e)
        {
            // Mantendo simples: tudo como 400 (igual seus usecases atuais)
            return BadRequest(new { title = e.Message, status = 400 });
        }
    }

    [HttpDelete("{login}")]
    public IActionResult Delete(string login)
    {
        try
        {
            _remover.Executar(login);
            return Ok(new { message = "Cliente removido com sucesso", login = login.Trim() });
        }
        catch (Exception e)
        {
            return BadRequest(new { title = e.Message, status = 400 });
        }
    }
}
