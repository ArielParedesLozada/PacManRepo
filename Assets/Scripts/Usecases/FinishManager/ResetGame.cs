public class ResetGame
{
    public const int _maximumScore = 2700;
    public bool ResetGameBoard(ISubjectGame game)
    {
        if (game.Score < _maximumScore)
        {
            return false;
        }
        int currentLevel = new LevelSetter().GetLevel();
        int nextLevel = currentLevel + 1;
        int nextScore = game.Score + _maximumScore * game.Level;
        new LevelSaver().Save(nextScore, nextLevel);
        return true;
    }
}
