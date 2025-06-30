using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public class PlayDeath : MonoBehaviour
{
    [Header("Death Animation")]
    public RuntimeAnimatorController deathAnimation;

    private Animator _animator;
    private SpriteRenderer _sprite;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    public void Play()
    {
        if (deathAnimation == null)
        {
            Debug.LogWarning("⚠️ No se asignó deathAnimation en PlayDeath.");
            return;
        }

        _animator.runtimeAnimatorController = deathAnimation;
        _animator.enabled = true;
        _sprite.enabled = true;
    }

    public void Stop()
    {
        _animator.enabled = false;
    }
}
