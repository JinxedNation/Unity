using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/**
 * @class MobileUtility
 * @brief A dialogue Manager that controls the displaying and navigating of dialogue objects.
 *
 * @author Chase Percy, Rhys Mader (Design Edits)
 * @version 03
 * @date 23/03/2022
 *
 */
public class DialogueManager : MonoBehaviour
{
    [SerializeField] [Tooltip("The menu that contains the dialogue")]
    private MenuManager _menu;

    public MenuManager Menu
    {
        get { return this._menu; }
    }

    [SerializeField] [Tooltip("The text element where headers are displayed")]
    private Text headerText;

    [SerializeField] [Tooltip("The text element where body content is displayed")]
    private Text bodyText;

    [SerializeField] [Tooltip("The button used to advance the dialogue")]
    private Button continueButton;

    [SerializeField] [Tooltip("The button used to reverse the dialogue")]
    private Button backButton;

    [SerializeField] [Tooltip("The button used to open more info")]
    private Button moreInfoButton;

    private Queue<Dialogue> dialogueQueue = new Queue<Dialogue>();

    /// Queue of dialogue to be processed
    private PageFrame _frame;

    /**
	 * @class MobileUtility
	 * @brief A pageframe object for managing and processing interactions with a dialogues pages.
	 *
	 * @author Rhys Mader
	 * @version 01
	 * @date 23/03/2022
	 *
	 */
    private class PageFrame
    {
        public readonly Dialogue Dialogue;

        /// The dialogue to process
        private int _pageIndex;

        /// The current page index relative to the dialogues page list

        public int PageIndex
        {
            get { return this._pageIndex; }
            set
            {
                if (value < 0 || value >= this.Dialogue.pages.Count)
                {
                    throw new System.IndexOutOfRangeException();
                }

                this._pageIndex = value;
            }
        }

        /// A public pageIndex
        public PageFrame(Dialogue dialogue, int pageIndex = 0)
        {
            if (dialogue == null)
            {
                throw new System.ArgumentNullException("dialogue");
            }

            this.Dialogue = dialogue;
            this.PageIndex = pageIndex;
        }

        /// PageFrame Constructor

        public Dialogue.Page Page
        {
            get { return this.Dialogue.pages[this.PageIndex]; }
        } /// A page from the dialogue's pages relative to pageIndex
    }

    private bool processing
    {
        get { return !this._menu.IsVisible; }
    }

    /// Returns true if the Dialogue Manager is still processing more information.
    private void Awake()
    {
        this._menu.OnShowRequest.AddListener(delegate()
        {
            this.UpdateContent();
            this.UpdateButtons();
        });
        this._menu.OnOpenComplete.AddListener(this.ProcessDialogue);
    }

    /**
	 *  Checks if new dialogue should be processed.
	 */
    void Update()
    {
        if (_menu.IsVisible && ControllerUtility.isGamepadInUse())
        {
            var current = EventSystem.current.currentSelectedGameObject;
            if (current != backButton.gameObject && current != moreInfoButton.gameObject &&
                current != continueButton.gameObject)
            {
                ControllerUtility.SelectGameObject(continueButton.gameObject);
            }
        }
    }

    /**
	 *  Places a dialogue in the queue, and awaits processing of all dialogues before it.
	 *  @param dialogue the dialogue to enqueue.
	 */
    public void StartDialogue(Dialogue dialogue)
    {
        this.dialogueQueue.Enqueue(dialogue);
        if (!this._menu.IsOpen && this.dialogueQueue.Count < 2)
            this._menu.Open();
    }

    /**
	 *  Process the dialogue, displaying it on screen and adding it to the stack to be processed.
	 */
    private void ProcessDialogue()
    {
        if (this.dialogueQueue.Count > 0 /*&& !this.processing*/)
        {
            this._frame = new PageFrame(this.dialogueQueue.Dequeue());
            ControllerUtility.SelectGameObject(this.continueButton.gameObject);
        }
    }

    /**
	 * Advance the dialogue to the next page
	 */
    public void Advance()
    {
        if (this._frame.PageIndex >= this._frame.Dialogue.pages.Count - 1)
        {
            this._menu.Close();
            return;
        }

        ++this._frame.PageIndex;
        this.UpdateContent();
        this.UpdateButtons();
    }

    /**
	 * Roll the dialogue back to the previous page
	 */
    public void Rollback()
    {
        if (this._frame.PageIndex > 0)
        {
            --this._frame.PageIndex;
        }

        this.UpdateContent();
        this.UpdateButtons();
    }

    /**
	 *  Update the UI content
	 */
    private void UpdateContent()
    {
        this.headerText.text = this._frame.Page.header;
        this.bodyText.text = this._frame.Page.body;
    }

    /**
	 *  Updates the button text and sets them active if need be.
	 */
    private void UpdateButtons()
    {
        this.continueButton.GetComponentInChildren<Text>().text =
            (this._frame.PageIndex < this._frame.Dialogue.pages.Count - 1) ? "Continue" : "Close";
        ControllerUtility.SelectGameObject(this.continueButton.gameObject);
        bool has_more_info = (this._frame.Page.compendiumHeader?.Length ?? 0) > 0;
        this.moreInfoButton.gameObject.SetActive(has_more_info);
        this.backButton.gameObject.SetActive(this._frame.PageIndex > 0);
        if (has_more_info)
            this.moreInfoButton.GetComponent<CompendiumEntryLink>().LinkedHeader = this._frame.Page.compendiumHeader;
    }

    private void OnDestroy()
    {
        this.StopAllCoroutines();
    }

    /**
     * Gets the current dialogue at the start of an event, before the page frame has been set.
     */
    public Dialogue CurrentDialogue()
    {
        return dialogueQueue.Peek();
    }
}