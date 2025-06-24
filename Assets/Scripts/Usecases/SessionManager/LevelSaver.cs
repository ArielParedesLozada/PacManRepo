public class LevelSaver
{
    public void Save(int score, int level)
    {
        PlayerEntity player = SessionEntity.GetInstance().CurrentPlayer;
        player.UpdateLevel(level);
        player.UpdateScore(score);
    }
}
