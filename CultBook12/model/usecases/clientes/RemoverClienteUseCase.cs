using CultBook12.model.interfaces;

namespace CultBook12.model.usecases.clientes;

public class RemoverClienteUseCase
{
    private readonly IClienteRepositorio _repo;

    public RemoverClienteUseCase(IClienteRepositorio repo)
    {
        _repo = repo;
    }

    public void Executar(string login)
    {
        if (string.IsNullOrWhiteSpace(login))
            throw new Exception("Login é obrigatório.");

        var cliente = _repo.BuscarPorLogin(login.Trim());

        if (cliente == null)
            throw new Exception("Cliente não encontrado");

        _repo.RemoverPorLogin(login.Trim());
    }
}
