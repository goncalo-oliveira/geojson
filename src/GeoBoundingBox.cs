// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// Copyright (c) Goncalo Oliveira. All rights reserved.
// Licensed under the MIT License.

namespace Faactory.Types.GeoJson;

/// <summary>
/// Represents information about the coordinate range of the <see cref="GeoObject"/>.
/// </summary>
public sealed class GeoBoundingBox : IEquatable<GeoBoundingBox>
{
    /// <summary>
    /// The westmost value of <see cref="GeoObject"/> coordinates.
    /// </summary>
    public double West { get; }

    /// <summary>
    /// The southmost value of <see cref="GeoObject"/> coordinates.
    /// </summary>
    public double South { get; }

    /// <summary>
    /// The eastmost value of <see cref="GeoObject"/> coordinates.
    /// </summary>
    public double East { get; }

    /// <summary>
    /// The northmost value of <see cref="GeoObject"/> coordinates.
    /// </summary>
    public double North { get; }

    /// <summary>
    /// The minimum altitude value of <see cref="GeoObject"/> coordinates.
    /// </summary>
    public double? MinAltitude { get; }

    /// <summary>
    /// The maximum altitude value of <see cref="GeoObject"/> coordinates.
    /// </summary>
    public double? MaxAltitude { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="GeoBoundingBox"/>.
    /// </summary>
    public GeoBoundingBox(double west, double south, double east, double north) : this(west, south, east, north, null, null)
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="GeoBoundingBox"/>.
    /// </summary>
    public GeoBoundingBox(double west, double south, double east, double north, double? minAltitude, double? maxAltitude)
    {
        West = west;
        South = south;
        East = east;
        North = north;
        MinAltitude = minAltitude;
        MaxAltitude = maxAltitude;
    }

    /// <inheritdoc />
    public bool Equals(GeoBoundingBox? other)
    {
        if (other == null)
        {
            return false;
        }
        return West.Equals(other.West) &&
                South.Equals(other.South) &&
                East.Equals(other.East) &&
                North.Equals(other.North) &&
                Nullable.Equals(MinAltitude, other.MinAltitude) &&
                Nullable.Equals(MaxAltitude, other.MaxAltitude);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is GeoBoundingBox other && Equals(other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCodeBuilder.Combine(West, South, East, North, MinAltitude, MaxAltitude);
    }

    /// <summary>
    /// Gets the component of the <see cref="GeoBoundingBox"/> based on its index.
    /// </summary>
    /// <param name="index">The index of the bounding box component.</param>
    public double this[int index]
    {
        get
        {
            if (MinAltitude is double minAltitude &&
                MaxAltitude is double maxAltitude)
            {
                return index switch
                {
                    0 => West,
                    1 => South,
                    2 => minAltitude,
                    3 => East,
                    4 => North,
                    5 => maxAltitude,
                    _ => throw new IndexOutOfRangeException()
                };
            }

            return index switch
            {
                0 => West,
                1 => South,
                2 => East,
                3 => North,
                _ => throw new IndexOutOfRangeException()
            };
        }
    }

    /// <inheritdoc />
    public override string ToString()
    {
        if (MinAltitude is double minAltitude &&
            MaxAltitude is double maxAltitude)
        {
            return $"[{West}, {South}, {minAltitude}, {East}, {North}, {maxAltitude}]";
        }

        return $"[{West}, {South}, {East}, {North}]";
    }

    /// <summary>
    /// Determines whether the specified position is contained within the bounding box.
    /// </summary>
    /// <param name="position">The <see cref="GeoPosition"/> containing the coordinates.</param>
    /// <returns><see langword="true"/> if the coordinates are contained within the bounding box, <see langword="false"/> otherwise.</returns>
    public bool Contains( GeoPosition position )
        => Contains( position.Latitude, position.Longitude );

    /// <summary>
    /// Determines whether the specified coordinates are contained within the bounding box.
    /// </summary>
    /// <param name="latitude">The latitude of the coordinates.</param>
    /// <param name="longitude">The longitude of the coordinates.</param>
    /// <returns><see langword="true"/> if the coordinates are contained within the bounding box, <see langword="false"/> otherwise.</returns>
    public bool Contains( double latitude, double longitude )
    {
        if ( longitude < West )
        {
            return false;
        }

        if ( longitude > East )
        {
            return false;
        }

        if ( latitude < South )
        {
            return false;
        }

        if ( latitude > North )
        {
            return false;
        }

        return true;
    }
}
