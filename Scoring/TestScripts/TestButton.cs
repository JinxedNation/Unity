using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TestButton : MonoBehaviour
{
    //Make sure to attach these Buttons in the Inspector
    public Button AddMistakeButton, EndLevelButton, LoadButton;
    public RunMetrics run;
    public SaveMetrics save;
    public StabilityManager stabilityManager;
    public LoadMetrics load;
    public RunMetricsSerializable form;
    public int attemptNum;

    void Start()
    {
        //Calls the TaskOnClick/TaskWithParameters/ButtonClicked method when you click the Button
        AddMistakeButton.onClick.AddListener(AddMistake);
        EndLevelButton.onClick.AddListener(EndLevel);
        LoadButton.onClick.AddListener(LoadAttempt);
    }

    void AddMistake()
    {
        run.mistakesTotal++;
    }

    void EndLevel()
    {
        save.SaveRunMetrics();
        float score = run.levelScore;
        stabilityManager.AddToStabilityScore(score);
        run.levelRunTime = 0f;
        run.mistakesTotal = 0;
    }

    void LoadAttempt()
    {
        form = load.loadAttempt(SceneManager.GetActiveScene().name, attemptNum);
        if (form != null)
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
