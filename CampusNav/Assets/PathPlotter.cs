using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathPlotter : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;

    public LineRenderer lineRenderer;

    private NavMeshPath navMeshPath;
    private List<Vector3> splinePoints = new List<Vector3>();

    private void Start()
    {
        navMeshPath = new NavMeshPath();
        DrawSpline();
    }

    private void DrawSpline()
    {
        if(NavMesh.CalculatePath(startPoint.position, endPoint.position, NavMesh.AllAreas, navMeshPath)) 
        {
            splinePoints = Smooth(navMeshPath.corners);
            lineRenderer.positionCount = splinePoints.Count;
            lineRenderer.SetPositions(splinePoints.ToArray());
        }
    }

    private List<Vector3> Smooth(Vector3[] corners)
    {
        List<Vector3> smoothedPoints = new List<Vector3>();
        int segments = 10;

        for (int i = 0; i < corners.Length - 1; i++)
        {
            for (int j = 0; j < segments; j++)
            {
                Vector3 interpPoint = Vector3.Lerp(corners[i], corners[i+1], j / (float)segments);
                smoothedPoints.Add(interpPoint);
            }
        }
        smoothedPoints.Add(corners[^1]);
        return smoothedPoints;
    }
}
