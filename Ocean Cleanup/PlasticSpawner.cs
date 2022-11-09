using System.Collections.Generic;
using UnityEngine;

public class PlasticSpawner : MonoBehaviour
{
	[Tooltip("The maximum distance from this plastic can spawn along each global axis")]
	[Min(0)]
	public Vector3 size;

	[Tooltip("The minimum number of seconds between spawning waves of plastic")]
	[Min(0)]
	public float minDelay;

	[Tooltip("The maximum number of seconds between spawning waves of plastic")]
	[Min(0)]
	public float maxDelay;

	[Tooltip("The inclusive minimum number of clusters in a wave")]
	[Min(0)]
	public int minSize;

	[Tooltip("The exclusive maximum number of plastic clusters in a wave")]
	[Min(0)]
	public int maxSize;

	[Tooltip("The pool of prefabs randomly pulled from to spawn plastic")]
	public List<GameObject> plasticPrefabs;
	
	private void ValidateMinMax(string name, float min, float max)
	{
		if (min > max)
			throw new System.ArgumentException("Maximum " + name + " cannot be lesser than minimum " + name);
	}

	private void OnValidate()
	{
		this.ValidateMinMax("delay", this.minDelay, this.maxDelay);
		this.ValidateMinMax("size", this.minSize, this.maxSize);
	}

	private void OnEnable()
	{
		this.SpawnPlastic();
	}

	private void OnDisable()
	{
		this.CancelInvoke("SpawnPlastic");
	}

	private void SpawnPlastic()
	{
		Vector3 pos;
		for (int size = Random.Range(this.minSize, this.maxSize); size > 0; --size)
		{
			pos = new Vector3(Random.Range(-this.size.x, this.size.x), Random.Range(-this.size.y, this.size.y), Random.Range(-this.size.z, this.size.z));
			GameObject.Instantiate(
				this.plasticPrefabs[Random.Range(0, this.plasticPrefabs.Count)],
				pos + this.transform.position,
				Random.rotation,
				this.transform);
		}
		this.Invoke("SpawnPlastic", Random.Range(this.minDelay, this.maxDelay));
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube(this.transform.position, this.size * 2);
	}
}
