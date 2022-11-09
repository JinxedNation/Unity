using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompendiumEntryLink : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The header of the compendium entry to link to")]
    public string LinkedHeader;

	[SerializeField]
	[Tooltip("The compendium to open when this is followed\nLeave null to search for it in the scene")]
    private CompendiumMenuGenerator _compendium;

	/**
	 * The menu containing the compendium
	 */
    private MenuManager _compendiumMenu;

	/**
	 * The menu that contains this
	 */
	private MenuManager _parentMenu;

	private void Awake()
	{
		this._compendium ??= Object.FindObjectOfType(typeof(CompendiumMenuGenerator)) as CompendiumMenuGenerator;
		this._compendiumMenu = this._compendium.GetComponentInParent<MenuManager>();
		this._parentMenu = this.GetComponentInParent<MenuManager>();
	}

	/**
	 * Open the compendium to the linked entry
	 */
	public void OpenEntry()
	{
		if (this.LinkedHeader == "")
			return;
		this._compendiumMenu.Open(this._parentMenu);
		this._compendium.ShowEntry(this.LinkedHeader);
	}
}
