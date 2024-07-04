// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// Copyright (c) Goncalo Oliveira. All rights reserved.
// Licensed under the MIT License.

using Faactory.Types.GeoJson;

namespace Faactory.Types.Tests;

public class GeoJsonSamples
{
    [Fact]
    public void CreatePoint()
    {
        #region Snippet:CreatePoint
        var point = new GeoPoint(-122.091954, 47.607148);
        #endregion
    }

    [Fact]
    public void CreateLineString()
    {
        #region Snippet:CreateLineString
        var line = new GeoLineString(new[]
        {
            new GeoPosition(-122.108727, 47.649383),
            new GeoPosition(-122.081538, 47.640846),
            new GeoPosition(-122.078634, 47.576066),
            new GeoPosition(-122.112686, 47.578559),
        });
        #endregion
    }

    [Fact]
    public void CreatePolygon()
    {
        #region Snippet:CreatePolygon
        var polygon = new GeoPolygon(new[]
        {
            new GeoPosition(-122.108727, 47.649383),
            new GeoPosition(-122.081538, 47.640846),
            new GeoPosition(-122.078634, 47.576066),
            new GeoPosition(-122.112686, 47.578559),
            new GeoPosition(-122.108727, 47.649383),
        });
        #endregion
    }

    [Fact]
    public void CreatePolygonWithHoles()
    {
        #region Snippet:CreatePolygonWithHoles
        var polygon = new GeoPolygon(new[]
        {
            // Outer ring
            new GeoLinearRing(new[]
            {
                new GeoPosition(-122.108727, 47.649383),
                new GeoPosition(-122.081538, 47.640846),
                new GeoPosition(-122.078634, 47.576066),
                new GeoPosition(-122.112686, 47.578559),
                // Last position same as first
                new GeoPosition(-122.108727, 47.649383),
            }),
            // Inner ring
            new GeoLinearRing(new[]
            {
                new GeoPosition(-122.102370, 47.607370),
                new GeoPosition(-122.083488, 47.608007),
                new GeoPosition(-122.085419, 47.597879),
                new GeoPosition(-122.107005, 47.596895),
                // Last position same as first
                new GeoPosition(-122.102370, 47.607370),
            })
        });
        #endregion
    }

    [Fact]
    public void CreateFeature()
    {
        #region Snippet:CreateFeature
        var feature = new GeoFeature(
            new GeoPoint(-122.091954, 47.607148),
            new Dictionary<string, object?>
            {
                ["name"] = "Feature 1"
            }
        );
        #endregion
    }

    [Fact]
    public void CreateFeatureCollection()
    {
        #region Snippet:CreateFeatureCollection
        var featureCollection = new GeoFeatureCollection(
            [
                new GeoFeature(
                    new GeoPoint(-122.091954, 47.607148),
                    new Dictionary<string, object?>
                    {
                        ["name"] = "Feature 1"
                    }
                ),
                new GeoFeature(
                    new GeoPoint(-122.081538, 47.640846),
                    new Dictionary<string, object?>
                    {
                        ["name"] = "Feature 2"
                    }
                )
            ]
        );
        #endregion
    }
}
