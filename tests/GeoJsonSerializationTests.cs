// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// Copyright (c) Goncalo Oliveira. All rights reserved.
// Licensed under the MIT License.

using System.Runtime.InteropServices;
using System.Text.Json;
using Faactory.Types.GeoJson;

namespace Faactory.Types.Tests;

public class GeoJsonSerializationTests
{
    [Theory]
    [InlineData( 2 )]
    [InlineData( 3 )]
    public void CanRoundTripPoint( int _points )
    {
        var p = new PositionHelper( _points );

        var input = $"{{ \"type\": \"Point\", \"coordinates\": [{p.PS(0)}] }}";

        var point = AssertRoundTrip<GeoPoint>( input );
        Assert.Equal( p.P(0), point.Coordinates );
    }

    [Theory]
    [InlineData( 2 )]
    [InlineData( 3 )]
    public void CanRoundTripBBox( int _points )
    {
        var p = new PositionHelper( _points );

        var input = $"{{ \"type\": \"Point\", \"coordinates\": [{p.PS(0)}], \"bbox\": [ {p.PS(1)}, {p.PS(2)} ] }}";

        var point = AssertRoundTrip<GeoPoint>(input);
        Assert.NotNull(point.BoundingBox);
        Assert.Equal(p.P(0), point.Coordinates);
        Assert.Equal(p.P(1).Longitude, point.BoundingBox.West);
        Assert.Equal(p.P(1).Latitude, point.BoundingBox.South);

        Assert.Equal(p.P(2).Longitude, point.BoundingBox.East);
        Assert.Equal(p.P(2).Latitude, point.BoundingBox.North);

        Assert.Equal(p.P(1).Altitude, point.BoundingBox.MinAltitude);
        Assert.Equal(p.P(2).Altitude, point.BoundingBox.MaxAltitude);

        if (_points == 2)
        {
            Assert.Equal(p.P(1).Longitude, point.BoundingBox[0]);
            Assert.Equal(p.P(1).Latitude, point.BoundingBox[1]);

            Assert.Equal(p.P(2).Longitude, point.BoundingBox[2]);
            Assert.Equal(p.P(2).Latitude, point.BoundingBox[3]);
        }
        else
        {
            Assert.Equal(p.P(1).Longitude, point.BoundingBox[0]);
            Assert.Equal(p.P(1).Latitude, point.BoundingBox[1]);
            Assert.Equal(p.P(1).Altitude, point.BoundingBox[2]);

            Assert.Equal(p.P(2).Longitude, point.BoundingBox[3]);
            Assert.Equal(p.P(2).Latitude, point.BoundingBox[4]);
            Assert.Equal(p.P(2).Altitude, point.BoundingBox[5]);
        }
    }

    [Theory]
    [InlineData( 2 )]
    [InlineData( 3 )]
    public void CanRoundTripNullBBox( int _points )
    {
        var p = new PositionHelper( _points );

        var input = $"{{ \"type\": \"Point\", \"coordinates\": [{p.PS(0)}], \"bbox\": null }}";

        var point = AssertRoundTrip<GeoPoint>(input);
        Assert.Equal(p.P(0), point.Coordinates);
        Assert.Null(point.BoundingBox);
    }

