﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(Animator), typeof(SpriteRenderer))]
public class PacManView : MonoBehaviour
{
    [Header("Chomp SFX")]
    public AudioClip chomp1;
    public AudioClip chomp2;

    [Header("Animations")]
    public RuntimeAnimatorController chompAnimation;
    public RuntimeAnimatorController deathAnimation;

    [Header("Idle Sprite")]
    public Sprite idleSprite;

    [Header("Movement (speed config only)")]
    public float speed = 6f;

    [HideInInspector]
    public Vector2 orientation = Vector2.left;

    [HideInInspector]
    public bool canMove = true;

    AudioSource _audio;
    Animator _anim;
    SpriteRenderer _sprite;

    Vector3 _startLocalPos;


    bool _playedChomp1 = false;

    void Awake()
    {
        _audio = GetComponent<AudioSource>();
        _anim = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        _startLocalPos = transform.localPosition;

    }

    /// <summary>
    /// Reproduce un chomp alternando entre clip1 y clip2
    /// </summary>
    public void PlayChomp()
    {
        if (_anim.runtimeAnimatorController != chompAnimation)
        {
            _anim.runtimeAnimatorController = chompAnimation;
        }

        if (!_anim.enabled)
            _anim.enabled = true;

        var clip = _playedChomp1 ? chomp2 : chomp1;
        _playedChomp1 = !_playedChomp1;
        _audio.PlayOneShot(clip);
    }

    /// <summary>
    /// Entra en estado idle (sprite estático)
    /// </summary>
    public void ShowIdle()
    {
        _anim.enabled = false;
        _sprite.sprite = idleSprite;
    }

    /// <summary>
    /// Reproduce la animación de muerte
    /// </summary>
    public void PlayDeath()
    {
        if (_anim == null) _anim = GetComponent<Animator>();
        if (_sprite == null) _sprite = GetComponent<SpriteRenderer>();

        if (deathAnimation != null)
        {
            _anim.runtimeAnimatorController = deathAnimation;
            _anim.enabled = true;
            _sprite.enabled = true; // por si se ocultó
        }
        else
        {
            Debug.LogWarning("deathAnimation no está asignado en PacManView.");
        }
    }

    /// <summary>
    /// Teletransporta a la posición inicial y pasa a idle.
    /// </summary>
    public void MoveToStartingPosition()
    {
        transform.localPosition = _startLocalPos;
        ShowIdle();
        canMove = false;
    }

    public void ShowMoving()
    {
        if (_anim.runtimeAnimatorController != chompAnimation)
        {
            _anim.runtimeAnimatorController = chompAnimation;
        }

        if (!_anim.enabled)
        {
            _anim.enabled = true;
        }
    }

    /// <summary>
    /// Ajusta la “velocidad” (solo visual para el Inspector)
    /// </summary>
    public void SetDifficultyForLevel(int level)
    {
        switch (level)
        {
            case 1: speed = 6f; break;
            case 2: speed = 7f; break;
            case 3: speed = 8f; break;
            case 4: speed = 9f; break;
            case 5: speed = 10f; break;
            default: speed = 6f; break;
        }
    }

    /// <summary>
    /// “Resetea” la vista como al inicio de la partida
    /// </summary>
    public void Restart()
    {
        canMove = true;
        _anim.runtimeAnimatorController = chompAnimation;
        _anim.enabled = true;
        ShowIdle();
    }

    //Para obtener la velocidad actual desde el View 
    public float GetCurrentSpeed()
    {
        return speed;
    }

    //Llamado por el caso de uso al comer pellet
    public void OnPelletConsumed()
    {
        PlayChomp();
    }

    public void IncreaseSpeedTemporarily(float amount, float duration)
    {
        speed += amount;
        StartCoroutine(ResetSpeedAfterDelay(duration));
    }

    IEnumerator ResetSpeedAfterDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        SetDifficultyForLevel(GameBoardView.currentPlayerLevel);
    }

    public void PlayDeathAnimation()
    {
        _anim.runtimeAnimatorController = deathAnimation;
        _anim.enabled = true;
    }

    public void RestartAnimation()
    {
        _anim.runtimeAnimatorController = chompAnimation;
        _anim.enabled = true;
    }



}
