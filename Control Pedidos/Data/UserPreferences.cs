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
            LastUsername = string.Empty;
        }

        [DataMember(Name = "lastUsername")]
        public string LastUsername { get; set; }

        public static UserPreferences Load()
        {
            var path = GetPreferencesPath();
            if (!File.Exists(path))
            {
                return new UserPreferences();
            }

            try
            {
                using (var stream = File.OpenRead(path))
                {
                    var serializer = new DataContractJsonSerializer(typeof(UserPreferences));
                    var loaded = serializer.ReadObject(stream) as UserPreferences;
                    return loaded ?? new UserPreferences();
                }
            }
            catch (SerializationException)
            {
                return new UserPreferences();
            }
            catch (IOException)
            {
                return new UserPreferences();
            }
        }

        public void Save()
        {
            var path = GetPreferencesPath();
            using (var stream = File.Create(path))
            {
                var serializer = new DataContractJsonSerializer(typeof(UserPreferences));
                serializer.WriteObject(stream, this);
            }
        }

        private static string GetPreferencesPath()
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            return Path.Combine(basePath, PreferencesFileName);
        }
    }
}