    [Theory]
    [InlineData( 2 )]
    [InlineData( 3 )]
    public void CanRoundTripAdditionalProperties( int _points )
    {
        var p = new PositionHelper( _points );

        var input = $"{{ \"type\": \"Point\", \"coordinates\": [{p.PS(0)}]," +
                    $" \"additionalNumber\": 1," +
                    $" \"additionalNumber2\": 2.2," +
                    $" \"additionalNumber3\": 9999999999999999999," +
                    $" \"additionalString\": \"hello\", " +
                    $" \"additionalBool\": true, " +
                    $" \"additionalNull\": null, " +
                    $" \"additionalArray\": [1, 2.2, 9999999999999999999, \"hello\", true, null]," +
                    $" \"additionalObject\": {{ " +
                    $"    \"additionalNumber\": 1," +
                    $"    \"additionalNumber2\": 2.2" +
                    $" }}" +
                    $" }}";

        var point = AssertRoundTrip<GeoPoint>(input);
        Assert.Equal(p.P(0), point.Coordinates);
        Assert.Equal(1, point.CustomProperties["additionalNumber"]);
        Assert.Equal(2.2, point.CustomProperties["additionalNumber2"]);
        Assert.Equal(9999999999999999999L, point.CustomProperties["additionalNumber3"]);
        Assert.Equal("hello", point.CustomProperties["additionalString"]);
        Assert.Null( point.CustomProperties["additionalNull"]);
        Assert.Equal(true, point.CustomProperties["additionalBool"]);
        Assert.Equal(new object?[] {1, 2.2, 9999999999999999999L, "hello", true, null}, point.CustomProperties["additionalArray"]);

        Assert.True( point.TryGetCustomProperty("additionalObject", out var obj));
        Assert.True(obj is IReadOnlyDictionary<string, object>);
        var dictionary = (IReadOnlyDictionary<string, object>) obj;
        Assert.Equal(1, dictionary["additionalNumber"]);
        Assert.Equal(2.2, dictionary["additionalNumber2"]);
    }

    [Theory]
    [InlineData( 2 )]
    [InlineData( 3 )]
    public void CanRoundTripPolygon( int _points )
    {
        var p = new PositionHelper( _points );

        var input = $" {{ \"type\": \"Polygon\", \"coordinates\": [ [ [{p.PS(0)}], [{p.PS(1)}], [{p.PS(2)}], [{p.PS(3)}], [{p.PS(4)}], [{p.PS(0)}] ] ] }}";

        var polygon = AssertRoundTrip<GeoPolygon>(input);
        Assert.Equal(1, polygon.Rings.Count);

        Assert.True( new[]
        {
            p.P(0),
            p.P(1),
            p.P(2),
            p.P(3),
            p.P(4),
            p.P(0),
        }.SequenceEqual( polygon.Rings[0].Coordinates ) );
    }

    [Theory]
    [InlineData( 2 )]
    [InlineData( 3 )]
    public void CanRoundTripPolygonHoles( int _points )
    {
        var p = new PositionHelper( _points );

        var input = $"{{ \"type\": \"Polygon\", \"coordinates\": [" +
                    $" [ [{p.PS(0)}], [{p.PS(1)}], [{p.PS(2)}], [{p.PS(3)}], [{p.PS(4)}], [{p.PS(0)}] ]," +
                    $" [ [{p.PS(5)}], [{p.PS(6)}], [{p.PS(7)}], [{p.PS(8)}], [{p.PS(9)}], [{p.PS(5)}] ]" +
                    $" ] }}";

        var polygon = AssertRoundTrip<GeoPolygon>(input);
        Assert.Equal(2, polygon.Rings.Count);

        Assert.True( new[]
        {
            p.P(0),
            p.P(1),
            p.P(2),
            p.P(3),
            p.P(4),
            p.P(0),
        }.SequenceEqual( polygon.Rings[0].Coordinates ) );

        Assert.True(new[]
        {
            p.P(5),
            p.P(6),
            p.P(7),
            p.P(8),
            p.P(9),
            p.P(5),
        }.SequenceEqual( polygon.Rings[1].Coordinates ) );
    }

    [Theory]
    [InlineData( 2 )]
    [InlineData( 3 )]
    public void CanRoundTripMultiPoint( int _points )
    {
        var p = new PositionHelper( _points );

        var input = $"{{ \"type\": \"MultiPoint\", \"coordinates\": [ [{p.PS(0)}], [{p.PS(1)}] ] }}";

        var multipoint = AssertRoundTrip<GeoPointCollection>(input);
        Assert.Equal(2, multipoint.Points.Count);

        Assert.Equal(p.P(0), multipoint.Points[0].Coordinates);
        Assert.Equal(p.P(1), multipoint.Points[1].Coordinates);
    }

