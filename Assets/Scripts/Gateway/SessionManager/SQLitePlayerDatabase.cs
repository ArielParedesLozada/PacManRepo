using System.Data;
using UnityEngine;
using Mono.Data.Sqlite;

public class SQLitePlayerDatabase : IDatabase<PlayerEntity>
{
    private string _dbPath;
    private static SQLitePlayerDatabase _instance;

    private SQLitePlayerDatabase(string databasePath)
    {
        _dbPath = $"URI=file:{databasePath}";
        InitDatabase();
    }

    public static SQLitePlayerDatabase GetInstance(string databaseFileName = "players.sqlite")
    {
        if (_instance == null)
        {
            string dbFullPath = System.IO.Path.Combine(Application.persistentDataPath, databaseFileName);
            _instance = new SQLitePlayerDatabase(dbFullPath);
        }
        return _instance;
    }

    private void InitDatabase()
    {
        using (var connection = new SqliteConnection(_dbPath))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText =
                    @"CREATE TABLE IF NOT EXISTS players (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        nombre TEXT UNIQUE NOT NULL,
                        clave TEXT NOT NULL,
                        max_score INTEGER,
                        last_score INTEGER,
                        max_level INTEGER,
                        last_level INTEGER
                    );";
                command.ExecuteNonQuery();
            }
        }
    }

    public void Add(PlayerEntity element)
    {
        using (var connection = new SqliteConnection(_dbPath))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"INSERT INTO players (nombre, clave, max_score, last_score, max_level, last_level)
                                        VALUES (@nombre, @clave, @max_score, @last_score, @max_level, @last_level);";
                command.Parameters.AddWithValue("@nombre", element.Nombre);
                command.Parameters.AddWithValue("@clave", element.Clave);
                command.Parameters.AddWithValue("@max_score", element.MaxScore);
                command.Parameters.AddWithValue("@last_score", element.LastScore);
                command.Parameters.AddWithValue("@max_level", element.MaxLevel);
                command.Parameters.AddWithValue("@last_level", element.LastLevel);
                command.ExecuteNonQuery();
            }
        }
        Debug.Log($"✅ Jugador '{element.Nombre}' agregado.");
    }

    public PlayerEntity FindUser(string nombre, string clave)
    {
        using (var connection = new SqliteConnection(_dbPath))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"SELECT nombre, clave, max_score, last_score, max_level, last_level FROM players WHERE nombre = @nombre AND clave = @clave;";
                command.Parameters.AddWithValue("@nombre", nombre);
                command.Parameters.AddWithValue("@clave", clave);

                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string nombreDb = reader.GetString(0);
                        string claveDb = reader.GetString(1);
                        int maxScore = reader.GetInt32(1+1);
                        int lastScore = reader.GetInt32(2+1);
                        int maxLevel = reader.GetInt32(3+1);
                        int lastLevel = reader.GetInt32(4+1);
                        return new PlayerEntity(nombreDb, claveDb, maxScore, lastScore, maxLevel, lastLevel);
                    }
                }
            }
        }
        return null;
    }

    public void Update(PlayerEntity element)
    {
        using (var connection = new SqliteConnection(_dbPath))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"UPDATE players
                                        SET clave = @clave,
                                            max_score = @max_score, 
                                            last_score = @last_score,
                                            max_level = @max_level,
                                            last_level = @last_level
                                        WHERE nombre = @nombre;";
                command.Parameters.AddWithValue("@clave", element.Clave);
                command.Parameters.AddWithValue("@max_level", element.MaxLevel);
                command.Parameters.AddWithValue("@last_level", element.LastLevel);
                command.Parameters.AddWithValue("@max_score", element.MaxScore);
                command.Parameters.AddWithValue("@last_score", element.LastScore);
                command.Parameters.AddWithValue("@nombre", element.Nombre);
                command.ExecuteNonQuery();
            }
        }
        Debug.Log($"🔄 Jugador '{element.Nombre}' actualizado.");
    }

    public void Delete(PlayerEntity element)
    {
        using (var connection = new SqliteConnection(_dbPath))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"DELETE FROM players WHERE nombre = @nombre;";
                command.Parameters.AddWithValue("@nombre", element.Nombre);
                command.ExecuteNonQuery();
            }
        }
        Debug.Log($"🗑️ Jugador '{element.Nombre}' eliminado.");
    }

    public void LogPath()
    {
        Debug.Log("📂 Ruta DB: " + _dbPath);
    }
}