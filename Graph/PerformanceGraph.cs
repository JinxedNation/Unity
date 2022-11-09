using UnityEngine;
using System;

/**
 * A graph of the player's performance
 * \author Rhys Mader 33705134
 * \since 17 May 2022
 */
public class PerformanceGraph : LineGraph<SerialisableDate,float>
{
	[Header("Performance Data")]

	[SerializeField]
	[Tooltip("The scene path of the visualised level performance")]
	private string _levelName;

	[SerializeField]
	[Tooltip("The metrics loader this uses to load performance data")]
	private LoadMetrics _metricsLoader;

	[SerializeField]
	[Tooltip("The run metrics this uses to calculate level scores")]
	private RunMetrics _metrics;

	protected override string FormatX(SerialisableDate val)
	{
		return val.Date.ToShortDateString();
	}

	protected override string FormatY(float val)
	{
		return ((int)val).ToString();
	}

	protected override float PositionX(SerialisableDate val)
	{
		return (float)((double)((val.Date - this.MinimumX.Date).Ticks) / (double)((this.MaximumX.Date - this.MinimumX.Date).Ticks));
	}

	protected override float PositionY(float val)
	{
		return (val - this.MinimumY) / (this.MaximumY - this.MinimumY);
	}

	/** Fetch the serialised performance data */
	private void FetchData()
	{
		RunMetricsSerializable old = this._metrics.GetSerilizableForm();
		this._metrics.enabled = false;
		try
		{
			this.LinePoints.Clear();
			int num_attempts = MetricIndexing.Instance.getSceneAttempts(this._levelName);
			Debug.Log(num_attempts.ToString() + " attempts found for " + this._levelName);
			for (int i = 1; i <= num_attempts; ++i)
			{
				this._metricsLoader.loadAttempt(this._levelName, i);
				this._metrics.SetFromSerilizableForm(this._metricsLoader.attempt);
				this.LinePoints.Add(new Point(new SerialisableDate(this._metricsLoader.attempt.timeStamp), this._metrics.levelScore));
			}
		}
		finally
		{
			this._metrics.SetFromSerilizableForm(old);
			this._metrics.enabled = true;
		}
		Debug.Log(this.LinePoints.Count + " points fetched");
	}

	/** Sort the list of points by their timestamp */
	private void SortPointsByX()
	{
		this.LinePoints.Sort(delegate (Point p0, Point p1) { return p0.x.CompareTo(p1.x); });
	}

	public override void UpdateGraph()
	{
		this.FetchData();
		this.SortPointsByX();
		base.UpdateGraph();
	}

	private void Awake()
	{
		this._metricsLoader ??= GameObject.FindObjectOfType<LoadMetrics>();
		this._metrics ??= GameObject.FindObjectOfType<RunMetrics>();
	}
}
