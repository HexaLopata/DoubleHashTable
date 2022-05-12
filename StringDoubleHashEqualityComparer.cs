using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

public class StringDoubleHashEqualityComparer : IDoubleHashCodeEqualityComparer<string>
{
    [DllImport("fnv.dll", CharSet = CharSet.Unicode)]
    public static extern int FNV(string str, int m, short p, short h0);

    public bool Equals(string? a, string? b)
    {
        return a == b;
    }

    public ushort GetFirstHashCode([DisallowNull] string obj)
    {
        return (ushort)FNV(obj, obj.Length - 1, 3517, 7919);
    }

    public byte GetSecondHashCode([DisallowNull] string obj)
    {
        return (byte)FNV(obj, obj.Length - 1, 199, 211);
    }
}