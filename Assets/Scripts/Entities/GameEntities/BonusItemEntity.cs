using System;
public class BonusItemEntity
{
    public float LifeExpectancy { get; set; }
    public float CurrentLifeTime { get; set; }
    public int Value { get; set; }
    public BonusItemEntity(int val)
    {
        Value = val;
        LifeExpectancy = new Random().Next(5, 10);
    }
}
