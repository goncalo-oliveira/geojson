// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// Copyright (c) Goncalo Oliveira. All rights reserved.
// Licensed under the MIT License.

using System.Collections;
using System.Text.Json.Serialization;

namespace Faactory.Types.GeoJson;

/// <summary>
/// Represents a geometry that is composed of multiple <see cref="GeoPolygon"/>.
/// </summary>
[JsonConverter(typeof(GeoJsonConverter))]
public sealed class GeoPolygonCollection : GeoObject, IReadOnlyList<GeoPolygon>
{
    /// <summary>
    /// Initializes new instance of <see cref="GeoPolygonCollection"/>.
    /// </summary>
    /// <param name="polygons">The collection of inner polygons.</param>
    public GeoPolygonCollection(IEnumerable<GeoPolygon> polygons): this(polygons, null, DefaultProperties)
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="GeoPolygonCollection"/>.
    /// </summary>
    /// <param name="polygons">The collection of inner geometries.</param>
    /// <param name="boundingBox">The <see cref="GeoBoundingBox"/> to use.</param>
    /// <param name="customProperties">The set of custom properties associated with the <see cref="GeoObject"/>.</param>
    public GeoPolygonCollection(IEnumerable<GeoPolygon> polygons, GeoBoundingBox? boundingBox, IReadOnlyDictionary<string, object?> customProperties): base(boundingBox, customProperties)
    {
        ArgumentNullException.ThrowIfNull(polygons, nameof(polygons));

        Polygons = polygons.ToArray();
    }

    internal IReadOnlyList<GeoPolygon> Polygons { get; }

    /// <inheritdoc />
    public IEnumerator<GeoPolygon> GetEnumerator() => Polygons.GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <inheritdoc />
    public int Count => Polygons.Count;

    /// <inheritdoc />
    public GeoPolygon this[int index] => Polygons[index];

    /// <summary>
    /// Returns a view over the coordinates array that forms this geometry.
    /// </summary>
    public GeoArray<GeoArray<GeoArray<GeoPosition>>> Coordinates => new( this );

    /// <inheritdoc />
    public override GeoObjectType Type { get; } = GeoObjectType.MultiPolygon;
}
