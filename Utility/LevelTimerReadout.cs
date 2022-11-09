using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTimerReadout : TimerReadout
{
	[SerializeField]
	[Tooltip("The run metrics to fetch timer data from\nLeave null to search scene")]
	private RunMetrics _metrics;

	private float _parTime;

	private void Awake()
	{
		this._metrics ??= Object.FindObjectOfType<RunMetrics>();
	}

	private void Start()
	{
		this._parTime = this._metrics.maxMinsRunTime * -60;
		this.UpdateTime(this._parTime);
	}

	private void Update()
	{
		this.UpdateTime(this._metrics.levelRunTime + this._parTime);
	}
}
