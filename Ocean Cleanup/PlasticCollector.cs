using UnityEngine;

/**
 * Detects collected plastic and adds it to the plastic counter
 * \author Rhys Mader 33705134
 * \since 11 April 2022
 */
[RequireComponent(typeof(Collider))]
public class PlasticCollector : MonoBehaviour
{
	[Tooltip("The counter to increase the total mass of")]
	public PlasticCounter counter;

	private void OnTriggerEnter(Collider other)
	{
		this.counter.AddPlastic(other.attachedRigidbody);
	}

	private void OnTriggerExit(Collider other)
	{
		this.counter.RemovePlastic(other.attachedRigidbody);
	}

	private void Awake()
	{
		this.counter ??= this.GetComponentInParent<PlasticCounter>();
	}
}
