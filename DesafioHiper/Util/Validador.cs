using System.Text.RegularExpressions;

namespace DesafioHiper.Util
{
    public static class Validador
    {
        public static bool ValidarCep(string cep)
        {
            return Regex.IsMatch(cep, @"^(?=\d*[1-9])\d{8}$");
        }
    }
}
