public class LoginPlayer : ISetPlayerSession
{
    public readonly IDatabase<PlayerEntity> Database;
    public LoginPlayer(IDatabase<PlayerEntity> database)
    {
        Database = database;
    }
    public void SetSession(string name, string clave)
    {
        PlayerEntity current = this.Database.FindUser(name, clave);
        if (current == null)
        {
            throw new System.Exception("Error Skibidi: Este jugador no esta registrado. Problema en UseCases/LoginPlayer");
        }
        SessionEntity.CreateSession(current);
    }

}
