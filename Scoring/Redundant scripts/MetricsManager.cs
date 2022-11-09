using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using TMPro;
using System.IO;

[Serializable]
public class LevelResults {
    public string name;
    public int levelNumber;
    public int score;
    public string runTime;
}

public class MetricsManager : MonoBehaviour
{

    public TextMeshProUGUI levelText;
    public TextMeshProUGUI runTimeText;
    public TextMeshProUGUI countDownText;
    public TextMeshProUGUI scoreText;

    public List<GameObject> targets;
    //public List<LevelMetrics> levelruns;
    public LevelMetrics currentLevel;
    private int levelrunIndex;

    private List<LevelResults> completedLevelResults;


    //private int level;
    //private int levelRunTime;
    //private int score;
    // Start is called before the first frame update
    void Awake()
    {
        //completedLevelResults = new List<LevelResults>();
        //LoadResultsData();
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        LoadResultsData();
        //LevelMetrics.OnTimeOut += LevelComplete;
        currentLevel.timeOutEvent.AddListener(LevelComplete);


        UpdateLevel();
        /*
        if (!(levelruns.Count > levelrunIndex))
        {
            levelruns.Add(new LevelMetrics());
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRunTime();
        UpdateCountDown();
    }

    public void UpdateScore(int value)
    {
        currentLevel.score += value;
        scoreText.text = "Score: " + currentLevel.score;
    }

    public void UpdateLevel()
    {
        levelText.text = "Level: " + currentLevel.levelNumber;
    }

    public void UpdateCountDown()
    {
        countDownText.text = "Time Left: " + currentLevel.getTimeLeft();
    }

    public void UpdateRunTime()
    {
        runTimeText.text = "Run Time: " + currentLevel.getRunTime();
    }

    public void LevelComplete()
    {
        LevelResults results = new LevelResults();
        results.name = "Test Name";
        results.levelNumber = currentLevel.levelNumber;
        results.score = currentLevel.score;
        results.runTime = currentLevel.getRunTime();
        completedLevelResults.Add(results);
        SaveResultsData();

        countDownText.text = "Level Completed";

        Debug.Log("Metrics manager level complete");
    }

    public void SaveResultsData()
    {
        IFormatter formatter = new BinaryFormatter();
        using (Stream stream = new FileStream(@"Metrics.dat", FileMode.Create, FileAccess.Write))
        {
            formatter.Serialize(stream, completedLevelResults);
            stream.Close();
        }

    }

    public void LoadResultsData()
    {
        if (completedLevelResults == null)
        {

            completedLevelResults = new List<LevelResults>();
            IFormatter formatter = new BinaryFormatter();

            try
            {
                Stream stream = new FileStream(@"Metrics.dat", FileMode.Open, FileAccess.Read);
                completedLevelResults = (List<LevelResults>)formatter.Deserialize(stream);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                Debug.Log("failed to open metrics.dat");

            }
            DebugResultsData();
        }
    }

    public void DebugResultsData()
    {
        Debug.Log("Results Count = " + completedLevelResults.Count);
        foreach (var result in completedLevelResults)
        {
            Debug.Log(result.name);
            Debug.Log(result.levelNumber);
            Debug.Log(result.score);
            Debug.Log(result.runTime);
        }
    }

    /*
    //THink this block is preventing the script running enabled on start
    void OnEnable()
    {
        LevelMetrics.OnTimeOut += LevelComplete;
    }

    void OnDisable()
    {
        LevelMetrics.OnTimeOut -= LevelComplete;
    }
    */
}