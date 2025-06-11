using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _smiles;

    private void OnEnable()
    {
        GameModeDemo.OnCrowdStateChange += ChangeCrowdMood;
    }

    private void OnDisable()
    {
        GameModeDemo.OnCrowdStateChange -= ChangeCrowdMood;
    }

    private void ChangeCrowdMood(CrowdState state)
    {
        if(state == CrowdState.Phase2)
        {
            _smiles[0].SetActive(false);
            _smiles[1].SetActive(true);
        }

        if(state == CrowdState.Phase3)
        {
            _smiles[1].SetActive(false);
            _smiles[2].SetActive(true);
        }

    }


}
