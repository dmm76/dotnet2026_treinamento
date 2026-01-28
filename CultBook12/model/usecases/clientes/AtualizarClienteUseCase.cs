using CultBook12.model.interfaces;

namespace CultBook12.model.usecases.clientes;

public class AtualizarClienteUseCase
{
    private readonly IClienteRepositorio _repo;

    public AtualizarClienteUseCase(IClienteRepositorio repo)
    {
        _repo = repo;
    }

    public void Executar(string login, string nome, string? email, string? fone)
    {
        if (string.IsNullOrWhiteSpace(login))
            throw new Exception("Login é obrigatório.");

        if (string.IsNullOrWhiteSpace(nome))
            throw new Exception("Nome é obrigatório.");

        var cliente = _repo.BuscarPorLogin(login.Trim());
        if (cliente == null)
            throw new Exception("Cliente não encontrado.");

        // Atualiza só campos permitidos
        cliente.Nome = nome.Trim();
        cliente.Email = (email ?? "").Trim();
        cliente.Fone = (fone ?? "").Trim();

        _repo.Atualizar(cliente);
    }
}
