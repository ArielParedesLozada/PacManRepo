public class RegisterPlayer : ISetPlayerSession
{
    public readonly IDatabase<PlayerEntity> Database;
    public RegisterPlayer(IDatabase<PlayerEntity> database)
    {
        Database = database;
    }
    public void SetSession(string name, string clave)
    {
        PlayerEntity player = new PlayerEntity(name, clave);
        Database.Add(player);
        SessionEntity.CreateSession(player);
    }

}
