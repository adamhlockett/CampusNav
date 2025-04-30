using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    int pointCount;
    Vector3[] pathPoints;
    int currentIndex = 0;
    float t = 0f;
    float speed = 30f;
    float startHeight = 55f;
    private Rigidbody rb;
    private float jumpBy = 10.0f;
    private Vector3 jump;
    private float jumpTimer = 0f, jumpTimerMax = 2.5f;
    private bool jumpTimerOn = false, canJump = true;
    [SerializeField] GameObject cam, camPoint;
    private float rotSpeed = 5f;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 4.0f, 0.0f);
    }

    public void LateStart()
    {
        pointCount = lineRenderer.positionCount;
        pathPoints = new Vector3[pointCount];
        lineRenderer.GetPositions(pathPoints);
        transform.position = new Vector3(pathPoints[0].x, startHeight, pathPoints[0].z);
    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.position = new Vector3(camPoint.transform.position.x, cam.transform.position.y, camPoint.transform.position.z);
        Vector3 camDir = transform.position - cam.transform.position;
        Quaternion camTargetRot = Quaternion.LookRotation(camDir);
        cam.transform.rotation = Quaternion.Euler(0f, camTargetRot.eulerAngles.y, 0f);
 
        if (jumpTimerOn)
        {
            jumpTimer += Time.deltaTime;
            if(jumpTimer >= jumpTimerMax) 
            {
                canJump = true;
                jumpTimer = 0f;
                jumpTimerOn = false;
            }
        }

        if(currentIndex < pathPoints.Length - 1)
        {
            float segmentLength = Vector3.Distance(pathPoints[currentIndex], pathPoints[currentIndex + 1]);
            t += (speed / segmentLength) * Time.deltaTime;
            Vector3 newPos = new Vector3(Vector3.Lerp(pathPoints[currentIndex], pathPoints[currentIndex + 1], t).x, transform.position.y, Vector3.Lerp(pathPoints[currentIndex], pathPoints[currentIndex + 1], t).z);
            transform.position = newPos;

            Vector3 direction = pathPoints[currentIndex + 1] - pathPoints[currentIndex];
            if(direction != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direction);

                //Quaternion targetRot = Quaternion.LookRotation(direction);
                //transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, rotSpeed * Time.deltaTime);
            }

            if (t >= 1f)
            {
                t = 0f;
                currentIndex++;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //transform.position += new Vector3(0, jumpBy * Time.deltaTime, 0);
            Jump();
            //StartCoroutine(CameraJumpChange());
        }
    }

    public void Jump()
    {
        if (!canJump)
        {
            return;
        }
            Debug.Log("JUMP");
        rb.AddForce(jump * jumpBy, ForceMode.Impulse);
        canJump = false;
        jumpTimerOn = true;
    }
}
