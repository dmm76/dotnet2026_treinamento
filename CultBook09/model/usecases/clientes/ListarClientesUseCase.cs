using CultBook09.model.entities.clientes;
using CultBook09.model.interfaces;

namespace CultBook09.model.usecases.clientes;

public class ListarClientesUseCase
{
    private readonly IClienteRepositorio repo;

    public ListarClientesUseCase(IClienteRepositorio repo)
    {
        this.repo = repo;
    }

    public List<Cliente> Executar() => repo.BuscarTodos();
}
