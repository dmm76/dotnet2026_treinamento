namespace HelloWorld;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Digite seu nome:");
        string nome = Console.ReadLine();
        Console.WriteLine("Digite seu CPF:");
        string cpf = Console.ReadLine();
        Console.WriteLine("Digite seu telefone:");
        string telefone = Console.ReadLine();
        Console.WriteLine("Digite seu ano de nascimento:");
        int anoNascimento = Convert.ToInt32(Console.ReadLine());


        Console.WriteLine("===Usuario Cadastrado com Sucesso!===");
        Console.WriteLine($"Nome: {nome}, CPF: {cpf}, Telelfone: {telefone}, Ano de Nascimento: {anoNascimento}");
    }
}
