using System.Numerics;

namespace Standard;

public struct Circle {
    public Vector2 Center;
    public float Radius;

    public Circle(Vector2 center, float radius) {
        Center = center;
        Radius = radius;
    }

    public bool Colliding(Circle otherCircle) {
        float dist = Vector2.Distance(Center, otherCircle.Center);
        return dist <= Radius + otherCircle.Radius;
    }
}