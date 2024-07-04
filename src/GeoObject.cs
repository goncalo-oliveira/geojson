// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// Copyright (c) Goncalo Oliveira. All rights reserved.
// Licensed under the MIT License.

using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Faactory.Types.GeoJson;

/// <summary>
/// A base type for all spatial types.
/// </summary>
[JsonConverter(typeof(GeoJsonConverter))]
public abstract class GeoObject
{
    internal static readonly IReadOnlyDictionary<string, object?> DefaultProperties = new ReadOnlyDictionary<string, object?>(new Dictionary<string, object?>());
    internal IReadOnlyDictionary<string, object?> CustomProperties { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="GeoObject"/>.
    /// </summary>
    /// <param name="boundingBox">The <see cref="GeoBoundingBox"/> to use.</param>
    /// <param name="customProperties">The set of custom properties associated with the <see cref="GeoObject"/>.</param>
    internal GeoObject(GeoBoundingBox? boundingBox, IReadOnlyDictionary<string, object?> customProperties)
    {
        ArgumentNullException.ThrowIfNull(customProperties, nameof(customProperties));

        BoundingBox = boundingBox;
        CustomProperties = customProperties;
    }

    /// <summary>
    /// Gets the GeoJSON type of this object.
    /// </summary>
    public abstract GeoObjectType Type { get; }

    /// <summary>
    /// Represents information about the coordinate range of the <see cref="GeoObject"/>.
    /// </summary>
    public GeoBoundingBox? BoundingBox { get; }

    /// <summary>
    /// Tries to get a value of a custom property associated with the <see cref="GeoObject"/>.
    /// </summary>
    public bool TryGetCustomProperty(string name, out object? value) => CustomProperties.TryGetValue(name, out value);

    /// <summary>
    /// Converts an instance of <see cref="GeoObject"/> to a GeoJSON representation.
    /// </summary>
    /// <returns>The GeoJSON representation of the object.</returns>
    public override string ToString()
    {
        using MemoryStream stream = new();
        using Utf8JsonWriter writer = new( stream );

        GeoJsonConverter.Write( writer, this );

        writer.Flush();

        return Encoding.UTF8.GetString( stream.ToArray() );
    }

    /// <summary>
    /// Parses an instance of <see cref="GeoObject"/> from provided JSON representation.
    /// </summary>
    /// <param name="json">The GeoJSON representation of an object.</param>
    /// <returns>The resulting <see cref="GeoObject"/> object.</returns>
    /// <exception cref="JsonException">Thrown when the JSON is invalid.</exception>
    public static GeoObject Parse( string json )
    {
        using JsonDocument jsonDocument = JsonDocument.Parse( json );
        return GeoJsonConverter.Read( jsonDocument.RootElement );
    }

    /// <summary>
    /// Parses an instance of a specific GeoJSON object from provided JSON representation.
    /// </summary>
    /// <typeparam name="T">The type of the GeoJSON object to parse.</typeparam>
    /// <param name="json">The GeoJSON representation of an object.</param>
    /// <returns>The resulting <see cref="GeoObject"/> object with type <typeparamref name="T"/>.</returns>
    /// <exception cref="JsonException">Thrown when the JSON is invalid.</exception>
    /// <exception cref="JsonException">Thrown when the JSON does not represent an object of type <typeparamref name="T"/>.</exception>
    public static T Parse<T>( string json ) where T : GeoObject
        => AssertTypeOf<T>( Parse( json ) );

    /// <summary>
    /// Parses an instance of <see cref="GeoObject"/> from provided JSON representation.
    /// </summary>
    /// <param name="utf8Json">The GeoJSON UTF-8 encoded stream.</param>
    /// <returns>The resulting <see cref="GeoObject"/> object.</returns>
    /// <exception cref="JsonException">Thrown when the JSON is invalid.</exception>
    public static GeoObject Parse( Stream utf8Json )
    {
        using JsonDocument jsonDocument = JsonDocument.Parse( utf8Json );
        return GeoJsonConverter.Read(jsonDocument.RootElement);
    }

    /// <summary>
    /// Parses an instance of a specific GeoJSON object from provided JSON representation.
    /// </summary>
    /// <typeparam name="T">The type of the GeoJSON object to parse.</typeparam>
    /// <param name="utf8Json">The GeoJSON UTF-8 encoded stream.</param>
    /// <returns>The resulting <see cref="GeoObject"/> object with type <typeparamref name="T"/>.</returns>
    /// <exception cref="JsonException">Thrown when the JSON is invalid.</exception>
    /// <exception cref="JsonException">Thrown when the JSON does not represent an object of type <typeparamref name="T"/>.</exception>
    public static T Parse<T>( Stream utf8Json ) where T : GeoObject
        => AssertTypeOf<T>( Parse( utf8Json ) );

    /// <summary>
    /// Parses an instance of <see cref="GeoObject"/> from provided JSON representation.
    /// </summary>
    /// <param name="utf8Json">The GeoJSON UTF-8 encoded text.</param>
    /// <returns>The resulting <see cref="GeoObject"/> object.</returns>
    /// <exception cref="JsonException">Thrown when the JSON is invalid.</exception>
    public static GeoObject Parse( ReadOnlyMemory<byte> utf8Json )
    {
        using JsonDocument jsonDocument = JsonDocument.Parse( utf8Json );
        return GeoJsonConverter.Read(jsonDocument.RootElement);
    }

    /// <summary>
    /// Parses an instance of a specific GeoJSON object from provided JSON representation.
    /// </summary>
    /// <typeparam name="T">The type of the GeoJSON object to parse.</typeparam>
    /// <param name="utf8Json">The GeoJSON UTF-8 encoded text.</param>
    /// <returns>The resulting <see cref="GeoObject"/> object with type <typeparamref name="T"/>.</returns>
    /// <exception cref="JsonException">Thrown when the JSON is invalid.</exception>
    /// <exception cref="JsonException">Thrown when the JSON does not represent an object of type <typeparamref name="T"/>.</exception>
    public static T Parse<T>( ReadOnlyMemory<byte> utf8Json ) where T : GeoObject
        => AssertTypeOf<T>( Parse( utf8Json ) );

    private static T AssertTypeOf<T>(  GeoObject obj ) where T : GeoObject
    {
        if( obj is not T typedObj )
        {
            throw new JsonException( $"The provided JSON does not represent a {typeof( T ).Name} object." );
        }

        return typedObj;
    }
}
