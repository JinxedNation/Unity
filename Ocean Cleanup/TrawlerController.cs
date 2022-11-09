using UnityEngine;
using System.Collections.Generic;

public class TrawlerController : MonoBehaviour
{
	[Tooltip("The rigidbody to control")]
	public Rigidbody rb;

	[Tooltip("Scale factor used when turning left")]
	public float leftScale = 1;

	[Tooltip("Scale factor used when turning right")]
	public float rightScale = 1;

	[Tooltip("The minimum angle required to apply either turn scaling")]
	[Range(0, 180)]
	public float angleTolerance;

	[Tooltip("The minimum angle difference required to turn the propellers")]
	[Min(0)]
	public float turnTolerance;

	public Vector3 TargetVelocity { get; set; }

	private List<Propeller> _propellers = new List<Propeller>();

	private void Awake()
	{
		this._propellers.AddRange(this.GetComponentsInChildren<Propeller>());
	}

	private void FixedUpdate()
	{
		foreach (Propeller p in this._propellers)
		{
			float turn = Vector3.SignedAngle(p.RudderDirection, this.TargetVelocity.sqrMagnitude == 0 ? p.transform.forward : this.TargetVelocity, Vector3.up)
				/ p.rudderTurnSpeed
				/ Time.fixedDeltaTime;
			if (Mathf.Abs(turn) < this.turnTolerance)
				turn = 0;
			float move = (this.TargetVelocity.magnitude - this.rb.velocity.magnitude)
				* (p.RudderAngle < -this.angleTolerance ? this.leftScale : 1)
				* (p.RudderAngle > this.angleTolerance ? this.rightScale : 1)
				/ p.propellerAcceleration
				/ Time.fixedDeltaTime;
			if (this.TargetVelocity.sqrMagnitude == 0)
				move = -Mathf.Abs(move);
			//Debug.Log("Turn: " + turn);
			//Debug.Log("Move: " + move);
			p.UpdateHeading(turn, move, Time.fixedDeltaTime);
			p.ApplyForce(ref this.rb);
		}
		Debug.DrawRay(this.transform.position, this.TargetVelocity, Color.magenta);
	}
}
