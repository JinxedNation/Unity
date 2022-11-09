using UnityEngine;

/**
 * A helper script which automatically scrolls
 * \author Rhys Mader 33705134
 * \since 4 May 2022
 */
public class AutoScroller : MonoBehaviour
{
	[SerializeField]
	[Tooltip("The rect transform to scroll")]
	private RectTransform _transform;

	[SerializeField]
	[Tooltip("The initial value to use for the y position")]
	private float _initialValue;

	[SerializeField]
	[Tooltip("The number of seconds it takes to scroll to the end\nUse a negative to reverse scroll")]
	private float _duration;

	private void Awake()
	{
		this._transform ??= this.GetComponentInParent<RectTransform>();
	}

	private void OnEnable()
	{
		this._transform.localPosition = new Vector3(this._transform.localPosition.x, 
			this._initialValue * (this._transform.parent as RectTransform).rect.height, 
			this._transform.localPosition.z);
	}

	private void Update()
	{
		this._transform.Translate(Vector3.up * Time.unscaledDeltaTime * (this._transform.parent as RectTransform).rect.height / this._duration);
	}
}
