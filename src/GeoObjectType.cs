// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// Copyright (c) Goncalo Oliveira. All rights reserved.
// Licensed under the MIT License.

namespace Faactory.Types.GeoJson;

/// <summary>
/// Identifies the type of the <see cref="GeoObject"/>
/// </summary>
public enum GeoObjectType
{
    /// <summary>
    /// The <see cref="GeoObject"/> is of the <see cref="GeoPoint"/> type.
    /// </summary>
    Point,
    /// <summary>
    /// The <see cref="GeoObject"/> is of the <see cref="GeoPointCollection"/> type.
    /// </summary>
    MultiPoint,
    /// <summary>
    /// The <see cref="GeoObject"/> is of the <see cref="GeoPolygon"/> type.
    /// </summary>
    Polygon,
    /// <summary>
    /// The <see cref="GeoObject"/> is of the <see cref="GeoPolygonCollection"/> type.
    /// </summary>
    MultiPolygon,
    /// <summary>
    /// The <see cref="GeoObject"/> is of the <see cref="GeoLineString"/> type.
    /// </summary>
    LineString,
    /// <summary>
    /// The <see cref="GeoObject"/> is of the <see cref="GeoLineStringCollection"/> type.
    /// </summary>
    MultiLineString,
    /// <summary>
    /// The <see cref="GeoObject"/> is of the <see cref="GeoCollection"/> type.
    /// </summary>
    GeometryCollection,

    /// <summary>
    /// The <see cref="GeoObject"/> is of the <see cref="GeoFeature"/> type.
    /// </summary>
    Feature,

    /// <summary>
    /// The <see cref="GeoObject"/> is of the <see cref="GeoFeatureCollection"/> type.
    /// </summary>
    FeatureCollection
}
