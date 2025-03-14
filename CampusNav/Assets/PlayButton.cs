using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    [SerializeField] Persist persistentDataHolder;
    [SerializeField] GameObject warningText;

    private void Start()
    {
        persistentDataHolder = GameObject.Find("DropdownDataHolder").GetComponent<Persist>();
        persistentDataHolder.chosenStartPoint = "A Block";
        persistentDataHolder.chosenEndPoint = "A Block";
    }
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
