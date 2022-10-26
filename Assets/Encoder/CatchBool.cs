using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchBool : MonoBehaviour
{
    public bool Right;
    void OnTriggerStay(Collider other)
    {
        if (Right && other.gameObject.tag == "SnowBall")
        {
            HandCatch.R_catchNow = true;
        }
        if (!Right && other.gameObject.tag == "SnowBall")
        {
            HandCatch.L_catchNow = true;
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (Right && other.gameObject.tag == "SnowBall")
        {
            HandCatch.R_catchNow = false;
        }
        if (!Right && other.gameObject.tag == "SnowBall")
        {
            HandCatch.L_catchNow = false;
        }
    }
}
