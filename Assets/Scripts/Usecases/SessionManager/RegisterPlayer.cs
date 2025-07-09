public class RegisterPlayer : ISetPlayerSession
{
    public readonly IDatabase<PlayerEntity> Database;
    private readonly string _password;

    public RegisterPlayer(IDatabase<PlayerEntity> database, string password)
    {
        Database = database;
        _password = password;
    }

    public void SetSession(string name)
    {
        PlayerEntity player = new PlayerEntity(name, _password);
        Database.Add(player);
        SessionEntity.CreateSession(player);
    }
}
