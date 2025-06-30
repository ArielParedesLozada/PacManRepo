public class GameGhostManager
{
    private PacmanEntity _pacman;
    private PhantomEntity[] _ghosts;
    public GameGhostManager(PacmanEntity pacman, PhantomEntity[] phantoms)
    {
        _pacman = pacman;
        _ghosts = phantoms;
    }
    public void Notify()
    {
        if (_ghosts == null)
        {
            return;
        }
        foreach (PhantomEntity ghost in _ghosts)
        {
            if (ghost == null)
            {
                continue;
            }
            switch (_pacman.PacManState)
            {
                case PacManState.Dead:
                    ghost.Deactivate();
                    break;
                case PacManState.Still:
                    ghost.Deactivate();
                    break;
                case PacManState.Empowered:
                    ghost.Scare();
                    break;
                case PacManState.Alive:
                    ghost.Revive();
                    break;
                default:
                    break;
            }
        }
    }
}
