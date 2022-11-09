using System;
using UnityEngine;

/**
 * @class RecyclingManager
 * @brief Class to manage the Recycling Scene
 *
 * @author Chase Percy
 * @date 26/04//2022
 *
 */
public class RecyclingManager : DialogueTrigger
{
    [SerializeField] [Tooltip("The ending dialogue")]
    private Dialogue endDialogue;

    private bool _done;

    /**
     * Inits the UI
     */
    private void Start()
    {
        ControllerUtility.DisableSupport();
        var item = Collected.Instance.GetLast();
        if (item != null) FindObjectOfType<UIManager_Recycle>().UpdateImage(item.GetName(), item.GetImage());
    }

    /**
     * Checks if the items have been recycled
     */
    private void Update()
    {
        if (Collected.Instance.GetCollected().Count <= 0)
        {
            QueueDialogue(endDialogue);
            _manager.Menu.OnCloseComplete.AddListener(EndScene);
        }

        if (_done)
        {
            LocalDestroy();
            SceneUtility.ExitSceneTo("hub");
            _done = false;
        }
    }

    /**
     * Destroys the DontDestroy Game Objects in the Collected Instance
     */
    private void LocalDestroy()
    {
        var items = Collected.Instance.GetCollected();

        foreach (var item in items) Destroy(item.GetComponentInParent<Transform>().gameObject);

        items.Clear();
    }

    /**
     * Ends the scene, done is used to stop the hub glitching after transition.
     */
    public void EndScene()
    {
        _manager.Menu.OnCloseComplete.RemoveListener(EndScene);
        _done = true;
    }

    private void OnDestroy()
    {
        ControllerUtility.DisableSupport();
    }
}