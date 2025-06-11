using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectionManager : MonoBehaviour
{
    private GameManager _gameManager;
    private Hand _hand;

    [SerializeField]
    private float _throwForce;

    [Header("Ball Object")]
    [SerializeField]
    private GameObject _ball;

    [SerializeField]
    private TrajectoryProjection _projection;

    void Start()
    {
        _hand = GetComponentInParent<Hand>();
        _gameManager = GameManager.Instance;

    }

    void Update()
    {
        _throwForce = _hand.ThrowForce;
        if (_hand.ThrowState != ThrowState.Thrown)
        {
            if (!_projection.isActiveAndEnabled)
            {
                _projection.gameObject.SetActive(true);
            }
            _projection.SimulateTrajectory(_gameManager.BallPrefab, _ball.transform.position, _throwForce);
        }
        else
        {
            _projection.gameObject.SetActive(false);
        }
    }

}
