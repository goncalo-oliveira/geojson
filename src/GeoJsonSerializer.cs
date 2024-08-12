namespace Faactory.Types.GeoJson;

public static class GeoJsonSerializer
{
    /// <summary>
    /// If true, an exception will be thrown when deserializing a GeoJson object that is not valid.
    /// </summary>
    public static bool ThrowOnDeserializationError { get; set; } = false;
}
