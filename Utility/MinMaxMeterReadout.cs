using UnityEngine;
using UnityEngine.UI;

/**
 * A value readout that reads a meter as a value-max fraction
 */
public class MinMaxMeterReadout : ValueReadout
{
	[SerializeField]
	[Tooltip("The meter to read values from")]
	private Slider _meter;

	[SerializeField]
	[Tooltip("The suffix to append to the end")]
	private string _suffix;

	private void Awake()
	{
		this._meter ??= this.GetComponent<Slider>();
		this._meter.onValueChanged.AddListener(this.ReadMeter);
	}

	private void Start()
	{
		this.ReadMeter(this._meter.value);
	}

	/**
	 * Update the text to a fraction with the given value as the numerator and the max value as denominator
	 */
	public void ReadMeter(float value)
	{
		this.UpdateValue(value.ToString() + " / " + this._meter.maxValue.ToString() + this._suffix);
	}
}
