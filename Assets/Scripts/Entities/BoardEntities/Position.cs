using UnityEngine;
public class Position : IPosition
{
    private Vector2 _inner;

    public float X => _inner.x;
    public float Y => _inner.y;

    public Position(float x, float y)
    {
        _inner = new Vector2(x, y);
    }

    public Position(Vector2 vector)
    {
        _inner = vector;
    }

    public IPosition Add(IPosition other)
    {
        return new Position(_inner + new Vector2(other.X, other.Y));
    }

    public IPosition Subtract(IPosition other)
    {
        return new Position(_inner - new Vector2(other.X, other.Y));
    }

    public IPosition Multiply(float scalar)
    {
        return new Position(_inner * scalar);
    }

    public float SqrMagnitude()
    {
        return _inner.sqrMagnitude;
    }

    public IPosition Normalize()
    {
        return new Position(_inner.normalized);
    }

    public bool Equals(IPosition other)
    {
        return Mathf.Approximately(X, other.X) && Mathf.Approximately(Y, other.Y);
    }

    public override string ToString()
    {
        return _inner.ToString();
    }

    public Vector2 ToVector2()
    {
        return _inner;
    }

    public float DistanceTo(IPosition other)
    {
        return (_inner - new Vector2(other.X, other.Y)).magnitude;
    }

    public IPosition Zero()
    {
        return new Position(0, 0);
    }

}
