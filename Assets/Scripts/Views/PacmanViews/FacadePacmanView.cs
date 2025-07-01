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
        if (ShouldChomp())
        {
            _playMovement.Chomp();
        }
        else
        {
            _playMovement.StopChomp();
        }
        if (ShouldDie())
        {
            _playDeath.Play();
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
