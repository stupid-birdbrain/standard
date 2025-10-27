using System.Numerics;

namespace Standard;

public struct Line(Vector2 start, Vector2 end) {
    public Vector2 Start = start;
    public Vector2 End = end;

    public ReadOnlySpan<Vector2> GetPoints(int quantity) {
        var scale = 1f / quantity;
        var points = new Vector2[quantity];

        for(var i = 0; i < quantity; i++)
            points[i] = Vector2.Lerp(Start, End, scale * i);

        return points;
    }

    public bool Intersects(Rectangle rectangle) {
        var left = this.Intersects(new Line(rectangle.TopLeft, rectangle.BottomLeft));
        var right = this.Intersects(new Line(rectangle.TopRight, rectangle.BottomRight));
        var top = this.Intersects(new Line(rectangle.TopLeft, rectangle.TopRight));
        var bottom = this.Intersects(new Line(rectangle.BottomLeft, rectangle.BottomRight));

        return left || right || top || bottom;
    }

    public bool Intersects(Line line) {
        var uA = ((line.End.X - line.Start.X) * (this.Start.Y - line.Start.Y) - (line.End.Y - line.Start.Y) * (this.Start.X - line.Start.X)) / ((line.End.Y - line.Start.Y) * (this.End.X - this.Start.X) - (line.End.X - line.Start.X) * (this.End.Y - this.Start.Y));

        var uB = ((this.End.X - this.Start.X) * (this.Start.Y - line.Start.Y) - (this.End.Y - this.Start.Y) * (this.Start.X - line.Start.X)) / ((line.End.Y - line.Start.Y) * (this.End.X - this.Start.X) - (line.End.X - line.Start.X) * (this.End.Y - this.Start.Y));

        return uA >= 0 && uA <= 1 && uB >= 0 && uB <= 1;
    }

    public Vector2 IntersectionPoint(Line line) {
        var uA = ((line.End.X - line.Start.X) * (this.Start.Y - line.Start.Y) - (line.End.Y - line.Start.Y) * (this.Start.X - line.Start.X)) / ((line.End.Y - line.Start.Y) * (this.End.X - this.Start.X) - (line.End.X - line.Start.X) * (this.End.Y - this.Start.Y));

        var uB = ((this.End.X - this.Start.X) * (this.Start.Y - line.Start.Y) - (this.End.Y - this.Start.Y) * (this.Start.X - line.Start.X)) / ((line.End.Y - line.Start.Y) * (this.End.X - this.Start.X) - (line.End.X - line.Start.X) * (this.End.Y - this.Start.Y));

        var intersectionX = this.Start.X + (uA * (this.End.X - this.Start.X));
        var intersectionY = this.Start.Y + (uA * (this.End.Y - this.Start.Y));

        return new Vector2(intersectionX, intersectionY);
    }
}