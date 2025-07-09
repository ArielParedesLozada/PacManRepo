public interface IDatabase<T>
{
    public void Add(T element);
    public void Update(T element);
    public void Delete(T element);
    public T FindUser(string nombre, string clave);
}
