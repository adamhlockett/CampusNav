using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMinimapPos : MonoBehaviour
{
    [SerializeField] GameObject player;
    void Update()
    {
        this.transform.position = new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z);
    }
}
