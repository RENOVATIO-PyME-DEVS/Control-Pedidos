using System.Security.Cryptography;
using System.Text;

namespace Control_Pedidos.Helpers
{
    /// <summary>
    /// Utilidad para encriptar contraseñas usando SHA256.
    /// </summary>
    public static class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                // Si llega vacío devolvemos string vacío para no calcular nada.
                return string.Empty;
            }

            using (var sha256 = SHA256.Create())
            {
                // Codificamos la contraseña como UTF8 y generamos el hash en hexadecimal.
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                var builder = new StringBuilder(hash.Length * 2);
                foreach (var b in hash)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
