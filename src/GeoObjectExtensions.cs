// Copyright (c) Goncalo Oliveira. All rights reserved.
// Licensed under the MIT License.

namespace Faactory.Types.GeoJson;

public static class GeoObjectExtensions
{
    /// <summary>
    /// Calculates the bounding box of the <see cref="GeoObject"/>.
    /// </summary>
    /// <param name="obj">The <see cref="GeoObject"/> to calculate the bounding box for.</param>
    /// <returns>The bounding box of the <see cref="GeoObject"/>.</returns>
    /// <exception cref="NotSupportedException">Thrown when the <see cref="GeoObject"/> type is not supported.</exception>
    public static GeoBoundingBox CalculateBoundingBox( this GeoObject obj )
    {
        ArgumentNullException.ThrowIfNull( obj, nameof( obj ) );

        if ( obj.BoundingBox is not null )
        {
            return obj.BoundingBox;
        }

        return obj switch
        {
            GeoPoint point => new GeoBoundingBox(
                point.Coordinates.Longitude,
                point.Coordinates.Latitude,
                point.Coordinates.Longitude,
                point.Coordinates.Latitude
            ),

            GeoPointCollection multiPoint => multiPoint.Coordinates.AggregateBoundingBox(),

            GeoLineString lineString => lineString.Coordinates.AggregateBoundingBox(),

            GeoLineStringCollection multiLineString => multiLineString.Coordinates.SelectMany( x => x )
                .AggregateBoundingBox(),

            GeoPolygon polygon => polygon.Coordinates.SelectMany( x => x ).AggregateBoundingBox(),

            GeoPolygonCollection multiPolygon => multiPolygon.Coordinates.SelectMany( x => x )
                .SelectMany( x => x )
                .AggregateBoundingBox(),

            GeoCollection geometryCollection => geometryCollection.Geometries.Select( x => x.CalculateBoundingBox() )
                !.Aggregate(),

            GeoFeature feature => feature.Geometry.CalculateBoundingBox(),

            GeoFeatureCollection featureCollection => featureCollection.Features.Select( x => x.Geometry.CalculateBoundingBox() )
                !.Aggregate(),

            _ => throw new NotSupportedException( $"The type {obj.GetType().Name} is not supported." )
        };
    }

    private static GeoBoundingBox Aggregate( this IEnumerable<GeoBoundingBox> boundingBoxes )
    {
        ArgumentNullException.ThrowIfNull( boundingBoxes, nameof( boundingBoxes ) );

        double east = double.MinValue;
        double west = double.MaxValue;
        double north = double.MinValue;
        double south = double.MaxValue;

        foreach ( var boundingBox in boundingBoxes )
        {
            east = Math.Max( east, boundingBox.East );
            west = Math.Min( west, boundingBox.West );
            north = Math.Max( north, boundingBox.North );
            south = Math.Min( south, boundingBox.South );
        }

        return new GeoBoundingBox( west, south, east, north );
    }

    private static GeoBoundingBox AggregateBoundingBox( this IEnumerable<GeoPosition> positions )
    {
        ArgumentNullException.ThrowIfNull( positions, nameof( positions ) );

        double minLongitude = double.MaxValue;
        double minLatitude = double.MaxValue;
        double maxLongitude = double.MinValue;
        double maxLatitude = double.MinValue;

        foreach ( var position in positions )
        {
            minLongitude = Math.Min( minLongitude, position.Longitude );
            minLatitude = Math.Min( minLatitude, position.Latitude );
            maxLongitude = Math.Max( maxLongitude, position.Longitude );
            maxLatitude = Math.Max( maxLatitude, position.Latitude );
        }

        return new GeoBoundingBox( minLongitude, minLatitude, maxLongitude, maxLatitude );
    }
}
