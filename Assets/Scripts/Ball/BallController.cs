using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField]
    private AudioClip _hitSound;
    private AudioSource _audioSource;

    public bool IsGhost { get; set; }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!IsGhost)
        {
            SoundManager.Instance.PlayOneShot(SoundType.SFX_BALL_HIT, _audioSource);

        }
    }

    private void DestroyBall()
    {
        gameObject.SetActive(false);
    }


}
