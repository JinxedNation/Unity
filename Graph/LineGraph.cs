using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/**
 * An abstract class that creates a line on a graph
 * \author Rhys Mader 33705134
 * \since 17 May 2022
 */
public abstract class LineGraph<XT,YT> : GraphBase<XT, YT> where XT : IComparable<XT> where YT : IComparable<YT>
{
	[SerializeField]
	[Tooltip("Automatically calculate the minimum x label")]
	private bool _autoMinX;

	public bool AutoMinimumX
	{
		get
		{
			return this._autoMinX;
		}
	}

	[SerializeField]
	[Tooltip("Automatically calculate the maximum x label")]
	private bool _autoMaxX;

	public bool AutoMaximumX
	{
		get
		{
			return this._autoMaxX;
		}
	}

	[SerializeField]
	[Tooltip("Automatically calculate the minimum y label")]
	private bool _autoMinY;

	public bool AutoMinimumY
	{
		get
		{
			return this._autoMinY;
		}
	}

	[SerializeField]
	[Tooltip("Automatically calculate the maximum y label")]
	private bool _autoMaxY;

	public bool AutoMaximumY
	{
		get
		{
			return this._autoMaxY;
		}
	}

	[Header("Line")]

	[SerializeField]
	[Tooltip("The area in which to draw the line")]
	private RectTransform _lineArea;

	[SerializeField]
	[Tooltip("The prefab used to draw points on the line\nIts width and height will be set to the line width\nIts pivot will be translated to the point")]
	private GameObject _pointPrefab;

	[SerializeField]
	[Tooltip("The prefab used to draw segments of the line\nIts width will be set the the segment length\nIts height will be set to the line width\nIts pivot will be translated to the midpoint\nIt will be rotate about its z axis to match the segment angle")]
	private GameObject _linePrefab;

	[SerializeField]
	[Tooltip("The width of the line drawn")]
	[Min(0)]
	public float LineWidth;

	[SerializeField]
	[Tooltip("The colour of the line drawn")]
	public Color LineColour;

	[SerializeField]
	[Tooltip("The colour of the points drawn")]
	public Color PointColour;

	[SerializeField]
	[Tooltip("The values used as points on the line")]
	public List<Point> LinePoints = new List<Point>();

	/** All the game objects that make up the drawn line */
	private HashSet<GameObject> _lineParts = new HashSet<GameObject>();

	/** A pair of x and y values */
	[System.Serializable]
	public struct Point
	{
		public XT x;
		public YT y;
		public Point(XT x, YT y)
		{
			this.x = x;
			this.y = y;
		}
		public override string ToString()
		{
			return "{" + this.x.ToString() + ", " + this.y.ToString() + "}";
		}
	}

	/**
	 * Calculate the normalised position of the given value on the x axis
	 * \param val The x value to calculate the position of
	 * \return The normalised position of the given x value within [0..1]
	 */
	protected abstract float PositionX(XT val);

	/**
	 * Calculate the normalised position of the given value on the y axis
	 * \param val The y value to calculate the position of
	 * \return The normalised position of the given y value within [0..1]
	 */
	protected abstract float PositionY(YT val);

	/**
	 * Calculate the global position of the given point
	 * \param point The point to calculate the global position of
	 * \return The global position of the given point on the line
	 */
	private Vector2 PositionPoint(Point point)
	{
		return Rect.NormalizedToPoint(this._lineArea.rect, new Vector2(this.PositionX(point.x), this.PositionY(point.y)));
	}

