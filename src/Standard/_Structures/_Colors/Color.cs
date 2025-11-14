using System.Numerics;
using System.Runtime.InteropServices;

namespace Standard;

/// <summary> Immutable 32bit color value in RGBA format. </summary>
[StructLayout(LayoutKind.Explicit, Size = sizeof(uint))]
public readonly struct Color : IEquatable<Color> {
    [field: FieldOffset(0)]
    private readonly uint _packedValue;
    /// <summary> Gets or sets the red component. </summary>
    [FieldOffset(0)] // Red at the lowest address
    public readonly byte R;

    /// <summary> Gets or sets the green component. </summary>
    [FieldOffset(1)] // Green next
    public readonly byte G;

    /// <summary> Gets or sets the blue component. </summary>
    [FieldOffset(2)] // Blue next
    public readonly byte B;

    /// <summary> Gets or sets the alpha component. </summary>
    [FieldOffset(3)] // Alpha at the highest address
    public readonly byte A;

    /// <summary>   Gets or sets packed value of this <see cref="Color"/>.  </summary>
    public readonly uint PackedValue => _packedValue;

    /// <summary>
    /// Constructs an RGBA color from a packed value.
    /// The value is a 32-bit unsigned integer, with R in the least significant octet.
    /// </summary>
    /// <param name="packedValue">The packed value.</param>
    public Color(uint packedValue) {
        _packedValue = packedValue;
    }

    /// <summary>
    /// Constructs an RGBA color from the XYZW unit length components of a vector.
    /// </summary>
    /// <param name="color">A <see cref="Vector4"/> representing color.</param>
    public Color(Vector4 color)
        : this((int)(color.X * 255), (int)(color.Y * 255), (int)(color.Z * 255), (int)(color.W * 255)) {
    }

    /// <summary>
    /// Constructs an RGBA color from the XYZ unit length components of a vector. Alpha value will be opaque.
    /// </summary>
    /// <param name="color">A <see cref="Vector3"/> representing color.</param>
    public Color(Vector3 color)
        : this((int)(color.X * 255), (int)(color.Y * 255), (int)(color.Z * 255)) {
    }

    /// <summary>
    /// Constructs an RGBA color from a <see cref="Color"/> and an alpha value.
    /// </summary>
    /// <param name="color">A <see cref="Color"/> for RGB values of new <see cref="Color"/> instance.</param>
    /// <param name="alpha">The alpha component value from 0 to 255.</param>
    public Color(Color color, int alpha) {
        if((alpha & 0xFFFFFF00) != 0) {
            var clampedA = (uint)Math.Clamp(alpha, Byte.MinValue, Byte.MaxValue);
            _packedValue = (color._packedValue & 0x00FFFFFF) | (clampedA << 24);
        }
        else {
            _packedValue = (color._packedValue & 0x00FFFFFF) | ((uint)alpha << 24);
        }
    }

    /// <summary>
    /// Constructs an RGBA color from color and alpha value.
    /// </summary>
    /// <param name="color">A <see cref="Color"/> for RGB values of new <see cref="Color"/> instance.</param>
    /// <param name="alpha">Alpha component value from 0.0f to 1.0f.</param>
    public Color(Color color, float alpha)
        : this(color, (int)(alpha * 255)) {
    }

    /// <summary>
    /// Constructs an RGBA color from scalars representing red, green and blue values. Alpha value will be opaque.
    /// </summary>
    /// <param name="r">Red component value from 0.0f to 1.0f.</param>
    /// <param name="g">Green component value from 0.0f to 1.0f.</param>
    /// <param name="b">Blue component value from 0.0f to 1.0f.</param>
    public Color(float r, float g, float b)
        : this((int)(r * 255), (int)(g * 255), (int)(b * 255)) {
    }

    /// <summary>
    /// Constructs an RGBA color from scalars representing red, green, blue and alpha values.
    /// </summary>
    /// <param name="r">Red component value from 0.0f to 1.0f.</param>
    /// <param name="g">Green component value from 0.0f to 1.0f.</param>
    /// <param name="b">Blue component value from 0.0f to 1.0f.</param>
    /// <param name="alpha">Alpha component value from 0.0f to 1.0f.</param>
    public Color(float r, float g, float b, float alpha)
        : this((int)(r * 255), (int)(g * 255), (int)(b * 255), (int)(alpha * 255)) {
    }

    /// <summary>
    /// Constructs an RGBA color from scalars representing red, green and blue values. Alpha value will be opaque.
    /// </summary>
    /// <param name="r">Red component value from 0 to 255.</param>
    /// <param name="g">Green component value from 0 to 255.</param>
    /// <param name="b">Blue component value from 0 to 255.</param>
    public Color(int r, int g, int b) {
        _packedValue = 0xFF000000; // A = 255
        if(((r | g | b) & 0xFFFFFF00) != 0) {
            var clampedR = (uint)Math.Clamp(r, Byte.MinValue, Byte.MaxValue);
            var clampedG = (uint)Math.Clamp(g, Byte.MinValue, Byte.MaxValue);
            var clampedB = (uint)Math.Clamp(b, Byte.MinValue, Byte.MaxValue);

            _packedValue |= (clampedB << 16) | (clampedG << 8) | (clampedR);
        }
        else {
            _packedValue |= ((uint)b << 16) | ((uint)g << 8) | ((uint)r);
        }
    }

    /// <summary>
    /// Constructs an RGBA color from scalars representing red, green, blue and alpha values.
    /// </summary>
    /// <param name="r">Red component value from 0 to 255.</param>
    /// <param name="g">Green component value from 0 to 255.</param>
    /// <param name="b">Blue component value from 0 to 255.</param>
    /// <param name="alpha">Alpha component value from 0 to 255.</param>
    public Color(int r, int g, int b, int alpha) {
        if(((r | g | b | alpha) & 0xFFFFFF00) != 0) {
            var clampedR = (uint)Math.Clamp(r, Byte.MinValue, Byte.MaxValue);
            var clampedG = (uint)Math.Clamp(g, Byte.MinValue, Byte.MaxValue);
            var clampedB = (uint)Math.Clamp(b, Byte.MinValue, Byte.MaxValue);
            var clampedA = (uint)Math.Clamp(alpha, Byte.MinValue, Byte.MaxValue);

            _packedValue = (clampedA << 24) | (clampedB << 16) | (clampedG << 8) | (clampedR);
        }
        else {
            _packedValue = ((uint)alpha << 24) | ((uint)b << 16) | ((uint)g << 8) | ((uint)r);
        }
    }

    /// <summary>
    /// Constructs an RGBA color from scalars representing red, green, blue and alpha values.
    /// </summary>
    /// <remarks>
    /// This overload sets the values directly without clamping, and may therefore be faster than the other overloads.
    /// </remarks>
    /// <param name="r"></param>
    /// <param name="g"></param>
    /// <param name="b"></param>
    /// <param name="alpha"></param>
    public Color(byte r, byte g, byte b, byte alpha) {
        _packedValue = ((uint)alpha << 24) | ((uint)b << 16) | ((uint)g << 8) | (r);
    }

    /// <summary>
    /// Converts this color to a vector of floats.
    /// </summary>
    public Vector4 ToVector4() => new Vector4(R / 255f, G / 255f, B / 255f, A / 255f);

    public bool Equals(Color other) {
        return this._packedValue == other._packedValue;
    }

    public override bool Equals(object obj) {
        return obj is Color other && Equals(other);
    }

    public override int GetHashCode() {
        return _packedValue.GetHashCode();
    }

    public static bool operator ==(Color left, Color right) {
        return left.Equals(right);
    }

    public static bool operator !=(Color left, Color right) {
        return !left.Equals(right);
    }
}