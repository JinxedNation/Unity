using UnityEngine;

public class Floater : MonoBehaviour
{
	[Tooltip("The power of buoyancy for this floater")]
	[Min(0)]
	public float floatPower;

	public bool IsUnderwater { get; private set; } = false;

	public void UpdateBoyancy(ref Rigidbody rb, float water_height)
	{
		float diff = this.transform.position.y - water_height;
		this.IsUnderwater = diff < 0;
		if (this.IsUnderwater)
		{
			rb.AddForceAtPosition(Vector3.up * this.floatPower * Mathf.Abs(diff), this.transform.position);
		}
	}
}