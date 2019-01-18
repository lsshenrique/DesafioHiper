using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioHiper.Model
{
    public class Registro
    {
        public string NomeCliente { get; set; }
        public string CEP { get; set; }
        public string RuaComComplemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public decimal ValorFatura { get; set; }
        public int NumeroPaginas { get; set; }

        public string EnderecoCompleto => $"{RuaComComplemento} - {Bairro} - {Cidade} - {Estado}";
    }
}
