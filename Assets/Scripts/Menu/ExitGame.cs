using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ExitGame : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnMouseOver()
    {
        if(GameManager.Instance.gameType == GameModeType.None)
        {
            if (!_animator.isActiveAndEnabled)
                _animator.enabled = true;
            else
                _animator.SetBool("isHovering", true);

            if (Input.GetMouseButton(0))
            {
                Application.Quit();

#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }
        }

    }
            
        

    private void OnMouseExit()
    {
        _animator.SetBool("isHovering", false);
    }

}