    [Theory]
    [InlineData( 2 )]
    [InlineData( 3 )]
    public void CanRoundTripMultiLineString( int _points )
    {
        var p = new PositionHelper( _points );

        var input = $"{{ \"type\": \"MultiLineString\", \"coordinates\": [ [ [{p.PS(0)}], [{p.PS(1)}] ], [ [{p.PS(2)}], [{p.PS(3)}] ] ] }}";

        var polygon = AssertRoundTrip<GeoLineStringCollection>(input);
        Assert.Equal(2, polygon.Lines.Count);

        Assert.True(new[]
        {
            p.P(0),
            p.P(1)
        }.SequenceEqual(polygon. Lines[0].Coordinates ) );

        Assert.True(new[]
        {
            p.P(2),
            p.P(3)
        }.SequenceEqual( polygon.Lines[1].Coordinates ) );
    }

    [Theory]
    [InlineData( 2 )]
    [InlineData( 3 )]
    public void CanRoundTripMultiPolygon( int _points )
    {
        var p = new PositionHelper( _points );

        var input = $" {{ \"type\": \"MultiPolygon\", \"coordinates\": [" +
                    $" [ [ [{p.PS(0)}], [{p.PS(1)}], [{p.PS(2)}], [{p.PS(3)}], [{p.PS(4)}], [{p.PS(0)}] ] ]," +
                    $" [" +
                    $" [ [{p.PS(0)}], [{p.PS(1)}], [{p.PS(2)}], [{p.PS(3)}], [{p.PS(4)}], [{p.PS(0)}] ]," +
                    $" [ [{p.PS(5)}], [{p.PS(6)}], [{p.PS(7)}], [{p.PS(8)}], [{p.PS(9)}], [{p.PS(5)}] ]" +
                    $" ] ]}}";

        var multiPolygon = AssertRoundTrip<GeoPolygonCollection>(input);

        var polygon = multiPolygon.Polygons[0];

        Assert.Equal(1, polygon.Rings.Count);

        Assert.True(new[]
        {
            p.P(0),
            p.P(1),
            p.P(2),
            p.P(3),
            p.P(4),
            p.P(0),
        }.SequenceEqual( polygon.Rings[0].Coordinates ) );

        polygon = multiPolygon.Polygons[1];
        Assert.Equal(2, polygon.Rings.Count);

        Assert.True(new[]
        {
            p.P(0),
            p.P(1),
            p.P(2),
            p.P(3),
            p.P(4),
            p.P(0),
        }.SequenceEqual( polygon.Rings[0].Coordinates ) );

        Assert.True(new[]
        {
            p.P(5),
            p.P(6),
            p.P(7),
            p.P(8),
            p.P(9),
            p.P(5),
        }.SequenceEqual( polygon.Rings[1].Coordinates ) );
    }

    [Theory]
    [InlineData( 2 )]
    [InlineData( 3 )]
    public void CanRoundTripGeometryCollection( int _points )
    {
        var p = new PositionHelper( _points );

        var input = $"{{ \"type\": \"GeometryCollection\", \"geometries\": [{{ \"type\": \"Point\", \"coordinates\": [{p.PS(0)}] }}, {{ \"type\": \"LineString\", \"coordinates\": [ [{p.PS(1)}], [{p.PS(2)}] ] }}] }}";

        var collection = AssertRoundTrip<GeoCollection>(input);
        var point = (GeoPoint) collection.Geometries[0];
        Assert.Equal(p.P(0), point.Coordinates);

        var lineString = (GeoLineString) collection.Geometries[1];
        Assert.Equal(p.P(1), lineString.Coordinates[0]);
        Assert.Equal(p.P(2), lineString.Coordinates[1]);

        Assert.Equal(2, collection.Geometries.Count);
    }

    [Theory]
    [InlineData( 2 )]
    [InlineData( 3 )]
    public void CanRoundTripFeature( int _points )
    {
        var p = new PositionHelper( _points );

        var input = $"{{ \"type\": \"Feature\", \"geometry\": {{ \"type\": \"Point\", \"coordinates\": [{p.PS(0)}] }}, \"properties\": {{ \"name\": \"value\" }} }}";

        var feature = AssertRoundTrip<GeoFeature>(input);
        var point = Assert.IsType<GeoPoint>(feature.Geometry);

        Assert.Equal( p.P(0), point.Coordinates );
        Assert.Equal( "value", feature.Properties["name"] );
    }

