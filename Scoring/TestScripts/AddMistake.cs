using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMistake : MonoBehaviour
{
    public RunMetrics run;


    private void OnMouseDown()
    {

        run.mistakesTotal++;
    }
}
