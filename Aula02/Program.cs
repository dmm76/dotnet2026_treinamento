using System;
using System.Linq;
using System.Collections.Generic;

namespace Aula02
{
        class Aluno
    {
        public string Nome {get;} = "";
        public int Idade   {get;}

        public bool Ativo {get;}

        public Aluno(string nome, int idade, bool ativo)
        {
            Nome = nome;
            Idade = idade;
            Ativo = ativo;
        }
    }

    class CadastrarAlunoDto
    {
        public string Nome{get;set;} = "";
        public int Idade{get;set;}
        public bool Ativo{get;set;}
    }

    class ResultadoCadastroAlunoDto
    {
        public bool Sucesso{get;set;}
        public string Mensagem{get;set;} = "";

        public AlunoDto? Aluno{get;set;}
    }


    class AlunoPublicoDto
    {
        public string Nome{get;set;}="";
    }

    class AlunoDetalheDto
    {
        public string Nome {get;set;} = "";
        public int Idade{get;set;}
        public bool Ativo{get;set;}
    }

    class AlunoDto
    {
        public string Nome{get;set;} = "";
        public int Idade{get;set;}
    }

    interface IAlunoRepositorio
    {
        List<Aluno> BuscarTodos();
        void Adicionar(Aluno aluno);
    }

    class AlunoRepositorioFakeA : IAlunoRepositorio
    {
        private List<Aluno> _alunos = new List<Aluno>{
            new Aluno("João", 20, true),
            new Aluno("Maria", 25, true),
            new Aluno("Pedro", 28, false),
        };
        public List<Aluno> BuscarTodos() => _alunos.ToList();
        
        public void Adicionar(Aluno aluno) => _alunos.Add(aluno);
    }

    class AlunoRepositorioFakeB : IAlunoRepositorio
    {
        private readonly List<Aluno> _alunos = new List<Aluno>{
            new Aluno("Ana", 22, true),
            new Aluno("Carlos", 30, false),
            new Aluno("Beatriz", 27, true),
        };
        public List<Aluno> BuscarTodos() => _alunos.ToList();

        public void Adicionar(Aluno aluno) => _alunos.Add(aluno);        
    }

    class ListarAlunosPublicosUseCase
    {
        private readonly IAlunoRepositorio _repositorio;

        public ListarAlunosPublicosUseCase(IAlunoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public List<AlunoPublicoDto> Executar()
        {
            var alunos = _repositorio.BuscarTodos();

            return alunos
                    .Select(a => new AlunoPublicoDto{Nome = a.Nome})
                    .ToList();
        }
    }

    class ListarAlunosAtivosUseCase
    {
        private readonly IAlunoRepositorio _repositorio;

        public ListarAlunosAtivosUseCase(IAlunoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public List<AlunoDto> Executar()
        {
            var alunos = _repositorio.BuscarTodos();

            return alunos
                    .Where(a => a.Ativo)
                    .Select(a => new AlunoDto{Nome = a.Nome, Idade = a.Idade})
                    .ToList();
        }
    }

    class CadastrarAlunoUseCase
    {
        private readonly IAlunoRepositorio _repositorio;

        public CadastrarAlunoUseCase(IAlunoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public ResultadoCadastroAlunoDto Executar(CadastrarAlunoDto dto)
        {
            if(string.IsNullOrWhiteSpace(dto.Nome))
            {
                return new ResultadoCadastroAlunoDto{
                    Sucesso = false,
                    Mensagem = "Nome é obrigatório"
                };
            }

            if(dto.Idade <= 0 || dto.Idade > 130)
            {
                return new ResultadoCadastroAlunoDto{
                    Sucesso = false,
                    Mensagem = "Idade inválida"
                };
            }

            var nome = dto.Nome.Trim();
            var alunosExistentes = _repositorio.BuscarTodos().Any(a => a.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
            if(alunosExistentes)    
            {
                return new ResultadoCadastroAlunoDto{
                    Sucesso = false,
                    Mensagem = "Já existe um aluno com esse nome"
                };
            }

            var aluno = new Aluno(nome, dto.Idade, dto.Ativo);
            _repositorio.Adicionar(aluno);

            return new ResultadoCadastroAlunoDto{
                Sucesso = true,
                Mensagem = "Aluno cadastrado com sucesso",
                Aluno = new AlunoDto{Nome = aluno.Nome, Idade = aluno.Idade}
            };
        }
    }

    class Program
    {
        static void ImprimirAtivos(string titulo, List<AlunoDto> alunos)
        {
            Console.WriteLine("==="+titulo+"===");
            foreach(var aluno in alunos)
            {
                Console.WriteLine($"Nome: {aluno.Nome} - Idade: {aluno.Idade}");
            }
            Console.WriteLine();
        }

        static void ImprimirPublicos(string titulo, List<AlunoPublicoDto> alunos)
        {
            Console.WriteLine("==="+titulo+"===");
            foreach(var aluno in alunos)
            {
                Console.WriteLine($"Nome: {aluno.Nome}");
            }
            Console.WriteLine();
        }   

        static void Main()
        {
            IAlunoRepositorio repositorioA = new AlunoRepositorioFakeA();
            IAlunoRepositorio repositorioB = new AlunoRepositorioFakeB();

            var useCaseA = new ListarAlunosPublicosUseCase(repositorioA);
            var useCaseB = new ListarAlunosPublicosUseCase(repositorioB);

            var alunosPublicosA = useCaseA.Executar();
            var alunosPublicosB = useCaseB.Executar();

            // ImprimirPublicos("Alunos Publicos - Repositorio A", alunosPublicosA);
            // ImprimirPublicos("Alunos Publicos - Repositorio B", alunosPublicosB);


            var cadastrar = new CadastrarAlunoUseCase(repositorioA);

            var resultado = cadastrar.Executar(new CadastrarAlunoDto{
                Nome = "Patricia",
                Idade = 23,
                Ativo = true
            });

            Console.WriteLine("==Cadastro de Aluno==");
            Console.WriteLine($"Sucesso: {resultado.Sucesso}");
            Console.WriteLine($"Mensagem: {resultado.Mensagem}");

            var listarAtivos = new ListarAlunosAtivosUseCase(repositorioA);
            var ativos = listarAtivos.Executar();

            foreach (var a in ativos)
                Console.WriteLine($"{a.Nome} - {a.Idade}");

        }
    }    
}