using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hand : MonoBehaviour
{
    protected Animator animator;
    protected GameManager gameManager;
    protected HandController handController;

    [SerializeField]
    protected ThrowState throwState;
    [SerializeField]
    protected GameObject ballObject;

    public Renderer BallObjectRenderer { get { return ballObject.GetComponent<Renderer>(); } }
    public float ThrowForce { get; set; }
    public Camera Camera { protected get; set; }
    public ThrowState ThrowState { get { return throwState; } }

    protected virtual void OnEnable()
    {
        KillPlane.OnBallLost += ShowNewBall;
        CupRemover.OnPointScored += ShowNewBall;
        throwState = ThrowState.Ready;

        if(handController == null)
            handController = GetComponentInParent<HandController>();

        ThrowForce = handController.MinForce;

        if (!ballObject.activeInHierarchy)
            ballObject.SetActive(true);

    }

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        gameManager = GameManager.Instance;
        Camera = Camera.main;

    }

    public abstract void HandControls(float minForce, float maxForce, float yPos, float handDepth);

    public void ShowNewBall()
    {
        ThrowForce = handController.MinForce;
        ballObject.SetActive(true);
        animator.SetTrigger("reload");
        throwState = ThrowState.Ready;
    }

    public void ThrowBall()
    {
        throwState = ThrowState.Thrown;
        gameManager.SpawnNewBall(ballObject.transform.position, ThrowForce, transform);
        ballObject.SetActive(false);
    }

    protected virtual void OnDisable()
    {
        KillPlane.OnBallLost -= ShowNewBall;
        CupRemover.OnPointScored -= ShowNewBall;
    }
}
