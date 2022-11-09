using UnityEngine;
using UnityEngine.Events;
using System;

/**
 * @class LevelInterface
 * @brief Interfaces with the level to add mistakes and end the level with the scoring system
 *
 * @author Michael John
 * @version 01
 * @date 31/03/2022
 *
 */
public class LevelInterface : MonoBehaviour
{
    private RunMetrics run;
    private SaveMetrics save;
    private GameObject Scoring;

    [Serializable]
    public class StringEvent : UnityEvent<float> { }

    [Tooltip("The event called the level is complete and score is calculated")]
    public UnityEvent<float> scoringComplete;

    private void Start()
    {
        Scoring = GameObject.Find("Scoring");
        run = Scoring.GetComponent<RunMetrics>();
        save = Scoring.GetComponent<SaveMetrics>();
    }

    public void AddMistake()
    {
        run.mistakesTotal++;
    }

    public void RunComplete()
    {
        save.SaveRunMetrics();
        float score = run.levelScore;
        StabilityManager.Instance.AddToStabilityScore(score);
        //scoringComplete.Invoke(score);
    }

}
