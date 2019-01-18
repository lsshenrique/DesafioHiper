using DesafioHiper.Model;
using DesafioHiper.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace DesafioHiper
{
    internal class Program
    {
        private static List<string> _csvFaturasZeradas;
        private static List<string> _csvFaturasPag6;
        private static List<string> _csvFaturasPag12;
        private static List<string> _csvFaturasPag12x;
        private static Stopwatch _stopWatch;


        private static void Main(string[] args)
        {
            InicializarTimer();

            var caminhoArquivo = @"Arquivos\Baseficticia.txt";
            var fileCsv = File.ReadAllLines(caminhoArquivo);
            var todosRegistros = RegistroFaturaConvert.Convert(fileCsv, out List<string> registrosErrosFormato);

            PrintRegistroComErroNoFormato(registrosErrosFormato);

            Processar(todosRegistros);

            ExportarParaArquivoCsv();

            PararTimer();

            Console.ReadKey();
        }

        private static void PararTimer()
        {
            _stopWatch.Stop();
            Console.WriteLine($"Tempo Total: {_stopWatch.ElapsedMilliseconds} milliseconds");
        }

        private static void InicializarTimer()
        {
            _stopWatch = new Stopwatch();
            _stopWatch.Start();
        }

        private static void ExportarParaArquivoCsv()
        {
            if (!Directory.Exists("Arquivos"))
                Directory.CreateDirectory("Arquivos");

            File.WriteAllLines(@"Arquivos\FaturasZeradas.csv", _csvFaturasZeradas);
            File.WriteAllLines(@"Arquivos\FaturasPag6.csv", _csvFaturasPag6);
            File.WriteAllLines(@"Arquivos\FaturasPag12.csv", _csvFaturasPag12);
            File.WriteAllLines(@"Arquivos\FaturasPag12x.csv", _csvFaturasPag12x);
        }

        private static void Processar(List<Registro> todosRegistros)
        {
            var registrosValidados = AplicaFiltroCepValido(todosRegistros);

            var cabecalho = "NomeCliente;EnderecoCompleto;ValorFatura;NumeroPaginas";
            _csvFaturasZeradas = new List<string> { cabecalho };
            _csvFaturasPag6 = new List<string> { cabecalho };
            _csvFaturasPag12 = new List<string> { cabecalho };
            _csvFaturasPag12x = new List<string> { cabecalho };

            foreach (var item in registrosValidados.OrderBy(x => x.NumeroPaginas))
            {
                var itemCsv = RegistroFaturaConvert.Convert(item);

                if (item.ValorFatura == 0)
                {
                    _csvFaturasZeradas.Add(itemCsv);
                }
                else
                {
                    if (item.NumeroPaginas <= 6)
                    {
                        _csvFaturasPag6.Add(itemCsv);
                        _csvFaturasPag12.Add(itemCsv);
                        _csvFaturasPag12x.Add(itemCsv);
                    }
                    else if (item.NumeroPaginas <= 12)
                    {
                        _csvFaturasPag12.Add(itemCsv);
                        _csvFaturasPag12x.Add(itemCsv);
                    }
                    else
                    {
                        _csvFaturasPag12x.Add(itemCsv);
                    }
                }
            }
        }

        private static List<Registro> AplicaFiltroCepValido(List<Registro> todosRegistros)
        {
            return todosRegistros.Where(x => Validador.ValidarCep(x.CEP)).ToList();
        }

        private static void PrintRegistroComErroNoFormato(List<string> registrosErrosFormato)
        {
            if (registrosErrosFormato.Any())
            {
                Console.WriteLine("Registros com erro em formato:");

                foreach (var item in registrosErrosFormato)
                    Console.WriteLine(item);
            }
        }
    }
}

