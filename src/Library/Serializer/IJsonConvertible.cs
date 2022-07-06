using System.Text.Json;

namespace NavalBattle
{
    /// <summary>
    /// Interfaz para implementar la serialización de Json.
    /// </summary>
    public interface IJsonConvertible
    {
        /// <summary>
        /// Método para serializar objetos a Json.
        /// </summary>
        /// <returns></returns>
        string ConvertToJson();

        /// <summary>
        /// Método para convertir strings en formato Json a texto.
        /// </summary>
        /// <param name="json"></param>
        void LoadFromJson(string json);
    }
}