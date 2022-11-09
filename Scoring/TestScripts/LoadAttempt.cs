using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAttempt : MonoBehaviour
{
    public LoadMetrics load;
    public GameObject level;
    public RunMetrics run;
    public RunMetricsSerializable form;
    public int attemptNum;

    // Start is called before the first frame update
    void Start()
    {
        run = level.GetComponent<RunMetrics>();
        //attemptNum = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {

        form = load.loadAttempt(SceneManager.GetActiveScene().name, attemptNum);
        if(form != null)
        {
            Debug.Log("Runtime: " + form.levelRunTime);
            Debug.Log("mistakes: " + form.mistakesTotal);
            //Debug.Log("score: " + form.levelScore);
            Debug.Log("sceneName: " + form.sceneName);
            Debug.Log("attemptNumber: " + form.attemptNumber);
            Debug.Log("timeStamp: " + form.timeStamp);
        }
        else
        {
            Debug.Log("form == null");
        }
    }
}
