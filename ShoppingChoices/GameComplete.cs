using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;
using System.Linq;

/**
 * @class GameComplete
 * @brief Handles the Game Ending back to the hub and explains the choices to the player
 *
 * Borrows from Chase Percy's TransitionManager
 *
 * @author Michael John
 * @version 01
 * @date 18/05/2022
 *
 */
public class GameComplete : DialogueTrigger
{

    [Tooltip("Main camera that folows player to be switched off at game end")]
    public Cinemachine.CinemachineVirtualCamera MainCamera;

    [Tooltip("This camera becomes priority and switches between collected items")]
    public Cinemachine.CinemachineVirtualCamera RegisterCamera;

    //contains list of collected items
    private GameManagement manager;

    [SerializeField] private int CollectedIndex = 0;

    [Tooltip("Turns scoring off which relies on the hub preceding this object.\nEnsure this is false for Build!")]
    public bool ScoringOff = false;


    /**
     * Tracks the transitions current state
     */
    private bool _isDisplaying;

    //loads dialogues for each item type
    private Queue<SpriteDialogue> _dialogues = new Queue<SpriteDialogue>();

    //list of possible dialogues for each item type
    public List<SpriteDialogue> _PossibleDialogues = new List<SpriteDialogue>();

    void Start()
    {
        manager = FindObjectOfType<GameManagement>();

    }

    public void DisplayItem()
    {
        //move to next item in checkout
        RegisterCamera.m_Follow = manager.CollectedItems[CollectedIndex].transform;
        RegisterCamera.m_LookAt = manager.CollectedItems[CollectedIndex].transform;
        CollectedIndex++;
    }


    /**
     * Function Calculates the games score
     */
    public void ScoreEvaluation()
    {
        Debug.Log("Game Complete running");
        MainCamera.m_Priority = 0;
        RegisterCamera.m_Priority = 10;
        ValidateCollectedItems();

        var player = GameObject.Find("Player");
        player.SetActive(false);
        var itemSelector = FindObjectOfType<ItemSelectionManager>();
        itemSelector.NoItemSelected(manager.CollectedItems.Last());


        if (ScoringOff)
        {
            Debug.Log("Scoring is off, this should only be set if testing Scene only");
            
        }
        else
        {
            FindObjectOfType<LevelInterface>().RunComplete();
        }
        
        Transition();
    }

    /**
     * Validates the items collected weren't natural, triggers dialogue if they were.
     */
    private void ValidateCollectedItems()
    {

        foreach (GameObject Item in manager.CollectedItems)
        {
            //move camera to look at item
            var ShoppingScript = Item.GetComponent<ShoppingItem>();

            //load good answer in first posiiton on possible dialogues list
            int index = (int)ShoppingScript.Package;

            if (index != 2)
            {
                FindObjectOfType<LevelInterface>().AddMistake();
            }
            _dialogues.Enqueue(_PossibleDialogues[index]);
        }
    }

    /**
     * Transitions from this scene to the Hub
     */
    private void Transition()
    {

        if (_dialogues.Count > 0)
        {
            //translate camera to next item
            DisplayItem();
            _manager.Menu.OnCloseComplete.AddListener(ProcessDialogue);
            _manager.StartDialogue(_dialogues.Dequeue());
        }
        else
        {
            SceneUtility.ExitSceneTo("Hub");
        }
    }

    /**
     * Processes all the wrong collected item dialogues
     */
    private void ProcessDialogue()
    {
        _manager.Menu.OnCloseComplete.RemoveListener(ProcessDialogue);
        if (_dialogues.Count > 0)
        {

            //translate camera to next item
            DisplayItem();
            _manager.Menu.OnCloseComplete.AddListener(ProcessDialogue);
            _manager.StartDialogue(_dialogues.Dequeue());
        }
        else
        {
            Transition();
        }
    }
}