using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private ObjectPuller _objectPuller;
    private SoundManager _soundManager;

    [SerializeField]
    private GameObject _ballPrefab;
    public GameObject BallPrefab { get { return _ballPrefab; } }
    public GameModeType gameType;

    [Header("Electric Shock Section")]
    [SerializeField]
    private TextMeshProUGUI _timerText;
    [SerializeField]
    private GameObject _electricObject;
    private Camera _mainCamera;

    [Header("Phases")]
    private Volume[] _volumes;
    [SerializeField, Tooltip("Time between Volume transitions")]
    private float _transitionTime;

    [Header("Player Stats")]
    [SerializeField]
    private CurrentTurn _currentTurn = CurrentTurn.Player;
    [SerializeField]
    private PlayerScore _playerSO;
    [SerializeField]
    private TextMeshProUGUI _moneyText;
    [SerializeField]
    private int _playerMoney;

    public Camera MainCamera { get { return _mainCamera; } }
    public TextMeshProUGUI ScoreText {  get { return _moneyText; } }
    public TextMeshProUGUI TimerText {  get { return _timerText; } }
    public Volume[] Volumes { get { return _volumes; } }
    public GameObject ElectricObject {  get { return _electricObject; } }
    public ObjectPuller ObjectPuller { get { return _objectPuller; } }
    public PlayerScore PlayerSO { get { return _playerSO; } }
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;

        _objectPuller = GetComponent<ObjectPuller>();
        ObjectPuller.PreparePools();

    }

    void Start()
    {
        _mainCamera = Camera.main;
        _soundManager = SoundManager.Instance;
        _volumes = _mainCamera.GetComponents<Volume>();
        gameType = GameModeType.None;
    }


    public void SpawnNewBall(Vector3 position, float forceValue, Transform handTransform)
    {
        GameObject ball = _objectPuller.GetObjectPool(ObjectPoolerType.BALL);
        if (_currentTurn == CurrentTurn.Enemy)
            ball = _objectPuller.GetObjectPool(ObjectPoolerType.AI_BALL);
        Rigidbody ballRb = ball.GetComponent<Rigidbody>();
        ball.transform.SetPositionAndRotation(position, Quaternion.identity);
        Debug.Log($"Force value: {forceValue}");
        ball.SetActive(true);
        ballRb.velocity = Vector3.zero;
        ballRb.AddForce(forceValue * (-handTransform.forward + new Vector3(0, 0.7f, 0)), ForceMode.Impulse);
        
    }

    public void SetPeopleLaugh(SoundType laughType)
    {
        _soundManager.PlayLoop(laughType);
    }

    public IEnumerator VolumeTransition(Volume previous, Volume current)
    {
        float reverseTimer = _transitionTime;
        float timer = 0;
        while(current.weight < 1 && previous.weight > 0)
        {
            reverseTimer -= Time.deltaTime;
            timer += Time.deltaTime;
            previous.weight = Mathf.Lerp(0, 1, reverseTimer / _transitionTime);
            Debug.Log(reverseTimer / _transitionTime);
            current.weight = Mathf.Lerp(0, 1, timer / _transitionTime);
            Debug.Log($"Previous volume: {previous.weight}, Current volume: {current.weight}");
            yield return null;
        }
    }

    public IEnumerator VolumeTransition(Volume current)
    {
        float timer = 0;
        while (current.weight < 1)
        {
            timer += Time.deltaTime;
            current.weight = Mathf.Lerp(0, 1, timer / _transitionTime);
            Debug.Log($"Current volume: {current.weight}");
            yield return null;
        }
    }

}
