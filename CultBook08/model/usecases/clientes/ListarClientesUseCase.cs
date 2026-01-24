using CultBook08.model.entities.clientes;
using CultBook08.model.interfaces;

namespace CultBook08.model.usecases.clientes;

public class ListarClientesUseCase
{
    private readonly IClienteRepositorio repo;

    public ListarClientesUseCase(IClienteRepositorio repo)
    {
        this.repo = repo;
    }

    public List<Cliente> Executar() => repo.BuscarTodos();
}
