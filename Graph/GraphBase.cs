using UnityEngine;
using UnityEngine.UI;

/**
 * An abstract base class for labeled 2D graphs
 * \author Rhys Mader 33705134
 * \since 17 May 2022
 */
public abstract class GraphBase<XT, YT> : MonoBehaviour
{
	[Header("Labels")]

	[SerializeField]
	[Tooltip("The text used as the minimum label for the X axis")]
	private Text _minXLabel;

	[SerializeField]
	[Tooltip("The text used as the maximum label for the X axis")]
	private Text _maxXLabel;

	[SerializeField]
	[Tooltip("The text used as the minimum label for the Y axis")]
	private Text _minYLabel;

	[SerializeField]
	[Tooltip("The text used as the maximum labal for the X axis")]
	private Text _maxYLabel;

	[Header("Axes Scale")]

	[SerializeField]
	[Tooltip("The minimum x value used for the label")]
	public XT MinimumX;

	[SerializeField]
	[Tooltip("The maximum x value used for the label")]
	public XT MaximumX;

	[SerializeField]
	[Tooltip("The minimum y value used for the label")]
	public YT MinimumY;

	[SerializeField]
	[Tooltip("The maximum y value used for the label")]
	public YT MaximumY;

	/** Format the given x value into a string for labels
	 * \param val The x value to format
	 * \return The string of the given x value
	 */
	protected virtual string FormatX(XT val)
	{
		return val.ToString();
	}

	/** Format the given y value into a string for labels
	 * \param val The y value to format
	 * \return The string of the given y value
	 */
	protected virtual string FormatY(YT val)
	{
		return val.ToString();
	}

	/** Update the labels with the current minimum and maximum data */
	protected void UpdateLabels()
	{
		this._minXLabel.text = this.FormatX(this.MinimumX);
		this._maxXLabel.text = this.FormatX(this.MaximumX);
		this._minYLabel.text = this.FormatY(this.MinimumY);
		this._maxYLabel.text = this.FormatY(this.MaximumY);
	}

	/** 
	 * Update the entire graph
	 */
	public virtual void UpdateGraph()
	{
		this.UpdateLabels();
	}
}
