using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class HitEffect : MonoBehaviour
{
    private AudioSource _audioSource;

    [SerializeField]
    private ParticleSystem _confetti;
    [SerializeField]
    private AudioClip _confettiSound;
    [SerializeField]
    private float _stopDelay;

    // Start is called before the first frame update
    void OnEnable()
    {
        _audioSource = GetComponent<AudioSource>();
        if(GameManager.Instance.gameType != GameModeType.None)
        {
            PlayEffects();
            Invoke(nameof(DisableObject), _stopDelay);
        }

    }

    private void PlayEffects()
    {
        SoundManager.Instance.PlayOneShot(SoundType.SFX_CONFETTI, _audioSource);
        _confetti.Play();
    }

    private void DisableObject()
    {
        gameObject.SetActive(false);
    }


}
