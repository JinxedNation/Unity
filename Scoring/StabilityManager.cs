using UnityEngine;
using System;
using System.IO;
using UnityEngine.Events;

[Serializable]
public class StabilityScoreType
{
    public StabilityScoreType()
    {
        score = 0f;
        timeStamp = DateTime.Now;
    }

    public float score;
    public DateTime timeStamp;
}

/**
 * @class StabilityManager
 * @brief Singleton that controls loading and saving stability score for progression
 * @author Michael John
 * @version 01
 * @date 31/03/2022
 *
 */
public sealed class StabilityManager : MonoBehaviour
{
    private static StabilityManager _instance;

    public static StabilityManager Instance => _instance;

    [Tooltip("Event called on stability score added to")]
    public UnityEvent stabilityEvent;


    [Tooltip("Current stability score")] public StabilityScoreType stabilityScore;

    [Tooltip("Floor value for stability score after decay applied")]
    public float minStabilityScore = 0f;

    //filename stability score progression saved as
    private string stabilityFileName;

    /**
     * Ensure only one instance exists
     * \author Rhys Mader 33705134
     */
    private void EnsureOnlyOne()
    {
        if (_instance == null)
            _instance = this;
        if (Instance != this)
            Destroy(this);
    }

    private void Awake()
    {
        EnsureOnlyOne();
        stabilityFileName = Application.persistentDataPath + "/Stability.dat";
    }

    // Start is called before the first frame update
    private void Start()
    {
        if (stabilityEvent == null) stabilityEvent = new UnityEvent();
        if (!File.Exists(stabilityFileName))
        {
            //some error alert or event here
            //or could be a new game start and no index saved ever
            stabilityScore = new StabilityScoreType();
            stabilityScore.timeStamp = DateTime.Now;
            stabilityScore.score = 0;
        }
        else
        {
            stabilityScore = LoadStability.loadStabilityScore(stabilityFileName);
        }
    }

    /**
     * Clear the stability score
     */
    public void ClearStability()
	{
        stabilityScore = new StabilityScoreType();
        stabilityScore.timeStamp = DateTime.Now;
        stabilityScore.score = 0;
        SaveStability.SaveStabilityData(stabilityScore, stabilityFileName);
    }

    /**
     * Adds value to stability score And saves to file
     */
    public void AddToStabilityScore(float value)
    {
        stabilityScore.score += value;
        stabilityScore.timeStamp = DateTime.Now;
        SaveStability.SaveStabilityData(stabilityScore, stabilityFileName);

        if (stabilityEvent != null) stabilityEvent.Invoke();
    }
}