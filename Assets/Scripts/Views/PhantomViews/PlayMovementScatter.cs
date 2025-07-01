using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMovementScatter : MonoBehaviour
{
    public RuntimeAnimatorController ghostUp;
    public RuntimeAnimatorController ghostDown;
    public RuntimeAnimatorController ghostLeft;
    public RuntimeAnimatorController ghostRight;

    private Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Play(Vector2 dir)
    {
        _animator.enabled = true;

        if (dir == Vector2.up)
            _animator.runtimeAnimatorController = ghostUp;
        else if (dir == Vector2.down)
            _animator.runtimeAnimatorController = ghostDown;
        else if (dir == Vector2.left)
            _animator.runtimeAnimatorController = ghostLeft;
        else if (dir == Vector2.right)
            _animator.runtimeAnimatorController = ghostRight;
        else
            _animator.runtimeAnimatorController = ghostLeft; // fallback
    }

    public void Stop()
    {
        _animator.enabled = false;
    }

}
