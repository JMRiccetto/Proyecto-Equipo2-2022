using System.Text.Json;

namespace NavalBattle
{
    public interface IJsonConvertible
    {
        string ConvertToJson(JsonSerializerOptions options);
    }
}