using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModeDemo : MonoBehaviour
{
    [Header("GameMode Values")]
    [SerializeField]
    private GameModeType _gameMode;
    private PlayerScore _playerScoreSO;
    [SerializeField]
    private int _cupsDrunk;
    [SerializeField]
    private int _pointsValue;
    private float _currentTimer;

    [Header("Player Stats")]
    [SerializeField]
    private int _maxLives;
    private int _currentLives;

    private GameManager _gameManager;
    private SoundManager _soundManager;

    [Header("Timers")]
    [SerializeField]
    private int[] _timers = new int[3];

    public static event Action<CrowdState> OnCrowdStateChange;

    private void OnApplicationFocus(bool focus)
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void OnEnable()
    {
        CupRemover.OnPointScored += AddPoints;
    }

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _soundManager = SoundManager.Instance;
        _playerScoreSO = _gameManager.PlayerSO;
        _gameManager.gameType = _gameMode;
        _cupsDrunk = 0;
        _playerScoreSO.continuations++;
        _currentLives = _maxLives;
        _currentTimer = _timers[0];
        _gameManager.ScoreText.text = $"{_playerScoreSO.score}";
    }

    void Update()
    {
        if (_gameManager.TimerText.gameObject.activeInHierarchy)
        {
            if(_currentTimer <= 0.3f)
            {
                _gameManager.ElectricObject.SetActive(true);
            }
            if(Timer() <= 0)
            {
                ChangeLivesAmount(-1);
                ElectricShock();
                
            }
        }
    }

    private void ChangeLivesAmount(int amount)
    {
        _currentLives = Mathf.Clamp(_currentLives + amount, 0, _maxLives);
        CheckRemainingLives();
    }

    private void AddPoints()
    {
        _cupsDrunk++;
        _playerScoreSO.score += _pointsValue * _playerScoreSO.continuations;
        _gameManager.ScoreText.text = $"{_playerScoreSO.score}";

        if(_cupsDrunk >= 6)
        {
            _gameManager.MainCamera.GetComponent<Animator>().SetTrigger("hasWon");
            _soundManager.StopAudioSource();
            _soundManager.PlayOneShot(SoundType.SFX_DRINKING);
        }
    }

    private void ElectricShock()
    {
        _gameManager.ElectricObject.SetActive(false);

        if (_currentLives == 2)
        {
            StartCoroutine(_gameManager.VolumeTransition(_gameManager.Volumes[1]));
        }

        if (_currentLives == 1)
        {
            StartCoroutine(_gameManager.VolumeTransition(_gameManager.Volumes[1], _gameManager.Volumes[2]));
        }

    }

    private void CheckRemainingLives()
    {
        if (_currentLives == 2)
        {
            _currentTimer = _timers[1];
            OnCrowdStateChange?.Invoke(CrowdState.Phase2);
            _gameManager.SetPeopleLaugh(SoundType.SFX_LAUGHTER_2);
            return;

        }

        if(_currentLives == 1)
        {
            _currentTimer = _timers[2];
            OnCrowdStateChange?.Invoke(CrowdState.Phase3);
            _gameManager.SetPeopleLaugh(SoundType.SFX_LAUGHTER_3);
            return;
        }

        if (_currentLives <= 0)
        {
            SceneManager.LoadScene(1);
            return;
        }
    }

    private float Timer()
    {
        _currentTimer -= Time.deltaTime;
        int minutes = (int)_currentTimer % 60;
        int seconds = (int)_currentTimer / 60;
        _gameManager.TimerText.text = string.Format("{1:00}:{0:00}", minutes, seconds);
        return _currentTimer;
    }

    private void OnDisable()
    {
        CupRemover.OnPointScored -= AddPoints;
    }

}
