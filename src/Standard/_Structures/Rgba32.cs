using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Standard;

[StructLayout(LayoutKind.Sequential)]
public struct Rgba32 {
    private const int size = sizeof(uint);

    public byte R, G, B, A;

    public uint PackedValue {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly get => Unsafe.As<Rgba32, uint>(ref Unsafe.AsRef(in this));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => Unsafe.As<Rgba32, uint>(ref this) = value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Rgba32(byte r, byte g, byte b, byte a) => (R, G, B, A) = (r, g, b, a);
}