public class LoginPlayer : ISetPlayerSession
{
    public readonly IDatabase<PlayerEntity> Database;
    private readonly string _password;

    public LoginPlayer(IDatabase<PlayerEntity> database, string password)
    {
        Database = database;
        _password = password;
    }

    public void SetSession(string name)
    {
        PlayerEntity current = this.Database.FindByName(name);
        if (current == null)
        {
            throw new System.Exception("Este jugador no está registrado.");
        }

        if (current.Password != _password)
        {
            throw new System.Exception("Contraseña incorrecta.");
        }

        SessionEntity.CreateSession(current);
    }
}