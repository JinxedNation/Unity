using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
using System;

/**
 * @class MetricIndexing
 * @brief Singleton that keeps a index of all attempts to be used for gather metrics data
 *          increments index number per scene and saves to file immediately
 *
 * @author Michael John
 * @version 01
 * @date 31/03/2022
 *
 */
public sealed class MetricIndexing
{
    private static MetricIndexing instance;
    //private static readonly object padlock = new object();
    private string indexFilename = Application.persistentDataPath + "/Index.dat";

    MetricIndexing()
    {
        //if no index file exists, create one
        if (!File.Exists(indexFilename))
        {
            SaveIndexFile();
        }
        loadIndexFile();
    }

    public static MetricIndexing Instance
    {
        get
        {
            //lock (padlock)
            //{
                if (instance == null)
                {
                    instance = new MetricIndexing();
                }
                return instance;
            //}
        }
    }

    //maintains the amount of attempts completed against a scene
    private Dictionary<string, int> sceneAttempts = new Dictionary<string, int>();

    /**
     * Allows access minium access to the dictionary to get a scene names count of attempts saved to file as metrics
     */
    public int getSceneAttempts(string sceneName)
    {
        int attempts = 0;
        if (sceneAttempts.ContainsKey(sceneName))
        {
            attempts = sceneAttempts[sceneName];
        }
        return attempts;
    }

    /**
     * Increments count of saved metrics against the current scene
     */
    public int incrementActiveSceneAttempt()
    {
        if (sceneAttempts.ContainsKey(SceneManager.GetActiveScene().name))
        {
            sceneAttempts[SceneManager.GetActiveScene().name] += 1;
        }
        else
        {
            sceneAttempts.Add(SceneManager.GetActiveScene().name, 1);
        }
        SaveIndexFile();

        return sceneAttempts[SceneManager.GetActiveScene().name];
    }

    /**
     * Load the index file on start and restore the dictionary
     */
    private void loadIndexFile()
    {
        IFormatter formatter = new BinaryFormatter();

        try
        {
            using (Stream stream = new FileStream(indexFilename, FileMode.Open, FileAccess.Read))
            {
                sceneAttempts = (Dictionary<string, int>)formatter.Deserialize(stream);
                stream.Close();
            }
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
            Debug.Log("LoadStability: Failed to open filename : " + indexFilename);

        }
    }

    /**
     * Saves the dictionary to file
     */
    public void SaveIndexFile()
    {
        IFormatter formatter = new BinaryFormatter();
        using (Stream stream = new FileStream(indexFilename, FileMode.Create, FileAccess.Write))
        {
            formatter.Serialize(stream, sceneAttempts);
            stream.Close();
            Debug.Log("LoadStability: Successfully saved indexfile");
        }

    }



    /**
     * Deletes all the scene attmpt files and sets sceneAttempts dictionary to 0
     */
    public void ClearMetrics()
    {
        foreach (KeyValuePair<string, int> scene in sceneAttempts)
        {
            string filename;
            for(int i = 0;  i <= scene.Value; i++)
            {
                filename = Application.persistentDataPath + "/" + scene.Key + i.ToString() + ".dat";
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }
            }
        }
        //sceneAttempts = new Dictionary<string, int>();
        sceneAttempts.Clear();

        SaveIndexFile();
    }
}