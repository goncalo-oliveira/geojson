// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// Copyright (c) Goncalo Oliveira. All rights reserved.
// Licensed under the MIT License.

using Faactory.Types.GeoJson;

namespace Faactory.Types.Tests;

public class GeoArrayTests
{
    [Fact]
    public void PointCoordinatesWork()
    {
        var point = new GeoPoint( 1, 2 );

        Assert.Equal( 2, point.Coordinates.Count );
        Assert.Equal( 1, point.Coordinates[0] );
        Assert.Equal( 2, point.Coordinates[1] );
    }

    [Fact]
    public void PointCoordinatesWork3()
    {
        var point = new GeoPoint( 1, 2, 3 );

        Assert.Equal( 3, point.Coordinates.Count );

        Assert.Equal( 1, point.Coordinates[0] );
        Assert.Equal( 2, point.Coordinates[1] );
        Assert.Equal( 3, point.Coordinates[2] );
    }

    [Fact]
    public void PointCollectionCoordinatesWork()
    {
        var pointCollection = new GeoPointCollection(
        [
            new GeoPoint( 1,2 ),
            new GeoPoint( 3,4 )
        ] );

        Assert.Equal( 2, pointCollection.Coordinates.Count );
        Assert.Equal( 2, pointCollection.Coordinates.Count() );

        Assert.Equal( 1, pointCollection.Coordinates[0][0] );
        Assert.Equal( 2, pointCollection.Coordinates[0][1] );

        Assert.Equal( 3, pointCollection.Coordinates[1][0] );
        Assert.Equal( 4, pointCollection.Coordinates[1][1] );
    }

    [Fact]
    public void LineCoordinatesWork()
    {
        var line = new GeoLineString(
        [
            new GeoPosition(1, 2),
            new GeoPosition(3, 4),
            new GeoPosition(5, 6),
        ] );

        Assert.Equal( 3, line.Coordinates.Count );
        Assert.Equal( 3, line.Coordinates.Count() );

        Assert.Equal( 1, line.Coordinates[0][0] );
        Assert.Equal( 2, line.Coordinates[0][1] );

        Assert.Equal( 3, line.Coordinates[1][0] );
        Assert.Equal( 4, line.Coordinates[1][1] );

        Assert.Equal( 5, line.Coordinates[2][0] );
        Assert.Equal( 6, line.Coordinates[2][1] );
    }

    [Fact]
    public void LineCollectionCoordinatesWork()
    {
        var lineCollection = new GeoLineStringCollection(
        [
            new GeoLineString([
                new GeoPosition( 1, 2 ),
                new GeoPosition( 3, 4 )
            ] ),
            new GeoLineString([
                new GeoPosition( 5, 6 ),
                new GeoPosition( 7, 8 )
            ] ),
        ] );

        Assert.Equal( 2, lineCollection.Coordinates.Count );
        Assert.Equal( 2, lineCollection.Coordinates.Count() );

        Assert.Equal( 1, lineCollection.Coordinates[0][0][0] );
        Assert.Equal( 2, lineCollection.Coordinates[0][0][1] );

        Assert.Equal( 3, lineCollection.Coordinates[0][1][0] );
        Assert.Equal( 4, lineCollection.Coordinates[0][1][1] );

        Assert.Equal( 5, lineCollection.Coordinates[1][0][0] );
        Assert.Equal( 6, lineCollection.Coordinates[1][0][1] );

        Assert.Equal( 7, lineCollection.Coordinates[1][1][0] );
        Assert.Equal( 8, lineCollection.Coordinates[1][1][1] );
    }

    [Fact]
    public void PolygonCoordinatesWork()
    {
        var polygon = new GeoPolygon(new[]
        {
            new GeoLinearRing([
                new GeoPosition( 1, 2 ),
                new GeoPosition( 3, 4 ),
                new GeoPosition( 3, 4 ),
                new GeoPosition( 1, 2 ),
            ]),
            new GeoLinearRing([
                new GeoPosition( 5, 6 ),
                new GeoPosition( 7, 8 ),
                new GeoPosition( 7, 8 ),
                new GeoPosition( 5, 6 ),
            ]),
        });

        Assert.Equal( 2, polygon.Coordinates.Count );
        Assert.Equal( 2, polygon.Coordinates.Count() );

        Assert.Equal( 1, polygon.Coordinates[0][0][0] );
        Assert.Equal( 2, polygon.Coordinates[0][0][1] );

        Assert.Equal( 3, polygon.Coordinates[0][1][0] );
        Assert.Equal( 4, polygon.Coordinates[0][1][1] );

        Assert.Equal( 3, polygon.Coordinates[0][2][0] );
        Assert.Equal( 4, polygon.Coordinates[0][2][1] );

        Assert.Equal( 1, polygon.Coordinates[0][3][0] );
        Assert.Equal( 2, polygon.Coordinates[0][3][1] );

        Assert.Equal( 5, polygon.Coordinates[1][0][0] );
        Assert.Equal( 6, polygon.Coordinates[1][0][1] );

        Assert.Equal( 7, polygon.Coordinates[1][1][0] );
        Assert.Equal( 8, polygon.Coordinates[1][1][1] );

        Assert.Equal( 7, polygon.Coordinates[1][2][0] );
        Assert.Equal( 8, polygon.Coordinates[1][2][1] );

        Assert.Equal( 5, polygon.Coordinates[1][3][0] );
        Assert.Equal( 6, polygon.Coordinates[1][3][1] );
    }

