using UnityEngine;
using System;

/**
 * A serialisable version of a date that the Unity Editor can draw in the inspector
 * \author Rhys Mader 33705134
 * \since 19 May 2022
 */
[System.Serializable]
public struct SerialisableDate : IComparable<SerialisableDate>
{
	[SerializeField]
	[Tooltip("The year of this date")]
	[Range(1, 9999)]
	public int year;

	[SerializeField]
	[Tooltip("The month of this date")]
	[Range(1, 12)]
	public int month;

	[SerializeField]
	[Tooltip("The day of this date")]
	[Range(1, 31)]
	public int day;

	/** The date this stores */
	public DateTime Date
	{
		get
		{
			return new DateTime(this.year, this.month, this.day);
		}
		set
		{
			this.year = value.Year;
			this.month = value.Month;
			this.day = value.Day;
		}
	}

	public SerialisableDate(DateTime date)
	{
		this.year = 1;
		this.month = 1;
		this.day = 1;
		this.Date = date;
	}

	public int CompareTo(SerialisableDate other)
	{
		return this.Date.CompareTo(other.Date);
	}

	public override string ToString()
	{
		return this.Date.ToShortDateString();
	}

	/**
	 * Validate the current data
	 * \note Should be called in client's OnValidate method
	 * \throw System.OutOfRangeException The specified date is invalid
	 */
	public void Validate()
	{
		var _ = this.Date;
	}
}
