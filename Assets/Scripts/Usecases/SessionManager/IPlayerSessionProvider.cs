public interface IPlayerSessionProvider
{
    public ISetPlayerSession GetPlayerSession(string player, string password, bool isNew);
}
