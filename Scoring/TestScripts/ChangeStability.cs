using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A test script for easily changing the stability score
 * \author Rhys Mader 33705134
 * \since 25 April 2022
 */
public class ChangeStability : MonoBehaviour
{
	[SerializeField]
	[Tooltip("The amount added to the stability score")]
	private float _amount;

	/**
	 * Add the specified amount to the stability score 
	 */
	public void AddToStability()
	{
		StabilityManager.Instance.AddToStabilityScore(this._amount);
	}
}
