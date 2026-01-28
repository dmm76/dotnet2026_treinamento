using CultBook12.model.entities.clientes;

namespace CultBook12.infra.data.factory;

public static class FabricaClientes
{
    public static List<Cliente> CriarMock()
    {
        var c1 = new Cliente("Douglas", "douglas", "123", "douglas@email.com", "44999990000");
        c1.InserirEndereco(
            new Endereco("Av. Brasil", 1000, "Ap 12", "Centro", "Maringá", "PR", "87000-000")
        );

        var c2 = new Cliente("Ana", "ana", "123", "ana@email.com", "44999991111");
        c2.InserirEndereco(
            new Endereco("Rua das Flores", 200, "Casa", "Jardim", "Sarandi", "PR", "87100-000")
        );

        var c3 = new Cliente("Carlos", "carlos", "123", "carlos@email.com", "44999992222");
        c3.InserirEndereco(new Endereco("Rua X", 45, "", "Zona 7", "Maringá", "PR", "87020-000"));

        return new List<Cliente> { c1, c2, c3 };
    }
}
