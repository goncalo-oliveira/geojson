﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// Copyright (c) Goncalo Oliveira. All rights reserved.
// Licensed under the MIT License.

using System.Collections;
using System.Text.Json.Serialization;

namespace Faactory.Types.GeoJson;

/// <summary>
/// Represents a geometry that is composed of multiple <see cref="GeoPoint"/>.
/// </summary>
[JsonConverter(typeof(GeoJsonConverter))]
public sealed class GeoPointCollection : GeoObject, IReadOnlyList<GeoPoint>
{
    /// <summary>
    /// Initializes new instance of <see cref="GeoPointCollection"/>.
    /// </summary>
    /// <param name="points">The collection of inner points.</param>
    public GeoPointCollection(IEnumerable<GeoPoint> points): this(points, null, DefaultProperties)
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="GeoPointCollection"/>.
    /// </summary>
    /// <param name="points">The collection of inner points.</param>
    /// <param name="boundingBox">The <see cref="GeoBoundingBox"/> to use.</param>
    /// <param name="customProperties">The set of custom properties associated with the <see cref="GeoObject"/>.</param>
    public GeoPointCollection(IEnumerable<GeoPoint> points, GeoBoundingBox? boundingBox, IReadOnlyDictionary<string, object?> customProperties): base(boundingBox, customProperties)
    {
        ArgumentNullException.ThrowIfNull(points, nameof(points));

        Points = points.ToArray();
    }

    internal IReadOnlyList<GeoPoint> Points { get; }

    /// <inheritdoc />
    public IEnumerator<GeoPoint> GetEnumerator() => Points.GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <inheritdoc />
    public int Count => Points.Count;

    /// <inheritdoc />
    public GeoPoint this[int index] => Points[index];

    /// <summary>
    /// Returns a view over the coordinates array that forms this geometry.
    /// </summary>
    public GeoArray<GeoPosition> Coordinates => new( this );

    /// <inheritdoc />
    public override GeoObjectType Type { get; } = GeoObjectType.MultiPoint;
}
