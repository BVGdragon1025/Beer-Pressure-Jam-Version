using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlane : MonoBehaviour
{
    public static Action OnBallLost;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        { 
            other.gameObject.SetActive(false);
            OnBallLost?.Invoke();
        }

    }
}