	/** Instantiate and transform all the objects required to draw the line based on the current point list */
	protected void DrawLine()
	{
		for (int i = 0; i + 1 < this.LinePoints.Count; ++i)
		{
			GameObject obj = GameObject.Instantiate(this._linePrefab, this._lineArea);
			this._lineParts.Add(obj);
			RectTransform trans = obj.transform as RectTransform;
			Vector2 p0 = this.PositionPoint(this.LinePoints[i]);
			Vector2 p1 = this.PositionPoint(this.LinePoints[i + 1]);
			trans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this.LineWidth);
			trans.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Vector2.Distance(p0, p1));
			Vector2 mid = Vector2.Lerp(p0, p1, .5f);
			trans.localPosition = new Vector3(mid.x, mid.y, trans.position.z);
			trans.rotation *= Quaternion.AngleAxis(Vector2.SignedAngle(Vector2.right, p1 - p0), Vector3.forward);
			obj.GetComponent<Image>().color = this.LineColour;
		}

		foreach (var point in this.LinePoints)
		{
			GameObject obj = GameObject.Instantiate(this._pointPrefab, this._lineArea);
			this._lineParts.Add(obj);
			RectTransform trans = obj.transform as RectTransform;
			trans.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, this.LineWidth);
			trans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this.LineWidth);
			Vector2 p = this.PositionPoint(point);
			trans.localPosition = new Vector3(p.x, p.y, trans.position.z);
			obj.GetComponent<Image>().color = this.PointColour;
		}
	}

	/** Clear the drawn line */
	public void ClearLine()
	{
		foreach (GameObject part in this._lineParts)
			GameObject.Destroy(part);
		this._lineParts.Clear();
	}

	/** Clear and then draw the line */
	public void UpdateLine()
	{
		this.ClearLine();
		this.DrawLine();
	}

	/** Set the minimum x value based on the current point data */
	public void CalculateMinX()
	{
		if (this.LinePoints.Count < 1)
			return;
		XT min = this.LinePoints[0].x;
		for (int i = 1; i < this.LinePoints.Count; ++i)
		{
			XT val = this.LinePoints[i].x;
			if (val.CompareTo(min) < 0)
				min = val;
		}
		this.MinimumX = min;
	}

	/** Set the maximum x value based on the current point data */
	public void CalculateMaxX()
	{
		if (this.LinePoints.Count < 1)
			return;
		XT max = this.LinePoints[0].x;
		for (int i = 1; i < this.LinePoints.Count; ++i)
		{
			XT val = this.LinePoints[i].x;
			if (val.CompareTo(max) > 0)
				max = val;
		}
		this.MaximumX = max;
	}

	/** Set the minimum y value based on the current point data */
	public void CalculateMinY()
	{
		if (this.LinePoints.Count < 1)
			return;
		YT min = this.LinePoints[0].y;
		for (int i = 1; i < this.LinePoints.Count; ++i)
		{
			YT val = this.LinePoints[i].y;
			if (val.CompareTo(min) < 0)
				min = val;
		}
		this.MinimumY = min;
	}

	/** Set the maximum y value based on the current point data */
	public void CalculateMaxY()
	{
		if (this.LinePoints.Count < 1)
			return;
		YT max = this.LinePoints[0].y;
		for (int i = 1; i < this.LinePoints.Count; ++i)
		{
			YT val = this.LinePoints[i].y;
			if (val.CompareTo(max) > 0)
				max = val;
		}
		this.MaximumY = max;
	}

	/** Throw an error if the given game obect does not have the given component
	 * \tparam ComponentT The component type to search for
	 * \param obj The game object to search
	 * \param var_name The variable name reported in the error
	 */
	private void ValidateComponent<ComponentT>(GameObject obj, string var_name) where ComponentT : Component
	{
		if (obj.GetComponent<ComponentT>() == null)
			throw new System.NullReferenceException("Game object in variable " + var_name + " must have a " + typeof(ComponentT).Name + " component");
	}

	public override void UpdateGraph()
	{
		if (this._autoMinX)
			this.CalculateMinX();
		if (this._autoMaxX)
			this.CalculateMaxX();
		if (this._autoMinY)
			this.CalculateMinY();
		if (this._autoMaxY)
			this.CalculateMaxY();
		base.UpdateGraph();
		this.UpdateLine();
	}

	private void OnValidate()
	{
		try
		{
			this.ValidateComponent<Canvas>(this._lineArea.gameObject, "Line Area");
			this.ValidateComponent<RectTransform>(this._pointPrefab, "Point Prefab");
			this.ValidateComponent<Image>(this._pointPrefab, "Point Prefab");
			this.ValidateComponent<RectTransform>(this._linePrefab, "Line Prefab");
			this.ValidateComponent<Image>(this._linePrefab, "Line Prefab");
		}
		catch (System.NullReferenceException) { }
		catch (UnassignedReferenceException) { }
	}
}
