using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseRoute : MonoBehaviour
{
    private Persist persistentObject;
    private string chosenStartPoint, chosenEndPoint;
    [SerializeField] GameObject startPoint, endPoint, player;
    private GameObject pointsHolder, newStartPoint, newEndPoint;
    [SerializeField] PathPlotter pathPlotter;

    private void Start()
    {
        if (GameObject.Find("DropdownDataHolder") != null)
        {
            persistentObject = GameObject.Find("DropdownDataHolder").GetComponent<Persist>();
            chosenStartPoint = persistentObject.chosenStartPoint;
            chosenEndPoint = persistentObject.chosenEndPoint;
        }
        else
        {
            chosenStartPoint = "South Entrance";
            chosenEndPoint = "Student Union";
        }

        pointsHolder = GameObject.FindGameObjectWithTag("PointsHolder");

        newStartPoint = pointsHolder.transform.Find(chosenStartPoint).gameObject;
        newEndPoint = pointsHolder.transform.Find(chosenEndPoint).gameObject;

        startPoint.transform.position = newStartPoint.transform.position;
        endPoint.transform.position = newEndPoint.transform.position;

        player.transform.position = new Vector3(startPoint.transform.position.x, startPoint.transform.position.y + 15, startPoint.transform.position.z);

        pathPlotter.Plot();
    }

    private void Update()
    {
        Debug.Log(chosenStartPoint + " to " + chosenEndPoint);
    }
}
