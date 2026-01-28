using CultBook12.model.usecases.clientes;
using Microsoft.AspNetCore.Mvc;

namespace CultBook12.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly LoginClienteUseCase _login;
    private readonly LogoutClienteUseCase _logout;

    public AuthController(LoginClienteUseCase login, LogoutClienteUseCase logout)
    {
        _login = login;
        _logout = logout;
    }

    public record LoginRequest(string Login, string Senha);

    public record LogoutRequest(string Login);

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest dto)
    {
        try
        {
            _login.Executar(dto.Login, dto.Senha);
            return Ok(new { message = "Login realizado com sucesso" });
        }
        catch (Exception e)
        {
            // do jeito que seu usecase está (exceptions genéricas), vamos mapear assim:
            if (e.Message.Contains("obrigatórios", StringComparison.OrdinalIgnoreCase))
                return BadRequest(new { title = e.Message, status = 400 });

            if (
                e.Message.Contains("Senha incorreta", StringComparison.OrdinalIgnoreCase)
                || e.Message.Contains("Login não encontrado", StringComparison.OrdinalIgnoreCase)
            )
                return Unauthorized(new { title = e.Message, status = 401 });

            if (e.Message.Contains("já está logado", StringComparison.OrdinalIgnoreCase))
                return Conflict(new { title = e.Message, status = 409 });

            return BadRequest(new { title = e.Message, status = 400 });
        }
    }

    [HttpPost("logout")]
    public IActionResult Logout([FromBody] LogoutRequest dto)
    {
        try
        {
            _logout.Executar(dto.Login);
            return Ok(new { message = "Logout realizado com sucesso" });
        }
        catch (Exception e)
        {
            if (e.Message.Contains("obrigatório", StringComparison.OrdinalIgnoreCase))
                return BadRequest(new { title = e.Message, status = 400 });

            if (e.Message.Contains("Login não encontrado", StringComparison.OrdinalIgnoreCase))
                return NotFound(new { title = e.Message, status = 404 });

            if (e.Message.Contains("já está deslogado", StringComparison.OrdinalIgnoreCase))
                return Conflict(new { title = e.Message, status = 409 });

            return BadRequest(new { title = e.Message, status = 400 });
        }
    }
}
