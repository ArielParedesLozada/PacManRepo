public class PasswordChange
{
    public readonly IDatabase<PlayerEntity> _database;

    public PasswordChange(IDatabase<PlayerEntity> database)
    {
        _database = database;
    }
    public void ChangePassword(string nuevaClave)
    {
        PlayerEntity player = SessionEntity.GetInstance().CurrentPlayer;
        string prevClave = player.Clave;
        if (prevClave.Equals(nuevaClave))
        {
            throw new System.Exception("Debe cambiar la clave porque ya la ingreso");
        }
        player.ChangePassword(nuevaClave);
        _database.Update(player);
    }
}
