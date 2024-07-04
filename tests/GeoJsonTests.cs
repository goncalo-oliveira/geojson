// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// Copyright (c) Goncalo Oliveira. All rights reserved.
// Licensed under the MIT License.

using Faactory.Types.GeoJson;

namespace Faactory.Types.Tests;

public class GeoJsonTests
{
    [Fact]
    public void BoundingBoxToStringReturnsStringRepresentation()
    {
        Assert.Equal( "[1, 2, 3, 4]", new GeoBoundingBox( 1, 2, 3, 4 ).ToString() );
        Assert.Equal( "[1, 2, 5, 3, 4, 6]", new GeoBoundingBox( 1, 2, 3, 4, 5, 6 ).ToString() );
    }

    [Fact]
    public void GeoObjectToStringReturnsSerializedRepresentation()
    {
        var point = new GeoPoint( 1, 2 );
        Assert.Equal( "{\"type\":\"Point\",\"coordinates\":[1,2]}", point.ToString() );
    }

    [Fact]
    public void GeoObjectParseParsesJson()
    {
        var point = GeoObject.Parse( "{\"type\":\"Point\",\"coordinates\":[1,2]}" );
        Assert.IsType<GeoPoint>( point );
        Assert.Equal( ((GeoPoint)point).Coordinates, new GeoPosition( 1, 2 ) );
    }

    [Fact]
    public void GeoObjectCalculateBoundingBox()
    {
        var point = new GeoPoint( 1, 2 );
        var boundingBox = point.CalculateBoundingBox();
        Assert.Equal( new GeoBoundingBox( 1, 2, 1, 2 ), boundingBox );

        var multiPoint = new GeoPointCollection( [new GeoPoint( 1, 2 ), new GeoPoint( 3, 4 )] );
        boundingBox = multiPoint.CalculateBoundingBox();
        Assert.Equal( new GeoBoundingBox( 1, 2, 3, 4 ), boundingBox );

        var lineString = new GeoLineString( [new GeoPosition( 1, 2 ), new GeoPosition( 3, 4 )] );
        boundingBox = lineString.CalculateBoundingBox();
        Assert.Equal( new GeoBoundingBox( 1, 2, 3, 4 ), boundingBox );

        var multiLineString = new GeoLineStringCollection( [new GeoLineString( [new GeoPosition( 1, 2 ), new GeoPosition( 3, 4 )] )] );
        boundingBox = multiLineString.CalculateBoundingBox();
        Assert.Equal( new GeoBoundingBox( 1, 2, 3, 4 ), boundingBox );

        var polygon = new GeoPolygon( [new GeoLinearRing( [new GeoPosition( 1, 2 ), new GeoPosition( 3, 4 ), new GeoPosition( 5, 6 ), new GeoPosition( 1, 2 )] )] );
        boundingBox = polygon.CalculateBoundingBox();
        Assert.Equal( new GeoBoundingBox( 1, 2, 5, 6 ), boundingBox );

        var multiPolygon = new GeoPolygonCollection( [new GeoPolygon( [new GeoLinearRing( [new GeoPosition( 1, 2 ), new GeoPosition( 3, 4 ), new GeoPosition( 5, 6 ), new GeoPosition( 1, 2 )] )] )] );
        boundingBox = multiPolygon.CalculateBoundingBox();
        Assert.Equal( new GeoBoundingBox( 1, 2, 5, 6 ), boundingBox );
    }
}
