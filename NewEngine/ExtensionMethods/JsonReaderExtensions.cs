using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NewEngine.ExtensionMethods;

public static class JsonReaderExtensions
{
    public static T GetPropertyOrThrow<T>(this JToken token, string name) where T : JToken
    {
        if (token[name] == null)
            throw new JsonSerializationException($"Missing expected {name} property");
        if (token[name] is not T)
            throw new JsonSerializationException($"Expected {typeof(T)} for {name} property, got {token[name]!.GetType()}");
        return (T)token[name]!;
    }
}