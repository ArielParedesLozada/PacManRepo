public class ChangePasswordAction
{
    private readonly SQLitePlayerDatabase _database;

    public ChangePasswordAction(SQLitePlayerDatabase database)
    {
        _database = database;
    }

    public bool TryChangePassword(string nombre, string nuevaPassword, out string error)
    {
        var player = _database.FindByName(nombre);
        if (player == null)
        {
            error = "Jugador no encontrado.";
            return false;
        }

        if (!player.CanChangePassword(nuevaPassword))
        {
            error = "Ya usaste esa contraseña antes.";
            return false;
        }

        if (_database.IsPasswordUsedByAnotherPlayer(nombre, nuevaPassword))
        {
            error = "Esa contraseña ya la usa otro jugador.";
            return false;
        }

        player.ChangePassword(nuevaPassword);
        _database.UpdatePassword(nombre, nuevaPassword);
        error = null;
        return true;
    }
}
