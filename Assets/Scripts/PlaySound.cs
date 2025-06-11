using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public void PlayElectrocutionAudioClip()
    {
        SoundManager.Instance.PlayOneShot(SoundType.SFX_ELECTRIC_SHOCK);
    }
}
