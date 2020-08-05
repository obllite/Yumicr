using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform player_;
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player_.position.x, player_.position.y, transform.position.z);
    }
}
