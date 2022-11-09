using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerSupport : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        ControllerUtility.DisableSupport();
    }

    private void OnDestroy()
    {
        ControllerUtility.EnableSupport();
    }
}