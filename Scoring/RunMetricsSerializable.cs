using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class RunMetricsSerializable
{
    public float levelRunTime = 0f;
    public int mistakesTotal = 0;
    public string sceneName;
    public int attemptNumber;
    public DateTime timeStamp;
}