using UnityEngine;

public class Propeller : MonoBehaviour
{
	[Header("Settings")]

	[Tooltip("The slowest the propeller can push the boat forwards")]
	[Min(0)]
	public float minPropellerForce;

	[Tooltip("The fastest the propeller can push the boat forwards")]
	[Min(0)]
	public float maxPropellerForce;

	[Tooltip("The speed at which the propeller force changes")]
	[Min(0)]
	public float propellerAcceleration;

	[Tooltip("The maximum angle from forward the rudder may be")]
	[Min(0)]
	public float maxRudderAngle;

	[Tooltip("The rate at which the rudder turns")]
	[Min(0)]
	public float rudderTurnSpeed;

	[Header("Sound")]

	[Tooltip("The sound effect looped while this is on")]
	public AudioSource motorSound;

	private float _propellerForce;

	public float PropellerForce
	{
		get
		{
			return this._propellerForce;
		}
		set
		{
			this._propellerForce = Mathf.Clamp(value, this.minPropellerForce, this.maxPropellerForce);
		}
	}

	private float _rudderAngle = 0;
	public float RudderAngle
	{
		get
		{
			return this._rudderAngle;
		}
		private set
		{
			this._rudderAngle = Mathf.Clamp(value, -this.maxRudderAngle, this.maxRudderAngle);
		}
	}

	public Vector3 RudderDirection
	{
		get
		{
			return Quaternion.AngleAxis(this.RudderAngle, Vector3.up) * this.transform.forward;
		}
	}

	public void UpdateHeading(float turn, float move, float delta_time)
	{
		this.RudderAngle += turn * this.rudderTurnSpeed * delta_time;
		this.PropellerForce += move * this.propellerAcceleration * delta_time;
		if (this.PropellerForce > 0 != this.motorSound.isPlaying)
		{
			if (this.motorSound.isPlaying)
			{
				this.motorSound.Stop();
			}
			else
			{
				this.motorSound.Play();
			}
		}
	}

	public void ApplyForce(ref Rigidbody rb)
	{
		rb.AddForceAtPosition(this.RudderDirection * this.PropellerForce, this.transform.position);
		Debug.DrawRay(this.transform.position, this.RudderDirection * this.PropellerForce, Color.blue);
	}

	private void ValidatePropellerForce()
	{
		if (this.maxPropellerForce < this.minPropellerForce)
		{
			throw new System.ArgumentException("Maximum propeller force must be greater than minimum propeller force");
		}
	}

	private void OnValidate()
	{
		this.ValidatePropellerForce();
	}

	private void Awake()
	{
		this.PropellerForce = this.minPropellerForce;
		this.motorSound.loop = true;
	}
}