using Newtonsoft.Json;

namespace NewEngine.ExtensionMethods;

/// <summary>
/// Extension methods for writing JSON properties.
/// </summary>
public static class JsonWriterExtensions
{
    /// <summary>
    /// Writes a property name and value in a single call.
    /// </summary>
    /// <param name="writer">The JSON writer.</param>
    /// <param name="name">The property name.</param>
    /// <param name="value">The property value.</param>
    public static void WriteProperty(this JsonWriter writer, string name, object value)
    {
        writer.WritePropertyName(name);
        writer.WriteValue(value);
    }
}