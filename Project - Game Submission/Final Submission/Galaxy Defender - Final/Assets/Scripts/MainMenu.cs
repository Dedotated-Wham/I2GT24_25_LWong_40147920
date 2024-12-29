using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1); //Load scene index (1) which is Beta Scene.
    }

    public void HowToPlay()
    {
        SceneManager.LoadSceneAsync(2); //Load scene index (2) which is How To Play Menu.
    }

    public void Options()
    {
        SceneManager.LoadSceneAsync(3); //Load scene index (3) which is Options Menu.
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadSceneAsync(0); //Load scene index (0) which is Main Menu.
    }
}
