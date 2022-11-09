using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject target;
    public RunMetrics run;
    //private Rigidbody targetRb;
    //private GameManager gameManager;
    private MetricsManager metricsManager;
    //public int pointValue;
    public int level;
    public bool positiveChoice;

    // Start is called before the first frame update
    void Start()
    {
        //gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        metricsManager = GameObject.Find("Metrics Manager").GetComponent<MetricsManager>();
        run = GameObject.Find("Test Level").GetComponent<RunMetrics>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        /*
        if (positiveChoice)
        {
            metricsManager.incrementPositiveChoice(level);
        }
        else
        {
            metricsManager.incrementNegativeChoice(level);
        }
        //gameManager.UpdateScore(pointValue);
        */
        run.mistakesTotal++;
    }
}
