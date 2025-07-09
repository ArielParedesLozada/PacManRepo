using System.Data;
using UnityEngine;
using Mono.Data.Sqlite;
using System;

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
                    password TEXT,
                    max_score INTEGER,
                    last_score INTEGER,
                    max_level INTEGER,
                    last_level INTEGER
                );

                  CREATE TABLE IF NOT EXISTS password_history (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    nombre TEXT NOT NULL,
                    password TEXT NOT NULL
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
                command.CommandText = @"INSERT INTO players (nombre, password, max_score, last_score, max_level, last_level)
                                    VALUES (@nombre, @password, @max_score, @last_score, @max_level, @last_level);";
                command.Parameters.AddWithValue("@nombre", element.Nombre);
                command.Parameters.AddWithValue("@password", element.Password);
                command.Parameters.AddWithValue("@max_score", element.MaxScore);
                command.Parameters.AddWithValue("@last_score", element.LastScore);
                command.Parameters.AddWithValue("@max_level", element.MaxLevel);
                command.Parameters.AddWithValue("@last_level", element.LastLevel);
                command.ExecuteNonQuery();
            }

            // Guardar en historial
            using (var historyCommand = connection.CreateCommand())
            {
                historyCommand.CommandText = @"INSERT INTO password_history (nombre, password) VALUES (@nombre, @password);";
                historyCommand.Parameters.AddWithValue("@nombre", element.Nombre);
                historyCommand.Parameters.AddWithValue("@password", element.Password);
                historyCommand.ExecuteNonQuery();
            }
        }
        Debug.Log($"✅ Jugador '{element.Nombre}' agregado.");
    }

    public PlayerEntity FindByName(string nombre)
    {
        using (var connection = new SqliteConnection(_dbPath))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"SELECT nombre, password, max_score, last_score, max_level, last_level FROM players WHERE nombre = @nombre;";
                command.Parameters.AddWithValue("@nombre", nombre);

                using (IDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string nombreDb = reader.GetString(0);
                        string password = reader.GetString(1);
                        int maxScore = reader.GetInt32(2);
                        int lastScore = reader.GetInt32(3);
                        int maxLevel = reader.GetInt32(4);
                        int lastLevel = reader.GetInt32(5);
                        return new PlayerEntity(nombreDb, password, maxScore, lastScore, maxLevel, lastLevel);
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
                                        SET max_score = @max_score, 
                                            last_score = @last_score,
                                            max_level = @max_level,
                                            last_level = @last_level
                                        WHERE nombre = @nombre;";
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

    public bool IsPasswordUsedByAnotherPlayer(string nombre, string password)
    {
        using (var connection = new SqliteConnection(_dbPath))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"SELECT COUNT(*) FROM password_history WHERE password = @password AND nombre != @nombre;";
                command.Parameters.AddWithValue("@password", password);
                command.Parameters.AddWithValue("@nombre", nombre);
                return Convert.ToInt32(command.ExecuteScalar()) > 0;
            }
        }
    }
    public void UpdatePassword(string nombre, string nuevaPassword)
    {
        using (var connection = new SqliteConnection(_dbPath))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"UPDATE players SET password = @password WHERE nombre = @nombre;";
                command.Parameters.AddWithValue("@nombre", nombre);
                command.Parameters.AddWithValue("@password", nuevaPassword);
                command.ExecuteNonQuery();
            }

            using (var historyCommand = connection.CreateCommand())
            {
                historyCommand.CommandText = @"INSERT INTO password_history (nombre, password) VALUES (@nombre, @password);";
                historyCommand.Parameters.AddWithValue("@nombre", nombre);
                historyCommand.Parameters.AddWithValue("@password", nuevaPassword);
                historyCommand.ExecuteNonQuery();
            }
        }
        Debug.Log($"🔐 Contraseña actualizada para {nombre}");
    }


}