using CultBook12.model.entities.clientes;
using CultBook12.model.interfaces;

namespace CultBook12.model.usecases.clientes;

public class ListarClientesUseCase
{
    private readonly IClienteRepositorio repo;

    public ListarClientesUseCase(IClienteRepositorio repo)
    {
        this.repo = repo;
    }

    public List<Cliente> Executar() => repo.BuscarTodos();
}
