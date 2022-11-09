using UnityEngine;
using UnityEngine.UI;

public class CompendiumMenuGenerator : MonoBehaviour
{
	[SerializeField]
	[Tooltip("The data used to generate the compendium")]
	private CompendiumData _data;

	[SerializeField]
	[Tooltip("The prefab that is instantiated for every entry")]
	private GameObject _headerButton;

	[SerializeField]
	[Tooltip("The game object that holds all the header links")]
	private Transform _headerContainer;

	[SerializeField]
	[Tooltip("The text component to insert the header of the shown entry into")]
	private Text _headerText;

	[SerializeField]
	[Tooltip("The text component to insert the body of the shown entry into")]
	private Text _bodyText;

	/**
	 * Generate all the header buttons
	 */
	private void GenerateMenu()
	{
		foreach (CompendiumData.Entry entry in this._data.Entries)
		{
			GameObject entry_link = GameObject.Instantiate(this._headerButton, this._headerContainer);
			entry_link.GetComponentInChildren<Text>().text = entry.Header;
			entry_link.GetComponentInChildren<Button>().onClick.AddListener(delegate ()
			{
				this.ShowEntry(entry.Header);
			});
		}
	}

	/**
	 * Display an entry with a header matching the given string
	 * \param header the header to search for
	 * \throw System.ArgumentException The given header was not found
	 */
	public void ShowEntry(string header)
	{
		foreach (CompendiumData.Entry entry in this._data.Entries)
			if (entry.Header == header)
			{
				this._headerText.text = header;
				this._bodyText.text = entry.Body;
				(this._bodyText.transform as RectTransform).SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this._bodyText.preferredHeight);
				return;
			}
		throw new System.ArgumentException("Entry with header \"" + header + "\" not found");
	}

	/**
	 * Hide the currently shown compendium entry
	 */
	public void HideEntry()
	{
		this._headerText.text = "";
		this._bodyText.text = "";
	}

	private void Awake()
	{
		this.GenerateMenu();
		this.HideEntry();
	}
}
