using Zenject;

public class PlayerSessionProvider : IPlayerSessionProvider
{
    private readonly IDatabase<PlayerEntity> _database;

    public PlayerSessionProvider(IDatabase<PlayerEntity> database)
    {
        _database = database;
    }

    public ISetPlayerSession GetPlayerSession(string player, string password, bool isNew)
    {
        if (isNew)
        {
            return new RegisterPlayer(_database, password);
        }
        else
        {
            return new LoginPlayer(_database, password);
        }
    }
}
