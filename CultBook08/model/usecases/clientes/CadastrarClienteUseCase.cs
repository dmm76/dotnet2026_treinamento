using System.Security.Cryptography;
using CultBook08.model.entities.clientes;
using CultBook08.model.interfaces;

namespace CultBook08.model.usecases.clientes;

public class CadastrarClienteUseCase
{
    private readonly IClienteRepositorio _repo;

    public CadastrarClienteUseCase(IClienteRepositorio repo)
    {
        _repo = repo;
    }

    public string? Executar(string nome, string login, string? senha, string? email, string? fone)
    {
        if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(login))
            throw new Exception("Nome e Login são obrigatórios.");

        nome = nome.Trim();
        login = login.Trim();

        if (_repo.LoginExiste(login))
            throw new Exception("Esse login já existe. Escolha outro.");

        // ✅ senha pode ser vazia → gera
        string? senhaGerada = null;
        if (string.IsNullOrWhiteSpace(senha))
        {
            senhaGerada = GerarSenhaForte(8);
            senha = senhaGerada;
        }
        else
        {
            senha = senha.Trim();
        }

        var cliente = new Cliente(nome, login, senha!, (email ?? "").Trim(), (fone ?? "").Trim());

        _repo.Adicionar(cliente);

        return senhaGerada;
    }

    private static string GerarSenhaForte(int tamanho)
    {
        if (tamanho < 8)
            tamanho = 8;

        const string letras = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string digitos = "0123456789";
        const string simbolos = "!@#$%&*()-_=+[]{}<>?";

        // garante pelo menos 1 de cada categoria
        var chars = new List<char>(tamanho)
        {
            EscolherUm(letras),
            EscolherUm(digitos),
            EscolherUm(simbolos),
        };

        // completa o restante com todos
        string todos = letras + digitos + simbolos;
        while (chars.Count < tamanho)
            chars.Add(EscolherUm(todos));

        // embaralha
        Embaralhar(chars);

        return new string(chars.ToArray());
    }

    private static char EscolherUm(string conjunto)
    {
        int idx = RandomNumberGenerator.GetInt32(conjunto.Length);
        return conjunto[idx];
    }

    private static void Embaralhar(List<char> lista)
    {
        for (int i = lista.Count - 1; i > 0; i--)
        {
            int j = RandomNumberGenerator.GetInt32(i + 1);
            (lista[i], lista[j]) = (lista[j], lista[i]);
        }
    }
}
