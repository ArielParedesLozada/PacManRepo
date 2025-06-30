using UnityEngine;
using Zenject;

[RequireComponent(typeof(PlayMovement))]
public class FacadePacmanView : MonoBehaviour
{
    [Inject]
    private PacmanManager _controller;
    [Inject]
    private PlayMovement _playMovement;
    [Inject]
    private PlayDeath _playDeath;
    void Awake()
    {
        _playMovement = GetComponent<PlayMovement>();
        _playDeath = GetComponent<PlayDeath>();
    }

    void Update()
    {
        if (_playMovement == null)
        {
            Debug.LogError("PlayMovement no encontrado en FacadePacmanView.");
            return;
        }

        // Aquí debes agregar una condición para que no se reproduzca el sonido siempre
        // Solo si Pacman se está moviendo, por ejemplo
        if (ShouldChomp())
        {
            Debug.Log("SIGMA");
            _playMovement.Chomp();
        }
        else
        {
            Debug.Log("NO SIGMA");
            _playMovement.StopChomp();
        }
        if (ShouldDie())
        {
            _playDeath.Play();
        }
        else
        {
            _playDeath.Stop();
        }
    }

    private bool ShouldChomp()
    {
        try
        {
            var pacman = _controller.Pacman;
            return !(pacman == null ||
                   pacman.PacManState == PacManState.Dead ||
                   pacman.PacManState == PacManState.Still);
        }
        catch
        {
            Debug.Log("error maluco");
            return false;
        }
    }

    private bool ShouldDie()
    {
        try
        {
            var pacman = _controller.Pacman;
            return pacman.PacManState == PacManState.Dead;
        }
        catch
        {
            return false;
        }
    }
}
