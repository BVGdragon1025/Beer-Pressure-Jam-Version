using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableTrigger : KillPlane
{
    private float _timer = 0.0f;
    [SerializeField]
    private float _maxTime;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ball has entered the trigger!");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            if(_maxTime <= Timer())
            {
                other.gameObject.SetActive(false);           
                _timer = 0.0f;
                Debug.Log("Ball has been destroyed!");
                OnBallLost?.Invoke();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _timer = 0.0f;
    }

    private float Timer()
    {
        _timer += Time.deltaTime;
        return _timer;

    }

}
