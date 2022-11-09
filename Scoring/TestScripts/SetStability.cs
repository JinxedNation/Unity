using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A debug script for setting the stability score
 * \author Rhys Mader 33705134
 * \since 25 April 2022
 */
public class SetStability : MonoBehaviour
{
	[SerializeField]
	[Tooltip("The value to set the stability score to")]
	private float _score;

	public void SetStabilityScore()
	{
		StabilityManager.Instance.AddToStabilityScore(this._score - StabilityManager.Instance.stabilityScore.score);
	}
}
