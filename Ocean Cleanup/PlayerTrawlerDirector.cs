using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTrawlerDirector : MonoBehaviour
{
	[Tooltip("How fast the trawlers should move")]
	[Min(0)]
	public float targetSpeed;

	[Header("Flocking")]

	[Tooltip("Whether to use flocking forces\n(Currently broken)")]
	public bool enableFlocking;

	[Tooltip("The desired displacement bewteen trawlers")]
	[Min(0)]
	public Vector2 targetDisplacement;

	[Header("Controls")]

	[Tooltip("The component to get player input from")]
	public PlayerInput input;

	[Tooltip("Name of the action to get movement from")]
	public string moveName = "Player/Move";

	private InputAction _moveAction;

	private List<TrawlerController> _trawlers = new List<TrawlerController>();

	private void Awake()
	{
		this._moveAction = this.input.actions[this.moveName];
		this._trawlers.AddRange(this.GetComponentsInChildren<TrawlerController>());
	}

	private void Update()
	{
		Vector2 move = this._moveAction.ReadValue<Vector2>();
		Vector3 input_velocity = new Vector3(move.x, 0, move.y);
		Vector3 total_heading = Vector3.zero;
		foreach (TrawlerController trawler in this._trawlers)
		{
			total_heading += trawler.transform.forward;
		}
		input_velocity = Quaternion.LookRotation(total_heading.normalized, Vector3.up) * input_velocity;

		Vector3 flock_grouping = Vector3.zero;
		foreach (TrawlerController trawler in this._trawlers)
		{
			if (this.enableFlocking)
			{
				flock_grouping = Vector3.zero;
				foreach (TrawlerController other in this._trawlers)
				{
					if (other == this)
						continue;
					Vector3 diff = other.transform.position - trawler.transform.position;
					Vector3 diff_x = Vector3.Project(diff, trawler.transform.right);
					if (diff_x.sqrMagnitude > this.targetDisplacement.x * this.targetDisplacement.x)
						flock_grouping += diff_x.normalized * (diff_x.magnitude - this.targetDisplacement.x);
					Vector3 diff_z = Vector3.Project(diff, trawler.transform.forward);
					if (diff_z.sqrMagnitude > this.targetDisplacement.y * this.targetDisplacement.y)
						flock_grouping += diff_z.normalized * (diff_z.magnitude - this.targetDisplacement.y);
				}
			}
			trawler.TargetVelocity = (input_velocity + flock_grouping).normalized * this.targetSpeed;
		}
	}
}
