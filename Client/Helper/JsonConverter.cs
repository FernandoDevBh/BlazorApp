using System.Text.Json;

namespace Client.Helper;

public static class JsonConverter
{
    public static string SerializeObject(this object obj)
    {
        return JsonSerializer.Serialize(obj, GetSerializerOptions());
    }

    public static T DeserializeObject<T>(this string json) where T : class
    {
        var result = JsonSerializer.Deserialize<T>(json, GetSerializerOptions());
        if(result == null) { throw new JsonException("Impossible to convert"); }
        return result;
    }

    public static T Deserialize<T>(this byte[] jsonBytes) where T : class
    {
        var result = JsonSerializer.Deserialize<T>(jsonBytes);
        if(result == null) { throw new JsonException("Impossible to convert"); }
        return result;
    }

    private static JsonSerializerOptions GetSerializerOptions() =>
        new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
}