    [Theory]
    [InlineData( 2 )]
    [InlineData( 3 )]
    public void CanRoundTripFeatureWithId( int _points )
    {
        var p = new PositionHelper( _points );

        var input = $"{{ \"type\": \"Feature\", \"geometry\": {{ \"type\": \"Point\", \"coordinates\": [{p.PS(0)}] }}, \"properties\": {{ \"name\": \"value\" }}, \"id\": \"1\" }}";

        var feature = AssertRoundTrip<GeoFeature>( input );
        var point = Assert.IsType<GeoPoint>( feature.Geometry );

        Assert.Equal( p.P(0), point.Coordinates );
        Assert.Equal( "value", feature.Properties["name"] );
        Assert.Equal( "1", feature.Id );
    }

    [Theory]
    [InlineData( 2 )]
    [InlineData( 3 )]
    public void CanRoundTripFeatureWithNumberId( int _points )
    {
        var p = new PositionHelper( _points );

        var input = $"{{ \"type\": \"Feature\", \"geometry\": {{ \"type\": \"Point\", \"coordinates\": [{p.PS(0)}] }}, \"properties\": {{ \"name\": \"value\" }}, \"id\": 1 }}";

        var feature = AssertRoundTrip<GeoFeature>( input );
        var point = Assert.IsType<GeoPoint>( feature.Geometry );

        Assert.Equal( p.P(0), point.Coordinates );
        Assert.Equal( "value", feature.Properties["name"] );
        Assert.Equal( 1, feature.Id );
    }

    [Theory]
    [InlineData( 2 )]
    [InlineData( 3 )]
    public void CanRoundTripFeatureCollection( int _points )
    {
        var p = new PositionHelper( _points );

        var input = $"{{ \"type\": \"FeatureCollection\", \"features\": [{{ \"type\": \"Feature\", \"geometry\": {{ \"type\": \"Point\", \"coordinates\": [{p.PS(0)}] }}, \"properties\": {{ \"name\": \"value\", \"aaa:bbb\": \"aaa_bbb\" }} }}] }}";

        var collection = AssertRoundTrip<GeoFeatureCollection>(input);
        var feature = Assert.Single(collection);
        var point = Assert.IsType<GeoPoint>(feature.Geometry);

        Assert.Equal(p.P(0), point.Coordinates);
        Assert.Equal("value", feature.Properties["name"]);
        Assert.Equal("aaa_bbb", feature.Properties["aaa:bbb"]);
    }

    private static T AssertRoundTrip<T>(string json) where T: GeoObject
    {
        var element = JsonDocument.Parse(json).RootElement;
        var geometry = GeoJsonConverter.Read(element);

        var memoryStreamOutput = new MemoryStream();
        using (Utf8JsonWriter writer = new(memoryStreamOutput))
        {
            GeoJsonConverter.Write(writer, geometry);
        }

        var element2 = JsonDocument.Parse(memoryStreamOutput.ToArray()).RootElement;
        var geometry2 = GeoJsonConverter.Read(element2);

        // Serialize and deserialize as a base class
        var bytes = JsonSerializer.SerializeToUtf8Bytes(geometry2, typeof(GeoObject));
        var geometry3 = JsonSerializer.Deserialize<GeoObject>(bytes);

        // Serialize and deserialize as a concrete class
        var bytes2 = JsonSerializer.SerializeToUtf8Bytes(geometry3);
        var geometry4 = JsonSerializer.Deserialize<T>(bytes2);

        Assert.NotNull(geometry4);

        return geometry4;
    }

    private class PositionHelper( int points )
    {
        private readonly int _points = points;

        public string PS( int number )
        {
            if (_points == 2)
            {
                return $"{1.1 * number:G17}, {2.2 * number:G17}";
            }

            return $"{1.1 * number:G17}, {2.2 * number:G17}, {3.3 * number:G17}";
        }

        public GeoPosition P( int number )
        {
            if (_points == 2)
            {
                return new GeoPosition(1.1 * number, 2.2 * number);
            }

            return new GeoPosition(1.1 * number, 2.2 * number, 3.3 * number);
        }
    }
}
