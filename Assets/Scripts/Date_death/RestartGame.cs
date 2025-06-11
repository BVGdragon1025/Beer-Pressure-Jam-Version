using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    [SerializeField]
    private float _quitDelay;

    private void Start()
    {
        Cursor.visible = true;
        if(_quitDelay > 0)
        {
            Invoke(nameof(CloseGame), _quitDelay);
        }
        
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void CloseGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

    }

}
