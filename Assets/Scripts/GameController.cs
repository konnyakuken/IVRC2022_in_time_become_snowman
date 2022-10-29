using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private bool isTestMode;
    public static bool IsTestMode;
    
    
    private void Start()
    {
        SetBaseStatus();
    }

    public void SetBaseStatus()
    {
        IsTestMode = isTestMode;
    }
}
