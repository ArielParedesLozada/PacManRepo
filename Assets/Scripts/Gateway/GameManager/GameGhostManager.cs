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
        switch (_pacman.PacManState)
        {
            case PacManState.Dead:
                foreach (PhantomEntity ghost in _ghosts)
                {
                    ghost.Deactivate();
                }
                break;
            case PacManState.Still:
                foreach (PhantomEntity ghost in _ghosts)
                {
                    ghost.Deactivate();
                }
                break;
            case PacManState.Empowered:
                foreach (PhantomEntity ghost in _ghosts)
                {
                    ghost.Scare();
                }
                break;
            case PacManState.Alive:
                foreach (PhantomEntity ghost in _ghosts)
                {
                    ghost.Activate();
                }
                break;
            default:
                break;
        }
    }
}
