/// <summary>
/// Like float, but always positive
/// If something try to do it is negative, it will become 0
/// </summary>
public struct UFloat
{
    private float _value;

    public UFloat(float val)
    {
        if (val < 0)
            _value = 0;
        _value = val;
    }

    #region Cast

    public static implicit operator float(UFloat f)
    {
        return f._value;
    }

    public static explicit operator UFloat(float f)
    {
        if (f < 0)
           return new UFloat(0);
        return new UFloat(f);
    }

    #endregion

    #region Сomparison

    public static bool operator <(UFloat a, UFloat b)
    {
        return a._value < b._value;
    }

    public static bool operator >(UFloat a, UFloat b)
    {
        return a._value > b._value;
    }

    public static bool operator ==(UFloat a, UFloat b)
    {
        return a._value == b._value;
    }

    public static bool operator !=(UFloat a, UFloat b)
    {
        return a._value != b._value;
    }

    public static bool operator <=(UFloat a, UFloat b)
    {
        return a._value <= b._value;
    }

    public static bool operator >=(UFloat a, UFloat b)
    {
        return a._value >= b._value;
    }


    public override int GetHashCode()
    {
        return _value.GetHashCode();
    }

    public override bool Equals(object a)
    {
        return !(a is UFloat) ? false : this == (UFloat)a;
    }

    #endregion

    public override string ToString()
    {
        return _value.ToString();
    }
}