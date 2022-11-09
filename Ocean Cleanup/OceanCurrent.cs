using UnityEngine;

[RequireComponent(typeof(Collider))]
public class OceanCurrent : MonoBehaviour
{
	[Tooltip("The force at which to push on things")]
	public Vector3 force;

	private void OnTriggerStay(Collider other)
	{
		other.attachedRigidbody.AddForce(this.force);
	}
}
