using Zenject;

public class PlayerSessionProvider : IPlayerSessionProvider
{

    private readonly IDatabase<PlayerEntity> _database;
    private readonly DiContainer _container;

    public PlayerSessionProvider(
        IDatabase<PlayerEntity> database,
        DiContainer container
    )
    {
        _database = database;
        _container = container;
    }
    public ISetPlayerSession GetPlayerSession(string player, string clave)
    {
        var found = _database.FindUser(player, clave);
        if (found != null)
        {
            return _container.Instantiate<LoginPlayer>();
        }
        else
        {
            return _container.Instantiate<RegisterPlayer>();
        }
    }

}
