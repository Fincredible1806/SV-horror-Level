using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private int nextSceneNo;
    public void LoadNextScene()
    {
        SceneManager.LoadSceneAsync(nextSceneNo);
    }
    public void QuitTheGame()
    {
        Application.Quit();
    }
}
