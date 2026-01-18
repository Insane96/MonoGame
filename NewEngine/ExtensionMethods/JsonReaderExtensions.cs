using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NewEngine.ExtensionMethods;

/// <summary>
/// Extension methods for reading and validating JSON tokens.
/// </summary>
public static class JsonReaderExtensions
{
    /// <summary>
    /// Gets a required property from a JSON token, throwing if missing or wrong type.
    /// </summary>
    /// <typeparam name="T">The expected JToken type (e.g., JObject, JArray, JValue).</typeparam>
    /// <param name="token">The parent token to read from.</param>
    /// <param name="name">The property name to retrieve.</param>
    /// <returns>The property value as the specified type.</returns>
    /// <exception cref="JsonSerializationException">Thrown if the property is missing or not of the expected type.</exception>
    public static T GetPropertyOrThrow<T>(this JToken token, string name) where T : JToken
    {
        if (token[name] == null)
            throw new JsonSerializationException($"Missing expected {name} property");
        if (token[name] is not T)
            throw new JsonSerializationException($"Expected {typeof(T)} for {name} property, got {token[name]!.GetType()}");
        return (T)token[name]!;
    }
}