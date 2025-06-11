using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupRemover : MonoBehaviour
{
    private GameManager _gameManager;
    public static event Action OnPointScored;
    public static event Action<GameObject> OnCupDisabled;

    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out BallController _ball))
        {
            _ball.gameObject.SetActive(false);
            if (!_ball.IsGhost)
            {
                ShowHitEffects();
                OnCupDisabled?.Invoke(transform.parent.gameObject);
                transform.parent.gameObject.SetActive(false);
            }
            OnPointScored?.Invoke();
        }
    }

    private void ShowHitEffects()
    {
        GameObject effect = _gameManager.ObjectPuller.GetObjectPool(ObjectPoolerType.SFX);
        effect.transform.position = transform.parent.position;
        effect.SetActive(true);
    }

}
