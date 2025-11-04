using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Control_Pedidos.Data
{
    [DataContract]
    public class DatabaseConfig
    {
        private const string ConfigFileName = "config.json";

        [DataMember(Name = "host")]
        public string Host { get; set; } = string.Empty;

        [DataMember(Name = "username")]
        public string Username { get; set; } = string.Empty;

        [DataMember(Name = "password")]
        public string Password { get; set; } = string.Empty;

        [DataMember(Name = "database")]
        public string Database { get; set; } = string.Empty;

        public static string GetConfigPath()
        {
            // Usamos la carpeta de la app para guardar el json de configuración.
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            return Path.Combine(basePath, ConfigFileName);
        }

        public static DatabaseConfig Load()
        {
            var path = GetConfigPath();
            if (!File.Exists(path))
            {
                // Si todavía no existe el archivo devolvemos null para que el caller lo maneje.
                return null;
            }

            using (var stream = File.OpenRead(path))
            {
                // Deserializamos el JSON a nuestro objeto de configuración.
                var serializer = new DataContractJsonSerializer(typeof(DatabaseConfig));
                return serializer.ReadObject(stream) as DatabaseConfig;
            }
        }

        public void Save()
        {
            var path = GetConfigPath();
            using (var stream = File.Create(path))
            {
                // Serializamos la config actual a json para que quede guardada para la próxima sesión.
                var serializer = new DataContractJsonSerializer(typeof(DatabaseConfig));
                serializer.WriteObject(stream, this);
            }
        }

        public bool IsValid()
        {
            // Lo mínimo indispensable: host, usuario y base definidos.
            return !string.IsNullOrWhiteSpace(Host)
                   && !string.IsNullOrWhiteSpace(Username)
                   && !string.IsNullOrWhiteSpace(Database);
        }
    }
}
