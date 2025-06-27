public interface IPosition
{
    float X { get; }
    float Y { get; }

    IPosition Add(IPosition other);
    IPosition Subtract(IPosition other);
    IPosition Multiply(float scalar);
    float SqrMagnitude();
    IPosition Normalize();
    float DistanceTo(IPosition other);
    bool Equals(IPosition other);
    string ToString();
}
