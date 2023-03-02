using System;

[Serializable]
public class Stat<T>
{
    public event Action<T> OnValueChanged;

    T _value;
    public T Value
    {
        get => _value;
        set
        {
            _value = value;
            OnValueChanged?.Invoke(value);
        }
    }
}