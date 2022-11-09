using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/**
 * Link a GUI slider to the stability score
 * \author Rhys Mader 33705134
 * \since 25 April 2022
 */
[RequireComponent(typeof(Slider))]
public class StabilityMeter : MonoBehaviour
{
	[SerializeField]
	[Tooltip("The GUI blenders to link with the stability score")]
	private List<GUIColourBlender> _blenders;

	/** The slider this links to the stability score */
	private Slider _slider;

	/**
	 * Update the slider's value and appearance using the current stability score
	 */
	private void UpdateMeter()
	{
		this._slider.value = StabilityManager.Instance.stabilityScore.score;
		foreach (GUIColourBlender blender in this._blenders)
			blender.UpdateGUI(StabilityManager.Instance.stabilityScore.score);
	}

	private void Awake()
	{
		this._slider ??= this.GetComponent<Slider>();
		this._slider.interactable = false;
	}

	private void Update()
	{
		this.UpdateMeter();
	}
}
