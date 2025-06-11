using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashEffect : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _particle;
    [SerializeField]
    private float _stopDelay;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            _particle.Play();
            StartCoroutine(nameof(StopParticle));
        }
    }

    private IEnumerable StopParticle()
    {
        yield return new WaitForSeconds(_stopDelay);
        _particle.Stop();
    }

}
