using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float smoothing = 0.2f;
    [SerializeField] Vector3 offset;


    //private void LateUpdate()
    //{
    //    Vector3 desiredPos = player.position + offset;
    //    Vector3 smoothedPos = Vector3.Lerp(player.position, desiredPos, smoothing);
    //    transform.position = smoothedPos;
    //
    //    transform.LookAt(player);
    //}

    private void LateUpdate()
    {
        //transform.position = player.position + offset;

        //Quaternion targetRot = Quaternion.LookRotation(player.forward);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, smoothing * Time.deltaTime);

        //transform.LookAt(player);
        //transform.Translate(Vector3.right * Time.deltaTime);

        //transform.RotateAround(player.position, player.forward, Time.deltaTime * smoothing);
    }
}
