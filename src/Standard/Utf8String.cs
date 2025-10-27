using System.Runtime.InteropServices;

namespace Standard;

public readonly unsafe struct Utf8String {
    public readonly byte* Buffer;
    public readonly int Length;

    public readonly ReadOnlySpan<byte> Span => new(Buffer, Length);

    public Utf8String(byte* buffer) : this(buffer, buffer == null ? 0 : MemoryMarshal.CreateReadOnlySpanFromNullTerminated(buffer).Length) { }

    public Utf8String(byte* buffer, int length) {
        Buffer = buffer;
        Length = length;
    }

    public static implicit operator ReadOnlySpan<byte>(Utf8String memory) => memory.Span;
}