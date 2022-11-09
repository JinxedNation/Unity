using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/**
 * @class EnvironmentalDecay
 * @brief Used for decaying the total scores by the player, to apply a constant negative to the score
 *          This value can be used to show a build up of trash over time
 *
 * @author Michael John
 * @version 01
 * @date 31/03/2022
 *
 */
public class EnvironmentalDecay : MonoBehaviour
{

    [Tooltip("Stability Score decays this value per second")]
    public float scorePerSecondDecay;// = 1f;

    [Tooltip("Reference to Stability Manager")]
    public StabilityManager stabManager;

    [Tooltip("Minimum Value The range of environmental Decay is added to")]
    public float min;// = 0f;

    [Tooltip("Maximum Value The Highest of environmental score can represent")]
    public float max;// = 1f;

    [Tooltip("Maximum Possible stability score that will be clamped to")]
    public float maxStabilityScore;// = 10000f;

    [Tooltip("Elapsed time since mini game incremented the Stability score")]
    public TimeSpan elapsedTime;

    [Tooltip("Scaled Stability score to be displayed between min and max")]
    public float stabScore;


    private void Start()
    {
        //get the elapsed time since a end of level event has saved a timeStamp to the Stability Manager
        elapsedTime = DateTime.Now - stabManager.stabilityScore.timeStamp;
        //do the intial decayed update
        UpdateDecay((float)elapsedTime.TotalSeconds);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDecay(Time.deltaTime);
    }

    /**
     * Applies a decay to the stability score for effects
     */
    void UpdateDecay(float elapsedTime)
    {
        float decay = (float)elapsedTime * scorePerSecondDecay;

        if (stabManager.stabilityScore.score - decay < stabManager.minStabilityScore)
        {
            stabManager.stabilityScore.score = stabManager.minStabilityScore;
        }
        else
        {
            stabManager.stabilityScore.score -= decay;
        }

        //contains a local scaled stability score between min/max properties
        //used for the lerpedColor particle system in the hub
        stabScore = ScaleStabilityScore();
    }

    /**
     * Scales the actual stability score from Stability Metrics to something that can be used in the hub
     */
    private float ScaleStabilityScore()
    {
        float stabScore = stabManager.stabilityScore.score;
        if(stabScore > maxStabilityScore)
        {
            stabScore = maxStabilityScore;
        }
         
        return (stabScore / maxStabilityScore) * max + min;
    }


}
