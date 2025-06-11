using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    private Camera _mainCamera;
    private Animator _cameraAnimator;
    private Animator _animator;
    [SerializeField]
    private GameObject _gameModeObject;

    private void Start()
    {
        _mainCamera = Camera.main;
        _cameraAnimator = _mainCamera.GetComponent<Animator>();
        _animator = GetComponent<Animator>();

    }

    private void OnMouseOver()
    {
        if (GameManager.Instance.gameType == GameModeType.None)
        {
            if (!_animator.isActiveAndEnabled)
                _animator.enabled = true;
            else
                _animator.SetBool("isHovering", true);

            if (Input.GetMouseButton(0))
            {
                _cameraAnimator.enabled = true;
                _gameModeObject.SetActive(true);

            }
        }

    }

    private void OnMouseExit()
    {
        _animator.SetBool("isHovering", false);
    }

}
