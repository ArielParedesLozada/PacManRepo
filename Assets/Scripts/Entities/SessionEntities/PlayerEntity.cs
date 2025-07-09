using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity
{
    public string Nombre { get; }
    public string Password { get; private set; }
    public int MaxScore { get; private set; }
    public int LastScore { get; private set; }
    public int MaxLevel { get; private set; }
    public int LastLevel { get; private set; }

    private List<string> passwordHistory = new List<string>();

    public PlayerEntity(string nombre, string password = "", int maxScore = 0, int lastScore = 0, int maxLevel = 1, int lastLevel = 1)
    {
        Nombre = nombre;
        Password = password;
        MaxScore = maxScore;
        LastScore = lastScore;
        MaxLevel = maxLevel;
        LastLevel = lastLevel;
        if (!string.IsNullOrEmpty(password))
            passwordHistory.Add(password);
    }

    public void UpdateScore(int score)
    {
        LastScore = score;
        MaxScore = Mathf.Max(score, MaxScore);
    }

    public void UpdateLevel(int level)
    {
        LastLevel = level;
        MaxLevel = Mathf.Max(level, MaxLevel);
    }

    public bool CanChangePassword(string nueva)
    {
        return !passwordHistory.Contains(nueva);
    }

    public void ChangePassword(string nueva)
    {
        if (!CanChangePassword(nueva))
            throw new System.Exception("Contraseña ya utilizada.");
        Password = nueva;
        passwordHistory.Add(nueva);
    }

    public List<string> GetPasswordHistory()
    {
        return passwordHistory;
    }
}
