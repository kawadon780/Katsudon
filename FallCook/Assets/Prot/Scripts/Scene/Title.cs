using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene("Game");
    }

    public void GameEnd()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
        UnityEngine.Application.Quit();
#else 
        Application.Quit();
#endif

    }

    void Update()
    {
#if UNITY_EDITOR || UNITY_ANDROID
        if (Input.GetKey(KeyCode.Escape)) GameEnd();
#endif
    }

}
