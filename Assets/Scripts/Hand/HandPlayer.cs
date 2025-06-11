using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPlayer : Hand
{
    [Header("Player Specific")]
    [SerializeField]
    private GameObject _trajectoryObject;

    protected override void OnEnable()
    {
        base.OnEnable();
        if (!_trajectoryObject.activeInHierarchy)
            _trajectoryObject.SetActive(true);
    }

    private void Update()
    {
        HandControls(handController.MinForce, handController.MaxForce, handController.HandYPosition, handController.HandDepth);
    }

    public override void HandControls(float minForce, float maxForce, float yPos, float handDepth)
    {
        Vector3 mousePosition = Camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, yPos, Camera.nearClipPlane + handDepth));

        if (throwState == ThrowState.Ready)
            transform.position = mousePosition;


        if (Input.GetMouseButton(0) && throwState == ThrowState.Ready)
        {
            ThrowForce = Mathf.Clamp(ThrowForce + (minForce * Time.deltaTime), minForce, maxForce);
        }

        handController.SliderController.ForceSlider.value = ThrowForce;

        if (Input.GetMouseButtonUp(0) && throwState == ThrowState.Ready)
        {
            throwState = ThrowState.Preparing;
            animator.SetTrigger("wasThrown");

        }
    }


}
