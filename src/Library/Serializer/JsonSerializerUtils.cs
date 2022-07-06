using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json;

namespace NavalBattle
{
    /// <summary>
    /// Clase donde reside la mayoría del código relacionado a la serialización/deserialización de Json.
    /// </summary>
    public static class JsonSerializerUtils
    {
        private static JsonSerializerOptions options = new JsonSerializerOptions()
        {
            WriteIndented = true,
        };

        private static string path = @".\..\..\Memory\memory.json";

        /// <summary>
        /// Método para serializar objetos GameUser a formato Json.
        /// </summary>
        public static void SerializeUsers()
        {
            using var fileStream = new FileStream(path, FileMode.Create);

            foreach (GameUser user in UserRegister.Instance.UserData)
            {
                user.Player = null;
                user.State = 0;
            }
            System.Text.Json.JsonSerializer.Serialize(fileStream, UserRegister.Instance.UserData, options);
        }

        /// <summary>
        /// Método para deserializar strings en formato Json a objetos GameUser.
        /// </summary>
        public static void DeserializeUsers()
        {
            string json = File.ReadAllText(path);
            List<GameUser> user = JsonConvert.DeserializeObject<List<GameUser>>(json);
            foreach (GameUser gameUser in user)
            {
                UserRegister.Instance.Add(gameUser);
            }
        }
    }
}