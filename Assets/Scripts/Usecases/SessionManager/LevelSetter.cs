public class LevelSetter
{
    public int GetLevel()
    {
        return SessionEntity.GetInstance().CurrentPlayer.LastLevel;
    }
}
