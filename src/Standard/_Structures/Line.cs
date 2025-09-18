using System.Numerics;

namespace Standard;

public struct Line(Vector2 start, Vector2 end) {
    public Vector2 Start = start;
    public Vector2 End = end;

    public float Length => Vector2.Distance(Start, End);
    public Vector2 Direction => Vector2.Normalize(End - Start);
}