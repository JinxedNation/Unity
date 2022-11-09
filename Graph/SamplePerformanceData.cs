using UnityEngine;
using System;

/**
 * Generates some sample performance data for a performance graph
 * \author Rhys Mader 33705134
 * \since 19 May 2022
 */
public class SamplePerformanceData : MonoBehaviour
{
	[SerializeField]
	[Tooltip("The performance graph to add points to")]
	private PerformanceGraph _graph;

	[SerializeField]
	[Tooltip("The minimum number of points to add to the graph")]
	private int _minSize;

	[SerializeField]
	[Tooltip("The maximum number of points to add to the graph")]
	private int _maxSize;

	[SerializeField]
	[Tooltip("The minimum generated score")]
	private float _minScore;

	[SerializeField]
	[Tooltip("The maximum generated score")]
	private float _maxScore;

	[SerializeField]
	[Tooltip("The minimum date to start at")]
	private SerialisableDate _minStartDate;

	[SerializeField]
	[Tooltip("The maximum date to start at")]
	private SerialisableDate _maxStartDate;

	[SerializeField]
	[Tooltip("The minimum number of days between dates")]
	[Min(0)]
	private float _minDateIncrement;

	[SerializeField]
	[Tooltip("The maximum number of days between dates")]
	private float _maxDateIncrement;

	/** Generate some random points for the performance graph */
	private void GeneratePoints()
	{
		float min_score = this._graph.AutoMinimumY ? this._minScore : Mathf.Max(this._graph.MinimumY, this._minScore);
		float max_score = this._graph.AutoMaximumY ? this._maxScore : Mathf.Min(this._graph.MaximumY, this._maxScore);
		DateTime date = new DateTime(0).AddDays(UnityEngine.Random.Range(
			this.TotalDaysOfDate(this._minStartDate.Date), 
			this.TotalDaysOfDate(this._maxStartDate.Date)));
		for (int i = UnityEngine.Random.Range(this._minSize, this._maxSize); i > 0; --i)
		{
			this._graph.LinePoints.Add(new PerformanceGraph.Point(new SerialisableDate(date), UnityEngine.Random.Range(min_score, max_score)));
			date = date.AddDays(UnityEngine.Random.Range(this._minDateIncrement, this._maxDateIncrement));
			if (date > DateTime.Today)
				break;
		}
	}

	/**
	 * Validate in minimum and maximum pair of variables
	 * \param min The minimum value
	 * \param max The maximum value
	 * \param min_name The name of the minimum variable
	 * \param max_name The name of the maximum variable
	 * \throw System.ArgumentException The given minimum is greater than the given maximum
	 */
	private void ValidateMinMax(float min, float max, string min_name, string max_name)
	{
		if (min > max)
			throw new System.ArgumentException(min_name + " cannot be greater than " + max_name);
	}

	/**
	 * Calculate the total days leading up to the given date
	 * \param date The date to total the days of
	 * \return The total numbe of days leading up to the given date
	 */
	private float TotalDaysOfDate(DateTime date)
	{
		return (float)(date - new DateTime(0)).TotalDays;
	}

	private void OnValidate()
	{
		this._minStartDate.Validate();
		this._maxStartDate.Validate();
		this.ValidateMinMax(this._minSize, this._maxSize, "Min Size", "Max Size");
		this.ValidateMinMax(this._minScore, this._maxScore, "Min Score", "Max Score");
		this.ValidateMinMax(this.TotalDaysOfDate(this._minStartDate.Date), this.TotalDaysOfDate(this._maxStartDate.Date), "Min Start Date", "Max Start Date");
		this.ValidateMinMax(this._minDateIncrement, this._maxDateIncrement, "Min Date Increment", "Max Date Increment");
	}

	private void Awake()
	{
		this._graph ??= this.GetComponentInChildren<PerformanceGraph>();
		if (this.enabled)
			this.GeneratePoints();
	}

	/** Only here to force the editor to show the enabled checkbox */
	private void OnEnable() { }
}
