using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    [SerializeField] Persist persistentDataHolder;
    [SerializeField] GameObject warningText;
    public void LaunchGame()
    {
        if(persistentDataHolder.chosenStartPoint == persistentDataHolder.chosenEndPoint)
        {
            warningText.SetActive(true);
            return;
        }
        SceneManager.LoadScene("Main");
    }
}
