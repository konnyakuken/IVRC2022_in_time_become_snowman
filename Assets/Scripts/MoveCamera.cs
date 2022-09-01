using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = new Vector3(0, 0, -(SnowBallStatus.Size / 2)); 
    }
}
