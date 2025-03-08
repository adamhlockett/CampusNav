using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropdownData : MonoBehaviour
{
    [SerializeField] TMP_Dropdown startDD, endDD;
    Persist persistentObject;

    private void Start()
    {
        persistentObject = GameObject.Find("DropdownDataHolder").GetComponent<Persist>();
    }

    public void StartValueChanged()
    {
        int selection = startDD.value;
        persistentObject.chosenStartPoint = startDD.options[selection].text;
    }

    public void EndValueChanged()
    {
        int selection = endDD.value;
        persistentObject.chosenEndPoint = endDD.options[selection].text;
    }
}
