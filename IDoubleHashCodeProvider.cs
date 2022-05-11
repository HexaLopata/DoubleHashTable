using System.Diagnostics.CodeAnalysis;

public interface IDoubleHashCodeEqualityComparer<T>
{
    bool Equals(T? a, T? b);
    ushort GetFirstHashCode([DisallowNull] T obj);
    byte GetSecondHashCode([DisallowNull] T obj);
}