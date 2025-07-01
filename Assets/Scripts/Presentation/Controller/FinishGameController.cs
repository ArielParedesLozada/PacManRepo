using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class FinishGameController : MonoBehaviour
{
    [Inject]
    private readonly ResetGame _resetter;
    [Inject]
    private readonly ISubjectGame _game;
    [Inject]
    private readonly LoseGame _loser;
    [Inject]
    private readonly PacmanEntity _pacman;
    private bool _isReloading = false;
    void Update()
    {
        if (_isReloading)
        {
            return;
        }
        if (_loser.IsOver(_pacman, _game))
        {
            _isReloading = true;
            SceneManager.LoadScene("Login");
        }
        if (_resetter.ResetGameBoard(_game))
        {
            _isReloading = true;
            SceneManager.LoadScene("Level1");
        }
    }
}
