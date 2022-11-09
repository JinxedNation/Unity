using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/**
 * A component that manages a canvas as a stackable menu
 * \author Rhys Mader 33705134
 * \since 25 April 2022
 */
[RequireComponent(typeof(Canvas))]
public class MenuManager : MonoBehaviour
{
    [SerializeField] [Tooltip("The UI object to select when this shows")]
    private Selectable _firstSelect;

    [SerializeField]
    [Tooltip(
        "The animator to wait for before transitioning\nWaits for all triggers to be consumed and no active transitions in the first layer")]
    private Animator _animator;

    [Header("Events")] [SerializeField] [Tooltip("The event triggered when this attempts to open")]
    public UnityEvent OnOpenRequest;

    [SerializeField] [Tooltip("The event triggered when this successfully opens")]
    public UnityEvent OnOpenComplete;

    [SerializeField] [Tooltip("The event trigger when this attempts to close")]
    public UnityEvent OnCloseRequest;

    [SerializeField] [Tooltip("The event triggered when this successfully closes")]
    public UnityEvent OnCloseComplete;

    [SerializeField]
    [Tooltip("The event triggered when this attempts to be shown after it opens or a child menu closes")]
    public UnityEvent OnShowRequest;

    [SerializeField]
    [Tooltip("The event triggered when this is successfully shown after it opens or a child menu closes")]
    public UnityEvent OnShowComplete;

    [SerializeField]
    [Tooltip("The event triggered when this attempts to be hidden before it closes or a child menu opens")]
    public UnityEvent OnHideRequest;

    [SerializeField]
    [Tooltip("The event triggered when this is successfully hidden before it closes or a child menu opens")]
    public UnityEvent OnHideComplete;

    public List<GameObject> buttons;

    /**
	 * Whether this menu is currently open
	 */
    public bool IsOpen { get; private set; }

    /**
	 * Whether this menu is currently visible
	 */
    public bool IsVisible
    {
        get { return this._menu?.enabled ?? false; }
    }

    /**
	 * The menu this manages
	 */
    private Canvas _menu;

    /**
	 * The action this menu will perform once it's animator stops
	 */
    public Coroutine CurrentAction { get; private set; }


    /**
	 * The queue of actions left to process
	 */
    private Queue<IEnumerator> _queuedActions = new Queue<IEnumerator>();

    /**
	 * The menu this will show when closed
	 */
    private MenuManager _returnMenu;

    private void Awake()
    {
        this._menu = this.GetComponent<Canvas>();
        this._firstSelect ??= this._menu.GetComponentInChildren<Selectable>();
        this._queuedActions.Enqueue(this.IsVisible ? this.Open(null, false) : this.Close(false));
        SetButtonState(_menu.enabled);
    }

    private void Update()
    {
        this.ProccessTransition();
    }

    /**
	 * Process the next transition in the queue
	 */
    private void ProccessTransition()
    {
        if (this.CurrentAction == null && this._queuedActions.Count > 0)
        {
            this.CurrentAction = this.StartCoroutine(this._queuedActions.Dequeue());
        }
    }

    /**
	 * Wait for the animator to finish transitioning and consume all its triggers
	 */
    private IEnumerator WaitBeforeTransition()
    {
        yield return new WaitWhile(delegate()
        {
            if (this._animator == null)
                return false;
            if (this._animator.IsInTransition(0))
                return true;
            foreach (var param in this._animator.parameters)
                if (param.type == AnimatorControllerParameterType.Trigger && this._animator.GetBool(param.name))
                    return true;
            return false;
        });
    }

    /**
	 * Open this menu
	 * \param parent The menu to return to when this closes (can be null)
	 */
    public void Open(MenuManager parent = null)
    {
        this._queuedActions.Enqueue(this.Open(parent, true));
    }

    /**
	 * Open this menu
	 * \param parent The menu to return to when this closes
	 * \param trigger Whether totrigger the on open event
	 */
    private IEnumerator Open(MenuManager parent, bool trigger)
    {
        SetButtonState(true);
        if (trigger)
            this.OnOpenRequest.Invoke();
        yield return this.WaitBeforeTransition();
        if (!this.IsOpen)
        {
            if (parent != null && parent.IsVisible)
                yield return parent.StartCoroutine(parent.Hide(trigger));
            this._returnMenu = parent;
            this.IsOpen = true;
            if (trigger)
                this.OnOpenComplete.Invoke();
            if (!this.IsVisible)
                yield return this.StartCoroutine(this.Show(trigger));
        }

        this.CurrentAction = null;
    }

    /**
	 * Close this menu
	 */
    public void Close()
    {
        this._queuedActions.Enqueue(this.Close(true));
    }

    /**
	 * Close this menu
	 * \param trigger Whether to trigger the on close event
	 */
    private IEnumerator Close(bool trigger)
    {
        SetButtonState(false);
        if (trigger)
            this.OnCloseRequest.Invoke();
        yield return this.WaitBeforeTransition();
        if (this.IsOpen)
        {
            if (this.IsVisible)
                yield return this.StartCoroutine(this.Hide(trigger));
            this.IsOpen = false;
            if (trigger)
                this.OnCloseComplete.Invoke();
            if (this._returnMenu != null && !this._returnMenu.IsVisible)
                yield return this._returnMenu.StartCoroutine(this._returnMenu.Show(trigger));
        }

        this.CurrentAction = null;
    }

    /**
	 * Show this menu without opening it
	 * \note Best used to reshow this menu after hiding it
	 */
    public void Show()
    {
        this._queuedActions.Enqueue(this.Show(true));
    }

    /**
	 * Show this menu
	 * \param trigger Whether to trigger the on show event
	 */
    private IEnumerator Show(bool trigger)
    {
        if (trigger)
            this.OnShowRequest.Invoke();
        yield return this.WaitBeforeTransition();
        if (!this.IsVisible)
        {
            this._menu.enabled = true;
            ControllerUtility.SelectGameObject(this._firstSelect.gameObject);
            if (trigger)
                this.OnShowComplete.Invoke();
        }

        this.CurrentAction = null;
    }

    /**
	 * Hide this menu without closing it
	 */
    public void Hide()
    {
        this._queuedActions.Enqueue(this.Hide(true));
    }

    /**
	 * Hide this menu
	 * \param trigger Whether to trigger the on hide event
	 */
    private IEnumerator Hide(bool trigger)
    {
        if (trigger)
            this.OnHideRequest.Invoke();
        yield return this.WaitBeforeTransition();
        if (this.IsVisible)
        {
            this._menu.enabled = false;
            if (trigger)
                this.OnHideComplete.Invoke();
        }

        this.CurrentAction = null;
    }

    private void SetButtonState(bool state)
    {
        foreach (var button in buttons)
        {
            button.SetActive(state);
        }
    }
}