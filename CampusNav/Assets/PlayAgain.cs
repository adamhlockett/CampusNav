using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgain : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Retry()
    {
        SceneManager.LoadScene("Main");
    }
}
