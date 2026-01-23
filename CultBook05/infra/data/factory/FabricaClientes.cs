using CultBook05.model.entities;

namespace CultBook05.infra.data.factory;

public static class FabricaClientes
{
    private static Cliente[] clientes = new Cliente[50];
    private static int qtd = 0;

    public static bool Inserir(Cliente c)
    {
        if (qtd >= clientes.Length)
            return false;
        clientes[qtd] = c;
        qtd++;
        return true;
    }

    public static Cliente[] GetClientes() => clientes;

    public static int GetQtd() => qtd;

    public static bool LoginExiste(string login)
    {
        for (int i = 0; i < qtd; i++)
        {
            if (
                clientes[i] != null
                && clientes[i].Login != null
                && clientes[i].Login?.Equals(login, StringComparison.OrdinalIgnoreCase) == true
            )
                return true;
        }
        return false;
    }
}
