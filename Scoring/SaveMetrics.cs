using UnityEngine;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using System.IO;
using System;

/**
 * @class SaveMetrics
 * @brief Saves to file all the required metrics and scoring information for a level
 *
 * @author Michael John
 * @version 01
 * @date 31/03/2022
 *
 */
public class SaveMetrics : MonoBehaviour
{

    [Tooltip("The current level run that is to be saved on completion")]
    public RunMetrics run;

    private GameObject MetricsIndexing;
    private MetricIndexing sceneAttemptLogs;

    private void Start()
    {

        //MetricsIndexing = GameObject.Find("MetricsIndexing");
        //sceneAttemptLogs = MetricsIndexing.GetComponent<MetricIndexing>();
    }

    /**
     * Saves the current level attempt into a serilizable form
     */
    public void SaveRunMetrics()
    {

        int nextFileIndex;
        if (run != null)
        {
            //nextFileIndex = sceneAttemptLogs.incrementActiveSceneAttempt();
            nextFileIndex = MetricIndexing.Instance.incrementActiveSceneAttempt();
            run.timeStamp = DateTime.Now;
            run.attemptNumber = nextFileIndex;
            RunMetricsSerializable form = run.GetSerilizableForm();

            string newFileName = (Application.persistentDataPath + "/" + SceneManager.GetActiveScene().name + nextFileIndex + ".dat");
            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(newFileName, FileMode.Create, FileAccess.Write))
            {
                formatter.Serialize(stream, form);
                stream.Close();
                Debug.Log("SaveMetrics: Successfully saved attempt as : " + newFileName);
            }
        }
    }

}
