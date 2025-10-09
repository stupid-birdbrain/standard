using System.Collections;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Standard;

[StructLayout(LayoutKind.Sequential)]
public struct Bitset8 : IEnumerable<int> {
    public const int CAPACITY = 8;

    private byte _bits;

    public static Bitset8 Zero => default;

    public bool IsZero => _bits == 0;

    public Bitset8(byte initialValue) {
        _bits = initialValue;
    }

    public bool this[int index] {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly get {
            if ((uint)index >= CAPACITY) throw new ArgumentOutOfRangeException(nameof(index), "index must be between 0 and 7.");
            return (_bits & (1 << index)) != 0;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set {
            if ((uint)index >= CAPACITY) throw new ArgumentOutOfRangeException(nameof(index), "index must be between 0 and 7.");
            if (value) {
                _bits |= (byte)(1 << index);
            } else {
                _bits &= (byte)~(1 << index);
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Set(int index) {
        if ((uint)index >= CAPACITY) throw new ArgumentOutOfRangeException(nameof(index));
        _bits |= (byte)(1 << index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Clear(int index) {
        if ((uint)index >= CAPACITY) throw new ArgumentOutOfRangeException(nameof(index));
        _bits &= (byte)~(1 << index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool IsSet(int index) {
        if ((uint)index >= CAPACITY) throw new ArgumentOutOfRangeException(nameof(index));
        return (_bits & (1 << index)) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool Contains(in Bitset8 other) {
        return (_bits & other._bits) == other._bits;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool AndAny(in Bitset8 other) {
        return (_bits & other._bits) != 0;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int PopCount() {
        return BitOperations.PopCount(_bits);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int FirstSetBit() {
        if (IsZero) return CAPACITY;
        return BitOperations.TrailingZeroCount(_bits);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetAll() {
        _bits = byte.MaxValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ClearAll() {
        _bits = 0;
    }

    public static Bitset8 operator &(Bitset8 left, Bitset8 right) => new((byte)(left._bits & right._bits));
    public static Bitset8 operator |(Bitset8 left, Bitset8 right) => new((byte)(left._bits | right._bits));
    public static Bitset8 operator ^(Bitset8 left, Bitset8 right) => new((byte)(left._bits ^ right._bits));
    public static Bitset8 operator ~(Bitset8 value) => new((byte)(~value._bits));
    public static bool operator ==(Bitset8 left, Bitset8 right) => left._bits == right._bits;
    public static bool operator !=(Bitset8 left, Bitset8 right) => left._bits != right._bits;

    public override readonly bool Equals(object? obj) => obj is Bitset8 other && this == other;
    public override readonly int GetHashCode() => _bits.GetHashCode();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator byte(Bitset8 bs) => bs._bits;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Bitset8(byte b) => new(b);

    public readonly Enumerator GetEnumerator() => new(this);
    IEnumerator<int> IEnumerable<int>.GetEnumerator() => GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    
    public override readonly string ToString() {
        Span<char> buffer = stackalloc char[CAPACITY];
        int length = ToString(buffer);
        return buffer.Slice(0, length).ToString();
    }
    
    public readonly int ToString(Span<char> buffer) {
        int length = 0;
        bool first = true;

        foreach (int index in this) {
            if (!first) {
                if (length + 2 > buffer.Length) break;
                buffer[length++] = ',';
                buffer[length++] = ' ';
            }

            if (!index.TryFormat(buffer.Slice(length), out int charsWritten)) {
                break;
            }
            length += charsWritten;
            first = false;
        }

        return length;
    }
    
    public struct Enumerator : IEnumerator<int> {
        private Bitset8 _mask;
        public Enumerator(in Bitset8 mask) {
            _mask = mask;
            Current = -1;
        }
        public int Current { get; private set; }
        object IEnumerator.Current => Current;
        public bool MoveNext() {
            int index = _mask.FirstSetBit();
            if (index >= CAPACITY) {
                Current = -1;
                return false;
            }
            Current = index;
            _mask.Clear(index);
            return true;
        }
        
        public void Reset() => throw new NotSupportedException();
        public readonly void Dispose() { }
    }
}