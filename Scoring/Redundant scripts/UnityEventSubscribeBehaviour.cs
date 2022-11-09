using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityEventSubscribeBehaviour : MonoBehaviour
{
    public UnityEventRaiseBehaviour eventRaiseBehaviour;
    private MetricsManager metricsManager;

    private void Start()
    {
        if (eventRaiseBehaviour == null)
        {
            eventRaiseBehaviour = GameObject.FindObjectOfType<UnityEventRaiseBehaviour>();
        }

        /*
        if (eventRaiseBehaviour.TestEvent == null)
        {
            eventRaiseBehaviour.TestEvent = new MyEvent();
        }*/

        if (eventRaiseBehaviour.ScoreEvent == null)
        {
            eventRaiseBehaviour.ScoreEvent = new MyEvent();
        }

        // subscribe to the event
        eventRaiseBehaviour.ScoreEvent.AddListener(ScoreEvent_Handler);


        metricsManager = GameObject.Find("Metrics Manager").GetComponent<MetricsManager>();
    }

    /*
    // event handler
    private void TestEvent_Handler(string argument)
    {
        Debug.Log("Unity Event called with argument: " + argument);
    }*/


    // event handler
    private void ScoreEvent_Handler(int argument)
    {
        Debug.Log("Unity Event called with argument: " + argument);
        metricsManager.UpdateScore(argument);
    }
}
