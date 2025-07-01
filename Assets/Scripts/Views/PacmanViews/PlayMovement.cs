using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(AudioSource), typeof(Animator), typeof(SpriteRenderer))]
public class PlayMovement : MonoBehaviour
{
    [Header("Chomp SFX")]
    public AudioClip chomp1;
    public AudioClip chomp2;

    [Header("Animations")]
    public RuntimeAnimatorController chompAnimation;

    [Header("Idle Sprite")]
    public Sprite idleSprite;

    [HideInInspector]
    public Vector2 orientation = Vector2.left;
    bool _playedChomp1 = false;

    AudioSource _audio;
    Animator _anim;
    SpriteRenderer _sprite;
    Vector3 _startLocalPos;
    public float chompCooldown = 0.25f; // tiempo mínimo entre sonidos
    float _lastChompTime = -999f;
    void Awake()
    {
        _audio = GetComponent<AudioSource>();
        _anim = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        _startLocalPos = transform.localPosition;
    }
    public void Chomp()
    {
        if (Time.time - _lastChompTime < chompCooldown)
            return; // demasiado pronto, no reproducir

        _lastChompTime = Time.time;

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
    public void StopChomp()
    {
        if (_anim.enabled)
        {
            _anim.enabled = false;
        }
        if (_sprite != null && idleSprite != null)
        {
            _sprite.sprite = idleSprite;
        }
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
}
