// Copyright (c) Goncalo Oliveira. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace Faactory.Types.GeoJson;

/// <summary>
/// Represents a feature object that contains a geometry and associated properties.
/// </summary>
/// <example>
/// <code snippet="Snippet:CreateFeature" language="csharp">
/// var feature = new GeoFeature( 
///     new GeoPoint( -122.091954, 47.607148 )
/// );
/// </code>
/// </example>
[JsonConverter( typeof( GeoJsonConverter ) )]
public sealed class GeoFeature : GeoObject
{
    public GeoFeature( GeoObject geometry, IReadOnlyDictionary<string, object?>? properties = null )
        : this( null, geometry, null, properties ?? DefaultProperties, DefaultProperties )
    { }

    public GeoFeature(
        string? id,
        GeoObject geometry,
        GeoBoundingBox? boundingBox,
        IReadOnlyDictionary<string, object?> properties,
        IReadOnlyDictionary<string, object?> customProperties
    )
        : base( boundingBox, customProperties )
    {
        ArgumentNullException.ThrowIfNull( properties, nameof( properties ) );
        ArgumentNullException.ThrowIfNull( customProperties, nameof( customProperties ) );

        Id = id;
        Geometry = geometry;
        Properties = properties;
    }

    /// <summary>
    /// Gets the identifier.
    /// </summary>
    public string? Id { get; }

    /// <summary>
    /// Gets the geometry.
    /// </summary>
    public GeoObject Geometry { get; }

    /// <inheritdoc />
    public override GeoObjectType Type { get; } = GeoObjectType.Feature;

    /// <summary>
    /// Gets the properties.
    /// </summary>
    public IReadOnlyDictionary<string, object?> Properties { get; }
}
