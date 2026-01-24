namespace CultBook09.controller.menus;

using System;
using CultBook09.model.entities.clientes;
using CultBook09.model.interfaces;
using CultBook09.model.usecases.clientes;

public static class ClienteMenu
{
    public static Cliente? OpcaoLogin(
        Cliente? clienteLogado,
        IClienteRepositorio clienteRepo,
        LoginClienteUseCase loginUc
    )
    {
        try
        {
            if (clienteLogado != null && clienteLogado.Logado)
            {
                Console.WriteLine("Já existe um cliente logado. Faça logout antes.");
                return clienteLogado;
            }

            Console.WriteLine("\n=== LOGIN ===");

            Console.Write("Login: ");
            var login = (Console.ReadLine() ?? "").Trim();

            Console.Write("Senha: ");
            var senha = Console.ReadLine() ?? "";

            loginUc.Executar(login, senha);

            var cliente = clienteRepo.BuscarPorLogin(login);

            Console.WriteLine("Login realizado com sucesso!");
            return cliente;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return clienteLogado;
        }
        finally
        {
            Console.ReadKey();
        }
    }

    public static Cliente? OpcaoLogout(
        Cliente? clienteLogado,
        LogoutClienteUseCase _logoutClienteUc
    )
    {
        try
        {
            if (clienteLogado == null || !clienteLogado.Logado)
            {
                Console.WriteLine("Nenhum cliente logado.");
                return clienteLogado;
            }

            _logoutClienteUc.Executar(clienteLogado.Login ?? "");

            Console.WriteLine("Logout realizado com sucesso!");
            return null; // zera o clienteLogado
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return clienteLogado;
        }
        finally
        {
            Console.ReadKey();
        }
    }

    public static void OpcaoCadastrarCliente(CadastrarClienteUseCase cadastrarUc)
    {
        try
        {
            Console.WriteLine("\n=== CADASTRO DE CLIENTE ===");

            Console.Write("Nome: ");
            string nome = Console.ReadLine() ?? "";

            Console.Write("Login: ");
            string login = Console.ReadLine() ?? "";

            Console.Write("Senha: ");
            string senha = Console.ReadLine() ?? "";

            Console.Write("Email: ");
            string email = Console.ReadLine() ?? "";

            Console.Write("Fone: ");
            string fone = Console.ReadLine() ?? "";

            string? senhaGerada = cadastrarUc.Executar(nome, login, senha, email, fone);

            Console.WriteLine("Cliente cadastrado com sucesso!");

            if (senhaGerada != null)
            {
                Console.WriteLine();
                Console.WriteLine("Senha não informada. Uma senha aleatória foi gerada:");
                Console.WriteLine($"Senha: {senhaGerada}");
                Console.WriteLine("Anote essa senha, pois ela não será mostrada novamente.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public static void OpcaoListarClientes(ListarClientesUseCase listarUc)
    {
        Console.WriteLine("\n=== CLIENTES CADASTRADOS ===");

        var clientes = listarUc.Executar();

        if (clientes.Count == 0)
        {
            Console.WriteLine("Nenhum cliente cadastrado.");
            return;
        }

        foreach (var c in clientes)
        {
            c.Mostrar();
            Console.WriteLine("------------------------");
        }
    }
}
