using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

/**
 * Tracks all the collected plastic and their total mass
 * \author Rhys Mader 33705134
 * \since 11 April 2022
 */
public class PlasticCounter : MonoBehaviour
{
	[Tooltip("The mass of plastic that needs to be collected to end the level")]
	[Min(0)]
	public float massThreshold;

	[Tooltip("The event called when the total mass changes\nThe first argument is the total accmulated mass")]
	public UnityEvent<float> onChange;

	[Tooltip("The event called when the mass threshold is exceeded\nThe first argument is the total accumulated mass")]
	public UnityEvent<float> onExceed;

	private HashSet<Rigidbody> _plastics = new HashSet<Rigidbody>();

	private float _totalMass = 0;

	/**
	 * The total mass of collected plastic
	 */
	public float TotalMass
	{
		get
		{
			return this._totalMass;
		}
		private set
		{
			this._totalMass = Mathf.Max(0, value);
			this.onChange.Invoke(this.TotalMass);
			if (this.TotalMass > this.massThreshold)
				this.onExceed.Invoke(this.TotalMass);
		}
	}

	/**
	 * Add the given rigidbody to the set of collected plastic
	 * \param plastic The rigidbody to add
	 */
	public void AddPlastic(Rigidbody plastic)
	{
		if (this.enabled && this._plastics.Add(plastic))
			this.TotalMass += plastic.mass;
	}

	/**
	 * Remove the given rigidbody from the set of collected plastic
	 * \param plastic The rigidbody to remove
	 */
	public void RemovePlastic(Rigidbody plastic)
	{
		if (this.enabled && this._plastics.Remove(plastic))
			this.TotalMass -= plastic.mass;
	}

	private void OnEnable()
	{
		this.TotalMass = 0;
		this._plastics.Clear();
	}
}
