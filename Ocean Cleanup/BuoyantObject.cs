using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class BuoyantObject : MonoBehaviour
{
	[Tooltip("The height of the water")]
	public float waterHeight = 0;

	[Tooltip("Drag applied when underwater")]
	[Min(0)]
	public float underwaterDrag = 1;

	[Tooltip("Angular drag applied when underwater")]
	[Min(0)]
	public float underwaterAngularDrag = 3;

	[Tooltip("Drag applied when in air")]
	[Min(0)]
	public float airDrag = 0;

	[Tooltip("Angular drag applied when in air")]
	[Min(0)]
	public float airAngularDrag = .05f;

	public bool IsUnderwater { get; private set; }

	private Rigidbody _rb;

	private List<Floater> _floaters = new List<Floater>();

	private void Awake()
	{
		this._rb = this.GetComponent<Rigidbody>();
		this._floaters.AddRange(this.GetComponentsInChildren<Floater>());
	}

	private void FixedUpdate()
	{
		this._floaters.RemoveAll(delegate (Floater f) { return f == null; });
		if (this._floaters.Count < 1)
		{
			Object.Destroy(this.gameObject);
		}
		foreach (Floater f in this._floaters)
		{
			f.UpdateBoyancy(ref this._rb, this.waterHeight);
		}
		this.IsUnderwater = this._floaters.Exists(delegate(Floater f) { return f.IsUnderwater; });
		this._rb.drag = this.IsUnderwater ? this.underwaterDrag : this.airDrag;
		this._rb.angularDrag = this.IsUnderwater ? this.underwaterAngularDrag : this.airAngularDrag;
	}
}
