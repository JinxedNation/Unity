using UnityEngine;
using UnityEngine.Events;

/**
 * A dialogue trigger which uses one of two dialogues based on score
 * \author Rhys Mader 33705134
 * \since 3 May 2022
 */
public class ScoreTrigger : DialogueTrigger
{
	[SerializeField]
	[Tooltip("The metrics to get the score from")]
	private RunMetrics _metrics;

	[SerializeField]
	[Tooltip("The dialogue to play when the player succeeds")]
	private Dialogue _successDialogue;

	[SerializeField]
	[Tooltip("The dialogue to play when the player fails")]
	private Dialogue _failDialogue;

	[SerializeField]
	[Tooltip("The event triggered once the success dialogue is closed")]
	public UnityEvent OnSuccessDone;

	[SerializeField]
	[Tooltip("The event triggered once the failure dialogue is closed")]
	public UnityEvent OnFailureDone;

	/**
	 * Play the dialogue corresponding to the current score
	 */
	public void PlayDialogue()
	{
		if (this._metrics.levelScore > 0)
		{
			this.QueueDialogue(this._successDialogue);
			this._manager.Menu.OnCloseComplete.AddListener(this.SuccessDone);
		}
		else
		{
			this.QueueDialogue(this._failDialogue);
			this._manager.Menu.OnCloseComplete.AddListener(this.FailureDone);
		}
	}

	/**
	 * The callback invoked when the success dialogue closes
	 */
	private void SuccessDone()
	{
		this._manager.Menu.OnCloseComplete.RemoveListener(this.SuccessDone);
		this.OnSuccessDone.Invoke();
	}

	/**
	 * The callback invoked when the failure dialogue closes
	 */
	private void FailureDone()
	{
		this._manager.Menu.OnCloseComplete.RemoveListener(this.FailureDone);
		this.OnFailureDone.Invoke();
	}
}
