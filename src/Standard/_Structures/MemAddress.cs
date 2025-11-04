using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Standard;

public readonly unsafe struct MemAddress(void* voidPtr) : IDisposable {
    public static readonly int Size = IntPtr.Size;

    private readonly byte* _ptr = (byte*)voidPtr;
    
    public readonly ref byte this[int index] {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => ref _ptr[(uint)index];
    }

    public void Dispose() => Marshal.FreeHGlobal(new IntPtr(_ptr));
}