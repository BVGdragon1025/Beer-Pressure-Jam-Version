using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAnimationEvents : MonoBehaviour
{
    [SerializeField]
    private GameObject _handObject;
    [SerializeField]
    private GameObject _trajectoryObject;
    [SerializeField]
    private GameObject _sliderObject;
    [SerializeField]
    private GameObject _timerObject;
    [SerializeField]
    private GameObject _moneyObject;

    public void ActivatePlayerControls()
    {
        _handObject.SetActive(true);
        _trajectoryObject.SetActive(true);
        _sliderObject.SetActive(true);
        _timerObject.SetActive(true);
        _moneyObject.SetActive(true);
        GameManager.Instance.SetPeopleLaugh(SoundType.SFX_LAUGHTER_1);
        Cursor.visible = false;
    }

    public void DeactivatePlayerControls()
    {
        _trajectoryObject.SetActive(false);
        _sliderObject.SetActive(false);
        _timerObject.SetActive(false);
        _handObject.SetActive(false);
        _trajectoryObject.SetActive(false);
    }

    public void RestartGame()
    {
        StartCoroutine(LoadNextScene());
    }

    private IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(6.0f);
        Cursor.visible = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
