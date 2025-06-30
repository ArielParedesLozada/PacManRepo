using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMovementFrightened : MonoBehaviour
{
    public RuntimeAnimatorController ghostBlue;
    public RuntimeAnimatorController ghostWhite;

    private Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Play(bool isWhite)
    {
        _animator.enabled = true;
        _animator.runtimeAnimatorController = isWhite ? ghostWhite : ghostBlue;
    }
}
