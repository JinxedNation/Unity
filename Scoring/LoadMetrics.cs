using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/**
 * @class LoadMetrics
 * @brief Loads an attempt for a given level name and attempt
 *
 * @author Michael John
 * @version 01
 * @date 31/03/2022
 *
 */
public class LoadMetrics : MonoBehaviour
{

    [Tooltip("Holds the requested attempt metrics")]
    public RunMetricsSerializable attempt;
    private GameObject MetricsIndexing;
    private MetricIndexing sceneAttemptLogs;

    private void Start()
    {

        //MetricsIndexing = GameObject.Find("MetricsIndexing");
        //sceneAttemptLogs = MetricsIndexing.GetComponent<MetricIndexing>();
    }

    /**
     * Returns the metrics saved to file from the requested scene name and attemptnumber
     */
    public RunMetricsSerializable loadAttempt(string sceneName, int attemptNumber)
    {
        string filename;
        attempt = null; //set to null to return no object loaded

        //get max amount of attempts that is logged against the chosen scene
        int maxSceneAttempts = MetricIndexing.Instance.getSceneAttempts(sceneName);
        Debug.Log("Requested metrics Load: Scene " + sceneName + " maxSceneAttempts " + maxSceneAttempts);
        
        //ensure the requested attempt is within the registered amount of attempt metrics
        if(attemptNumber <= maxSceneAttempts)
        {
            filename = Application.persistentDataPath + "/" + sceneName + attemptNumber.ToString() + ".dat";

            attempt = loadIndexedFile(filename);
        }
        return attempt;
    }

    /**
     * Gets the filename put together in loadAttempt method
     */
    private RunMetricsSerializable loadIndexedFile(string filename)
    {
        IFormatter formatter = new BinaryFormatter();

        try
        {
            Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            return (RunMetricsSerializable)formatter.Deserialize(stream);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
            Debug.Log("LoadMetrics: Failed to open filename : " + filename);

        }
        return null;
    }
}
