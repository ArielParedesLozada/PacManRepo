using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer), typeof(AudioSource))]
public class PlayDeath : MonoBehaviour
{
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
        if (deathAnimation == null || deathClip == null)
        {
            Debug.LogWarning("⚠️ Falta asignar animación o clip de muerte.");
            return;
        }

        // Activar componentes visuales
        _sprite.enabled = true;
        _animator.runtimeAnimatorController = deathAnimation;
        _animator.enabled = true;

        // Sonido
        _deathSound.PlayOneShot(deathClip);
    }

    public void Stop()
    {
        _animator.enabled = false;
    }
}
