using CultBook05.infra.data.factory;
using CultBook05.model.entities.clientes;

namespace CultBook05.model.usecases.clientes;

public class ListarClientesUseCase
{
    public Cliente[] Executar()
    {
        return FabricaClientes.GetClientes();
    }

    public int GetQtd() => FabricaClientes.GetQtd();
}
