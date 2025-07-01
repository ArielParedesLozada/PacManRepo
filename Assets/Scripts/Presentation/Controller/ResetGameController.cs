using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class ResetGameController : MonoBehaviour
{
    [Inject]
    private readonly ResetGame _resetter;
    [Inject]
    private readonly ISubjectGame _game;
    private bool _isReloading = false;
    void Update()
    {
        if (!_isReloading && _resetter.ResetGameBoard(_game))
        {
            _isReloading = true;
            Debug.Log($"Mi nivel actual es {new LevelSetter().GetLevel()} PAPU");
            SceneManager.LoadScene("Level1");
        }
    }
}
