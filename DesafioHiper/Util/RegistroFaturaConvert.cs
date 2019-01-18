using DesafioHiper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioHiper.Util
{
    public static class RegistroFaturaConvert
    {
        public static List<Registro> Convert(string[] linhasArquivo, out List<string> errosFormato)
        {
            errosFormato = new List<string>();
            var registros = new List<Registro>();

            foreach (var linha in linhasArquivo.Skip(1))
            {
                var campos = linha.Split(';');

                if (campos.Length > 8)
                {
                    errosFormato.Add(linha + ";colunas inválidas");
                }
                else
                {
                    var registro = new Registro
                    {
                        NomeCliente = campos[0],
                        CEP = campos[1].Trim(),
                        RuaComComplemento = campos[2],
                        Bairro = campos[3],
                        Cidade = campos[4],
                        Estado = campos[5],
                        ValorFatura = System.Convert.ToDecimal(campos[6]),
                        NumeroPaginas = System.Convert.ToInt32(campos[7])
                    };

                    if (registro.NumeroPaginas <= 0)
                    {
                        errosFormato.Add(linha + ";número de página inferior à 1");
                    }

                    registros.Add(registro);
                }
            }

            return registros;
        }

        /// <summary>
        /// Saída NomeCliente;EnderecoCompleto;ValorFatura;NumeroPaginas;
        /// </summary>
        /// <param name="registro"></param>
        /// <returns></returns>
        public static string Convert(Registro registro)
        {
            return $"{registro.NomeCliente};{registro.EnderecoCompleto};{registro.ValorFatura};{registro.NumeroPaginas};";
        }
    }
}
