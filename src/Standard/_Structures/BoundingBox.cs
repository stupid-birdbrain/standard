using System.Numerics;

namespace Standard;

/// <summary>
///     An axis-aligned bounding box.
/// </summary>
public struct BoundingBox(Vector3 min, Vector3 max) {
    public Vector3 Min = min;
    public Vector3 Max = max;

    /// <summary>
    ///     Gets the 8 corners of the bounding box.
    /// </summary>
    public ReadOnlySpan<Vector3> GetCorners() {
        Span<Vector3> corners = new Vector3[8];
        corners[0] = new Vector3(Min.X, Min.Y, Min.Z);
        corners[1] = new Vector3(Max.X, Min.Y, Min.Z);
        corners[2] = new Vector3(Max.X, Max.Y, Min.Z);
        corners[3] = new Vector3(Min.X, Max.Y, Min.Z);
        corners[4] = new Vector3(Min.X, Min.Y, Max.Z);
        corners[5] = new Vector3(Max.X, Min.Y, Max.Z);
        corners[6] = new Vector3(Max.X, Max.Y, Max.Z);
        corners[7] = new Vector3(Min.X, Max.Y, Max.Z);
        return corners;
    }

    /// <summary>
    ///     Checks if the bounding box intersects or is inside a plane.
    /// </summary>
    public bool Intersects(in Plane plane) {
        Vector3 pVertex;
        Vector3 nVertex;

        // x
        if (plane.Normal.X >= 0) {
            pVertex.X = Max.X;
            nVertex.X = Min.X;
        } else {
            pVertex.X = Min.X;
            nVertex.X = Max.X;
        }

        // y
        if (plane.Normal.Y >= 0) {
            pVertex.Y = Max.Y;
            nVertex.Y = Min.Y;
        } else {
            pVertex.Y = Min.Y;
            nVertex.Y = Max.Y;
        }

        // z
        if (plane.Normal.Z >= 0) {
            pVertex.Z = Max.Z;
            nVertex.Z = Min.Z;
        } else {
            pVertex.Z = Min.Z;
            nVertex.Z = Max.Z;
        }

        if (Plane.DotCoordinate(plane, pVertex) < 0) {
            return false;
        }

        // if n vertex is infront of the plane, the entire box is infront of the plane
        return true;
    }
    
    /// <summary>
    ///     Checks if this bounding box intersects another bounding box.
    /// </summary>
    public bool Intersects(in BoundingBox other) {
        if (this.Max.X < other.Min.X || this.Min.X > other.Max.X) {
            return false;
        }
        if (this.Max.Y < other.Min.Y || this.Min.Y > other.Max.Y) {
            return false;
        }
        if (this.Max.Z < other.Min.Z || this.Min.Z > other.Max.Z) {
            return false;
        }

        return true;
    }
}