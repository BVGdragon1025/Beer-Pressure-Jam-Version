using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForceSliderController : MonoBehaviour
{
    private HandController _hand;

    [Header("UI Elements")]
    [SerializeField]
    private Slider _forceSlider;

    public Slider ForceSlider { get { return _forceSlider; } }


    // Start is called before the first frame update
    void Start()
    {
        _hand = GetComponent<HandController>();

        _forceSlider.minValue = _hand.MinForce;
        _forceSlider.maxValue = _hand.MaxForce;
    }

}
