using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persist : MonoBehaviour
{
    public string chosenStartPoint, chosenEndPoint;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        chosenStartPoint = "A Block";
        chosenEndPoint = "A Block";
    }
}
