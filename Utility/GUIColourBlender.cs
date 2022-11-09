using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * A helper class that blends between manys colours on a spectrum and tints a linked GUI image
 * \author Rhys Mader 33705134
 * \since 25 April 2022
 */
public class GUIColourBlender : MonoBehaviour
{
	[System.Serializable]
	public struct ColourBlendPair
	{
		public float Value;
		public Color Colour;
	}

	[SerializeField]
	[Tooltip("The image to tint")]
	private Image _image;

	[SerializeField]
	[Tooltip("The colours to blend between and their associated values")]
	private List<ColourBlendPair> _blendPoints;// = new List<ColourBlendPair>();

	private void OnValidate()
	{
		if (this._blendPoints.Count < 1)
			throw new System.ArgumentNullException("Must have at least one blend point");
		this._blendPoints.Sort(delegate(ColourBlendPair lhs, ColourBlendPair rhs)
		{
			if (lhs.Value < rhs.Value)
				return -1;
			else if (lhs.Value > rhs.Value)
				return 1;
			return 0;
		});
		for (int i = 0; i < this._blendPoints.Count - 1; ++i)
			if (this._blendPoints.IndexOf(this._blendPoints[i], i + 1) > -1)
				throw new System.ArgumentException("Cannot have equal blend values");
	}

	/**
	 * Update the linked GUI image tint
	 * \param value The value used to blend colours
	 */
	public void UpdateGUI(float value)
	{
		if (value <= this._blendPoints[0].Value)
		{
			this._image.color = this._blendPoints[0].Colour;
			return;
		}
		if (value >= this._blendPoints[this._blendPoints.Count - 1].Value)
		{
			this._image.color = this._blendPoints[this._blendPoints.Count - 1].Colour;
			return;
		}
		for (int high_index = 1; high_index < this._blendPoints.Count; ++high_index)
		{
			ColourBlendPair high = this._blendPoints[high_index];
			if (high.Value >= value)
			{
				ColourBlendPair low = this._blendPoints[high_index - 1];
				float mix = (value - low.Value) / (high.Value - low.Value);
				this._image.color = Color.Lerp(low.Colour, high.Colour, mix);
				break;
			}
		}
	}
}
