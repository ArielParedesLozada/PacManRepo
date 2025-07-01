using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer), typeof(AudioSource))]
public class PlayDeath : MonoBehaviour
{
    private bool _hasPlayed = false;

    [Header("Death Animation")]
    public RuntimeAnimatorController deathAnimation;

    [Header("Death SFX")]
    public AudioClip deathClip;

    private AudioSource _deathSound;
    private Animator _animator;
    private SpriteRenderer _sprite;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _deathSound = GetComponent<AudioSource>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    public void Play()
    {
        if (_hasPlayed)
        {
            Debug.Log("Ya se jugo la cosa");
            return;
        }

        if (deathAnimation == null || deathClip == null)
        {
            Debug.LogWarning("⚠️ No se puede reproducir animación o sonido de muerte porque falta asignación.");
            return;
        }

        _hasPlayed = true;

        // Configurar animación
        _animator.runtimeAnimatorController = deathAnimation;
        _animator.enabled = true;
        _sprite.enabled = true;

        // Reproducir sonido
        _deathSound.PlayOneShot(deathClip);
    }

    public void Stop()
    {
        _animator.enabled = false;
    }

    public void ResetDeath()
    {
        _hasPlayed = false;
    }
}
