using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void PlayingFirstTime()
    {
        SceneManager.LoadScene(1);
    }

    public void NotPlayingFirstTime()
    {
        SceneManager.LoadScene(2);
    }

    public void SubmitName()
    {
        SceneManager.LoadScene(3);
    }

    public void ExitButton()
    {
        Application.Quit();
    } 
}
