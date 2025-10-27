using System.Numerics;
using System.Runtime.CompilerServices;

namespace Standard;

public struct Rectangle {
    public static readonly Rectangle Default = new(0f, 0f, 1f, 1f);
    public static readonly Rectangle Empty = new(0f, 0f, 0f, 0f);

    public float X;
    public float Y;
    public float Width;
    public float Height;

    public Vector2 Location {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly get => new(X, Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => (X, Y) = (value.X, value.Y);
    }

    public Rectangle(float x, float y, float width, float height) {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    public Rectangle(Vector2 position, Vector2 size) : this(position.X, position.Y, size.X, size.Y) { }

    public float Left {
        readonly get => X;
        set => X = value;
    }
    public float Top {
        readonly get => Y;
        set => Y = value;
    }

    public float Right {
        readonly get => X + Width;
        set => X = value - Width;
    }

    public float Bottom {
        readonly get => Y + Height;
        set => Y = value - Height;
    }

    public Vector2 TopLeft {
        readonly get => new(Left, Top);
        set {
            Left = value.X;
            Top = value.Y;
        }
    }

    public Vector2 TopRight {
        readonly get => new(Right, Top);
        set {
            Right = value.X;
            Top = value.Y;
        }
    }

    public Vector2 BottomLeft {
        readonly get => new(Left, Bottom);
        set {
            Left = value.X;
            Bottom = value.Y;
        }
    }
    public Vector2 BottomRight {
        readonly get => new(Right, Bottom);
        set {
            Right = value.X;
            Bottom = value.Y;
        }
    }
    public Vector2 Position {
        readonly get => TopLeft;
        set => TopLeft = value;
    }
    public Vector2 Size {
        readonly get => new(Width, Height);
        set {
            Width = value.X;
            Height = value.Y;
        }
    }
    public Vector4 Points {
        readonly get => new(X, Y, X + Width, Y + Height);
        set {
            X = value.X;
            Y = value.Y;
            Width = value.Z - X;
            Height = value.W - Y;
        }
    }

    public Vector2 Center {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new Vector2(this.X + (this.Width / 2), this.Y + (this.Height / 2));
    }

    public bool Intersects(Rectangle value) {
        return (value.Left < Right &&
                    Left < value.Right &&
                    value.Top < Bottom &&
                    Top < value.Bottom);
    }

    public Rectangle Moved(Vector2 offset) {
        return new Rectangle(X + offset.X, Y + offset.Y, Width, Height);
    }

    public bool Contains(Vector2 point, bool inclusive = false) {
        if(inclusive) {
            return point.X > X && point.X < X + Width && point.Y > Y && point.Y < Y + Height;
        }

        return point.X >= X && point.X <= X + Width && point.Y >= Y && point.Y <= Y + Height;
    }

    public bool Contains(Vector2Int point, bool inclusive = false) {
        if(inclusive) {
            return point.X > X && point.X < X + Width && point.Y > Y && point.Y < Y + Height;
        }

        return point.X >= X && point.X <= X + Width && point.Y >= Y && point.Y <= Y + Height;
    }

    public static Rectangle Union(Rectangle rect1, Rectangle rect2) {
        float x = Math.Min(rect1.X, rect2.X);
        float y = Math.Min(rect1.Y, rect2.Y);

        float right = Math.Max(rect1.Right, rect2.Right);
        float bottom = Math.Max(rect1.Bottom, rect2.Bottom);

        float width = right - x;
        float height = bottom - y;

        return new Rectangle(x, y, width, height);
    }
}