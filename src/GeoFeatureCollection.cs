// Copyright (c) Goncalo Oliveira. All rights reserved.
// Licensed under the MIT License.

using System.Collections;
using System.Text.Json.Serialization;

namespace Faactory.Types.GeoJson;

/// <summary>
/// Represents a feature collection object that contains a list of features.
/// </summary>
/// <example>
/// <code snippet="Snippet:CreateFeatureCollection" language="csharp">
/// var featureCollection = new GeoFeatureCollection(
/// [
///     new GeoFeature(
///         new GeoPoint( 1, 2 ),
///         new Dictionary<string, object?>
///         {
///             ["name"] = "Feature 1"
///         }
///     ),
///     new GeoFeature(
///         new GeoPoint( 3, 4 ),
///         new Dictionary<string, object?>
///         {
///             ["name"] = "Feature 2"
///         }
///     )
/// ]
/// );
/// </code>
/// </example>
[JsonConverter( typeof( GeoJsonConverter ) )]
public sealed class GeoFeatureCollection : GeoObject, IReadOnlyList<GeoFeature>
{
    public GeoFeatureCollection( IEnumerable<GeoFeature> features )
        : this( features, null, DefaultProperties )
    { }

    public GeoFeatureCollection(
        IEnumerable<GeoFeature> features,
        GeoBoundingBox? boundingBox,
        IReadOnlyDictionary<string, object?> customProperties
    )
        : base( boundingBox, customProperties )
    {
        ArgumentNullException.ThrowIfNull( features, nameof( features ) );

        Features = features.ToArray();
    }

    /// <inheritdoc />
    public override GeoObjectType Type { get; } = GeoObjectType.FeatureCollection;

    internal IReadOnlyList<GeoFeature> Features { get; }

    /// <inheritdoc />
    public IEnumerator<GeoFeature> GetEnumerator()
    {
        return Features.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <inheritdoc />
    public int Count => Features.Count;

    /// <inheritdoc />
    public GeoFeature this[int index] => Features[index];
}
