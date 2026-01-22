using System;
using System.Collections.Generic;
using System.Linq;

namespace AulaVeiculos
{
   // 1) DOMÍNIO
    class Veiculo
    {
        public string Placa { get; }
        public int Km { get;}
        public bool Ativo { get; }

        public Veiculo(string placa, int km, bool ativo)
        {
            Placa = placa;
            Km = km;
            Ativo = ativo;
        }
    }
    // 2) DTO (saída do Use Case)
    class MediaKmDto
    {
        public double MediaKm { get; set; }
        public int QuantidadeAtivos { get; set; }
    }

    // 3) INTERFACE (PORTA)
    interface IVeiculoRepositorio
    {
        List<Veiculo> BuscarTodos();
    }

     // 4) INFRA (Fake A)
    class VeiculoRepositoryFakeA : IVeiculoRepositorio
    {
        private readonly List<Veiculo> _veiculos = new List<Veiculo>
        {
            new Veiculo("ABC1234", 10000, true),
            new Veiculo("DEF5678", 20000, false),
            new Veiculo("GHI9012", 15000, true),
            new Veiculo("JKL3456", 30000, true)
        };

        public List<Veiculo> BuscarTodos() => _veiculos.ToList();
    }
    
     // 5) INFRA (Fake B) - dados diferentes
    class VeiculoRepositorioFakeB : IVeiculoRepositorio
    {
        private readonly List<Veiculo> _veiculos = new()
        {
            new Veiculo("MNO1P23", 30000, true),
            new Veiculo("QRS4T56", 90000, false),
            new Veiculo("UVW7X89", 50000, true),
        };

        public List<Veiculo> BuscarTodos() => _veiculos.ToList();
    }

    // 6) USE CASE (SERVIÇO)
    class CalcularMediaKmUseCase
    {
        private readonly IVeiculoRepositorio _veiculoRepositorio;

        public CalcularMediaKmUseCase(IVeiculoRepositorio veiculoRepositorio)
        {
            _veiculoRepositorio = veiculoRepositorio;
        }

        public MediaKmDto Executar()
        {
            var veiculos = _veiculoRepositorio.BuscarTodos();
            var veiculosAtivos = veiculos.Where(v => v.Ativo).ToList();
            var quantidadeAtivos = veiculosAtivos.Count;

            double mediaKm = 0;
            if (quantidadeAtivos > 0)
            {
                mediaKm = veiculosAtivos.Average(v => v.Km);
            }

            return new MediaKmDto
            {
                MediaKm = mediaKm,
                QuantidadeAtivos = quantidadeAtivos
            };
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Usando o primeiro repositório fake
            IVeiculoRepositorio veiculoRepositorio = new VeiculoRepositoryFakeA();
            var CalcularMediaKmUseCase = new CalcularMediaKmUseCase(veiculoRepositorio);
            var resultado = CalcularMediaKmUseCase.Executar();

            Console.WriteLine("Usando VeiculoRepositoryFake:");
            Console.WriteLine($"Média de Km dos veículos ativos: {resultado.MediaKm:F2}");
            Console.WriteLine($"Quantidade de veículos ativos: {resultado.QuantidadeAtivos}");

            // Usando o segundo repositório fake
            IVeiculoRepositorio veiculoRepositorioB = new VeiculoRepositorioFakeB();
            var CalcularMediaKmUseCaseB = new CalcularMediaKmUseCase(veiculoRepositorioB);
            var resultadoB = CalcularMediaKmUseCaseB.Executar();

            Console.WriteLine("\nUsando VeiculoRepositorioFakeB:");
            Console.WriteLine($"Média de Km dos veículos ativos: {resultadoB.MediaKm:F2}");
            Console.WriteLine($"Quantidade de veículos ativos: {resultadoB.QuantidadeAtivos}");
        }
    }

}