using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ForceSliderController))]
public class HandController : MonoBehaviour
{
    private GameManager _gameManager;
    private ForceSliderController _sliderController;

    [Header("Force Clamping")]
    [SerializeField]
    private float _minForce;
    [SerializeField]
    private float _maxForce;

    [Header("Hand Settings")]
    [SerializeField]
    private float _handDepthModifier;
    [SerializeField]
    private float _handYPosition;
    [SerializeField]
    private HandPlayer _playerHand;
    [SerializeField]
    private Hand _currentHand;

    public float MinForce { get { return _minForce; } }
    public float MaxForce { get { return _maxForce; } }
    public float HandYPosition { get { return _handYPosition; } }
    public float HandDepth {  get { return _handDepthModifier; } }
    public ForceSliderController SliderController { get { return _sliderController; } }


    void Start()
    {
        _sliderController = GetComponent<ForceSliderController>();
        _gameManager = GameManager.Instance;
        _currentHand = _playerHand;

    }

    private void OnDisable()
    {
        _sliderController.gameObject.SetActive(false);
    }

    public void SetCurrentHandActive(bool active)
    {
        _currentHand.gameObject.SetActive(active);

    }

}
