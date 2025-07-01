using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using System.Collections;

public class FinishGameController : MonoBehaviour
{
    [Inject] private ResetGame _resetter;
    [Inject] private ISubjectGame _game;
    [Inject] private LoseGame _loser;
    [Inject] private PacmanEntity _pacman;
    [Inject] private LogoutPlayer _logout;

    private bool _isReloading = false;

    // Duración de la animación de muerte (ajústala según tu animación real)
    [SerializeField] private float deathDelay = 2.5f;
    [SerializeField] private float winDelay = 2.5f;

    void Update()
    {
        if (_isReloading) return;

        // Si el jugador perdió
        if (_loser.IsOver(_pacman, _game, _logout))
        {
            _isReloading = true;
            StartCoroutine(WaitAndLoadScene("Login", deathDelay));
            return;
        }

        // Si se completa el nivel
        if (_resetter.ResetGameBoard(_game))
        {
            _isReloading = true;
            SceneManager.LoadScene("Level1");
        }
    }

    private IEnumerator WaitAndLoadScene(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
