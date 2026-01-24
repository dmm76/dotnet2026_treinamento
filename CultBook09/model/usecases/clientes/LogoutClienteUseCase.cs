using CultBook09.model.interfaces;

namespace CultBook09.model.usecases.clientes;

public class LogoutClienteUseCase
{
    private readonly IClienteRepositorio repo;

    public LogoutClienteUseCase(IClienteRepositorio repo)
    {
        this.repo = repo;
    }

    public void Executar(string login)
    {
        if (string.IsNullOrWhiteSpace(login))
            throw new Exception("Login é obrigatório.");

        var cliente = repo.BuscarPorLogin(login.Trim());
        if (cliente == null)
            throw new Exception("Login não encontrado.");

        if (!cliente.Logado)
            throw new Exception("Cliente já está deslogado.");

        cliente.Logado = false;
        repo.Atualizar(cliente);
    }
}
