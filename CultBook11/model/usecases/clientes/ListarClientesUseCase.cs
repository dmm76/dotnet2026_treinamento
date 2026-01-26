using CultBook11.model.entities.clientes;
using CultBook11.model.interfaces;

namespace CultBook11.model.usecases.clientes;

public class ListarClientesUseCase
{
    private readonly IClienteRepositorio repo;

    public ListarClientesUseCase(IClienteRepositorio repo)
    {
        this.repo = repo;
    }

    public List<Cliente> Executar() => repo.BuscarTodos();
}
