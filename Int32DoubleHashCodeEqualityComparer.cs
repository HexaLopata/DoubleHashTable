using System.Diagnostics.CodeAnalysis;

public class UInt32DoubleHashCodeEqualityComparer : IDoubleHashCodeEqualityComparer<uint>
{
    public bool Equals(uint a, uint b)
    {
        return a.Equals(b);
    }

    public ushort GetFirstHashCode([DisallowNull] uint obj)
    {
        return (ushort)(obj >> 8);
    }

    public byte GetSecondHashCode([DisallowNull] uint obj)
    {
        return (byte)(obj & 255);
    }
}