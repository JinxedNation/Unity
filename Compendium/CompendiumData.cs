using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

/**
 * A custom asset for all the compendium data
 * \author Rhys Mader 33705134
 * \since 25 April 2022
 */
[CreateAssetMenu(menuName="Compendium/Data")]
public class CompendiumData : ScriptableObject
{
	[System.Serializable]
	public struct Entry
	{
		public string Header;

		[TextArea(3,100)]
		public string Body;
	}

	[SerializeField]
	[Tooltip("The entries in this compendium")]
	private List<Entry> _entries;

	/**
	 * The list of entries in this compendium
	 */
	public ReadOnlyCollection<Entry> Entries
	{
		get
		{
			return this._entries.AsReadOnly();
		}
	}

	private void OnValidate()
	{
		for (int i = 0; i < this._entries.Count - 1; ++i)
			if (this._entries.FindIndex(i + 1, delegate (Entry entry)
			{
				return entry.Header == this._entries[i].Header;
			}) > -1)
				throw new System.ArgumentException("Cannot have entries with the same header");
	}
}