    [Fact]
    public void PolygonCollectionCoordinatesWork()
    {
        var polygonCollection = new GeoPolygonCollection(
        [
            new GeoPolygon(new[]
            {
                new GeoLinearRing([
                    new GeoPosition( 1, 2 ),
                    new GeoPosition( 3, 4 ),
                    new GeoPosition( 3, 4 ),
                    new GeoPosition( 1, 2 )
                ]),
                new GeoLinearRing([
                    new GeoPosition( 5, 6 ),
                    new GeoPosition( 7, 8 ),
                    new GeoPosition( 7, 8 ),
                    new GeoPosition( 5, 6 )
                ]),
            }),

            new GeoPolygon(new[]
            {
                new GeoLinearRing([
                    new GeoPosition( 9, 10 ),
                    new GeoPosition( 11, 12 ),
                    new GeoPosition( 11, 12 ),
                    new GeoPosition( 9, 10 )
                ]),
                new GeoLinearRing([
                    new GeoPosition( 13, 14 ),
                    new GeoPosition( 15, 16 ),
                    new GeoPosition( 15, 16 ),
                    new GeoPosition( 13, 14 )
                ]),
            }),
        ]);

        Assert.Equal( 2, polygonCollection.Coordinates.Count );
        Assert.Equal( 2, polygonCollection.Coordinates.Count() );

        var c = polygonCollection.Coordinates[0];
        Assert.Equal( 1, polygonCollection.Coordinates[0][0][0][0] );
        Assert.Equal( 2, polygonCollection.Coordinates[0][0][0][1] );

        Assert.Equal( 3, polygonCollection.Coordinates[0][0][1][0] );
        Assert.Equal( 4, polygonCollection.Coordinates[0][0][1][1] );

        Assert.Equal( 3, polygonCollection.Coordinates[0][0][2][0] );
        Assert.Equal( 4, polygonCollection.Coordinates[0][0][2][1] );

        Assert.Equal( 1, polygonCollection.Coordinates[0][0][3][0] );
        Assert.Equal( 2, polygonCollection.Coordinates[0][0][3][1] );

        Assert.Equal( 5, polygonCollection.Coordinates[0][1][0][0] );
        Assert.Equal( 6, polygonCollection.Coordinates[0][1][0][1] );

        Assert.Equal( 7, polygonCollection.Coordinates[0][1][1][0] );
        Assert.Equal( 8, polygonCollection.Coordinates[0][1][1][1] );

        Assert.Equal( 7, polygonCollection.Coordinates[0][1][2][0] );
        Assert.Equal( 8, polygonCollection.Coordinates[0][1][2][1] );

        Assert.Equal( 5, polygonCollection.Coordinates[0][1][3][0] );
        Assert.Equal( 6, polygonCollection.Coordinates[0][1][3][1] );

        Assert.Equal( 9, polygonCollection.Coordinates[1][0][0][0] );
        Assert.Equal( 10, polygonCollection.Coordinates[1][0][0][1] );

        Assert.Equal( 11, polygonCollection.Coordinates[1][0][1][0] );
        Assert.Equal( 12, polygonCollection.Coordinates[1][0][1][1] );

        Assert.Equal( 11, polygonCollection.Coordinates[1][0][2][0] );
        Assert.Equal( 12, polygonCollection.Coordinates[1][0][2][1] );

        Assert.Equal( 9, polygonCollection.Coordinates[1][0][3][0] );
        Assert.Equal( 10, polygonCollection.Coordinates[1][0][3][1] );

        Assert.Equal( 13, polygonCollection.Coordinates[1][1][0][0] );
        Assert.Equal( 14, polygonCollection.Coordinates[1][1][0][1] );

        Assert.Equal( 15, polygonCollection.Coordinates[1][1][1][0] );
        Assert.Equal( 16, polygonCollection.Coordinates[1][1][1][1] );

        Assert.Equal( 15, polygonCollection.Coordinates[1][1][2][0] );
        Assert.Equal( 16, polygonCollection.Coordinates[1][1][2][1] );

        Assert.Equal( 13, polygonCollection.Coordinates[1][1][3][0] );
        Assert.Equal( 14, polygonCollection.Coordinates[1][1][3][1] );
    }
}
