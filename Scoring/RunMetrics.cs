using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

/**
 * @class RunMetrics
 * @brief Contains all the required metrics and scoring information for a level
 *
 * @author Michael John
 * @version 01
 * @date 31/03/2022
 *
 */
public class RunMetrics : MonoBehaviour
{
    [Tooltip("Level elapsed run time")]
    public float levelRunTime = 0f;
    [Tooltip("Level mistakes made during play")]
    public int mistakesTotal = 0;

    public float levelScore
    {
        get { return CalculateScore(); }
    }//get method to calculate the score by using formula game doc and mistakes + time in this class

    [Tooltip("Scenename (Levelname) assigned on start")]
    public string sceneName;

    [Tooltip("Attempt number assigned from metricindexing on save")]
    public int attemptNumber;

    [Tooltip("Time this level was completed, assigned on save")]
    public DateTime timeStamp;


    [Tooltip("Time limit for this level. Finishing before this time passes results in more points")]
    public int maxMinsRunTime = 5;
    private const int secondsInMinute = 60;
    private const int secondToMilliSec = 1000;

    // Start is called before the first frame update
    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
    }

    void Update()
    {
        levelRunTime += Time.deltaTime;
    }

    private float CalculateScore()
    {
        float score = 0;

        //get seconds remaining until set max level run time
        float timePoints = -levelRunTime + (maxMinsRunTime * secondsInMinute);

        //each 0.01 of a second gives 1 point
        timePoints *= 10;
        Debug.Log("timepoints in seconds = " + timePoints);

        float mistakeDeductions = 100 * mistakesTotal;
        Debug.Log("mistakeDeductions = " + mistakeDeductions);

        // 0 is the floor for the score
        if (timePoints - mistakeDeductions > score)
        {
            score = timePoints - mistakeDeductions; 
        }

        return score;
    }

    /**
     * Converts the current Run Metrics into a serializable class - MonoBehaviours can't be serialized
     * Used for saving this level attempt to file
     */
    public RunMetricsSerializable GetSerilizableForm()
    {
        RunMetricsSerializable form = new RunMetricsSerializable();
        form.levelRunTime = levelRunTime;
        form.mistakesTotal = mistakesTotal;
        form.sceneName = sceneName;
        form.attemptNumber = attemptNumber;
        form.timeStamp = timeStamp;
        return form;
    }

    /**
     * Sets this class from a serializable formed object - used for loading run attempt from save
     */
    public void SetFromSerilizableForm(RunMetricsSerializable form)
    {

        levelRunTime = form.levelRunTime;
        mistakesTotal = form.mistakesTotal;
        sceneName = form.sceneName;
        attemptNumber = form.attemptNumber;
        timeStamp = form.timeStamp;
    }


}
