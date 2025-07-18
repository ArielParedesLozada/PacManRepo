public class LoseGame
{
    
    public bool IsOver(PacmanEntity pacman, ISubjectGame _game, IDestroyPlayerSession destroyer)
    {
        if (pacman.PacManState != PacManState.Dead)
        {
            return false;
        }
        var saver = new LevelSaver();
        saver.Save(_game.Score, _game.Level);
        destroyer.DestroySession();
        return true;
    }
}
