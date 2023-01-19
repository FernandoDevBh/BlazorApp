using System.Text;

namespace Client.Extensions;

public static class ObjectExtensions
{
    public static StringContent GetStringContent<T>(this T obj) where T : class
    {
        var content = obj.SerializeObject();
        return new StringContent(content, Encoding.UTF8, SD.Application_Json);
    }
}
