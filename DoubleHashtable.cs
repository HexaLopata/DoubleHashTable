public class DoubleHashtable<TKey, TValue>
{
    private IDoubleHashCodeEqualityComparer<TKey> _equalityComparer;
    private KeyValuePair<TKey, TValue>?[,] _data = new KeyValuePair<TKey, TValue>?[65536, 256];

    public TValue this[TKey key]
    {
        get => GetValue(key);
        set => UpdateValue(key, value);
    }

    public DoubleHashtable(IDoubleHashCodeEqualityComparer<TKey> equalityComparer)
    {
        _equalityComparer = equalityComparer;
    }

    public void Add(TKey key, TValue value)
    {
        var indexes = GetElementIndex(key);

        if (_data[indexes.x, indexes.y].HasValue)
        {
            if (_equalityComparer.Equals(_data[indexes.x, indexes.y]!.Value.Key, key))
                throw new ArgumentException("The element with the same key already exists");
            else
                throw new ArgumentException("Hash table has an unsolvable collision");
        }

        _data[indexes.x, indexes.y] = new KeyValuePair<TKey, TValue>(key, value);
    }


    public void RemoveValue(TKey key)
    {
        var indexes = GetElementIndex(key);
        
        if (!_data[indexes.x, indexes.y].HasValue)
            throw new ArgumentException($"Element with key {key} was not found");

        _data[indexes.x, indexes.y] = null;
    }

    public bool ContainsKey(TKey key)
    {
        var indexes = GetElementIndex(key);
        return _data[indexes.x, indexes.y].HasValue;
    }

    private void UpdateValue(TKey key, TValue value)
    {
        var indexes = GetElementIndex(key);

        if (!_data[indexes.x, indexes.y].HasValue)
            throw new ArgumentException($"Element with key {key} was not found");

        _data[indexes.x, indexes.y] = new KeyValuePair<TKey, TValue>(key, value);
    }

    private TValue GetValue(TKey key)
    {
        var indexes = GetElementIndex(key);

        if (!_data[indexes.x, indexes.y].HasValue)
            throw new ArgumentException($"Element with key {key} was not found");

        return _data[indexes.x, indexes.y]!.Value.Value;
    }

    private (int x, int y) GetElementIndex(TKey key)
    {
        ArgumentNullException.ThrowIfNull(key);

        var firstHashCode = _equalityComparer.GetFirstHashCode(key);
        var secondHashCode = _equalityComparer.GetSecondHashCode(key);

        return (firstHashCode, secondHashCode);
    }
}