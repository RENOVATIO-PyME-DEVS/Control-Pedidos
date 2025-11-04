using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Control_Pedidos.Data
{
    [DataContract]
    public class UserPreferences
    {
        private const string PreferencesFileName = "userprefs.json";

        public UserPreferences()
        {
            // Empezamos con valores vacíos para no romper cuando todavía no hay historial.
            LastUsername = string.Empty;
        }

        [DataMember(Name = "lastUsername")]
        public string LastUsername { get; set; }

        public static UserPreferences Load()
        {
            var path = GetPreferencesPath();
            if (!File.Exists(path))
            {
                // Si nunca se guardó nada, devolvemos una instancia limpia.
                return new UserPreferences();
            }

            try
            {
                using (var stream = File.OpenRead(path))
                {
                    // Leemos el JSON y lo convertimos en nuestro objeto de preferencias.
                    var serializer = new DataContractJsonSerializer(typeof(UserPreferences));
                    var loaded = serializer.ReadObject(stream) as UserPreferences;
                    return loaded ?? new UserPreferences();
                }
            }
            catch (SerializationException)
            {
                // Si el archivo está corrupto, preferimos resetear y seguir en lugar de explotar.
                return new UserPreferences();
            }
            catch (IOException)
            {
                // Lo mismo para errores de IO: devolvemos algo por defecto.
                return new UserPreferences();
            }
        }

        public void Save()
        {
            var path = GetPreferencesPath();
            using (var stream = File.Create(path))
            {
                // Guardamos el último usuario en un JSON muy simple.
                var serializer = new DataContractJsonSerializer(typeof(UserPreferences));
                serializer.WriteObject(stream, this);
            }
        }

        private static string GetPreferencesPath()
        {
            // Guardamos el archivo en la misma carpeta que la app para mantener todo junto.
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            return Path.Combine(basePath, PreferencesFileName);
        }
    }
}
