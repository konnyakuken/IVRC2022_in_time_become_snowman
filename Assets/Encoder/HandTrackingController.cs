using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTrackingController : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        float horizontal = 0;
        if (Input.GetKey(KeyCode.A)) horizontal -= 1;
        if (Input.GetKey(KeyCode.D)) horizontal += 1;
        float veritical = 0;
        if (Input.GetKey(KeyCode.W)) veritical += 1;
        if (Input.GetKey(KeyCode.S)) veritical -= 1;
        float depth = 0;
        if (Input.GetKey(KeyCode.Q)) depth -= 1;
        if (Input.GetKey(KeyCode.E)) depth += 1;
        var pos = new Vector3(horizontal, veritical, depth);

        this.transform.localPosition += pos * Time.deltaTime * 0.1f;
    }
}
