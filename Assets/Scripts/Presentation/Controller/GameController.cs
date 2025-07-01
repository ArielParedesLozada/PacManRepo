using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameController : MonoBehaviour
{
    [SerializeField] private Text ScoreText;
    [SerializeField] private Text LevelText;
    [SerializeField] private Text PlayerName;
    [Inject] private ISubjectGame _gameBoard;
    public int Score => _gameBoard.Score;
    public int Level => _gameBoard.Level;

    public void Initialize(ISubjectGame gameBoard)
    {
        _gameBoard = gameBoard;
    }

    void Start()
    {
        PlayerName.text = SessionEntity.GetInstance().CurrentPlayer.Nombre;
    }
    void Update()
    {
        if (_gameBoard == null)
        {
            Debug.LogWarning("SIGMA Sin gameboard");
            return;
        }
        ScoreText.text = Score.ToString();
        LevelText.text = Level.ToString();
    }

}
