using System.Text.RegularExpressions;

namespace Control_Pedidos.Helpers
{
    /// <summary>
    /// Métodos de validación simples para formularios.
    /// </summary>
    public static class ValidationHelper
    {
        private static readonly Regex EmailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);
        private static readonly Regex RfcRegex = new Regex(@"^[A-ZÑ&]{3,4}[0-9]{6}[A-Z0-9]{3}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static bool IsEmail(string value)
        {
            return !string.IsNullOrWhiteSpace(value) && EmailRegex.IsMatch(value);
        }

        public static bool IsRfc(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            return RfcRegex.IsMatch(value);
        }
    }
}
