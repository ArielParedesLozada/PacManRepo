using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMovementConsumed : MonoBehaviour
{
    public Sprite eyesUp;
    public Sprite eyesDown;
    public Sprite eyesLeft;
    public Sprite eyesRight;

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    public void Play(Vector2 dir)
    {
        _animator.enabled = false;

        if (dir == Vector2.up)
            _spriteRenderer.sprite = eyesUp;
        else if (dir == Vector2.down)
            _spriteRenderer.sprite = eyesDown;
        else if (dir == Vector2.left)
            _spriteRenderer.sprite = eyesLeft;
        else if (dir == Vector2.right)
            _spriteRenderer.sprite = eyesRight;
        else
            _spriteRenderer.sprite = eyesLeft;
    }
}
