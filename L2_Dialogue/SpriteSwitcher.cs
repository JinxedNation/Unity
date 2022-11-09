using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * @class SpriteSwitcher
 * @brief Used for switching sprites during dialogue
 *
 * @author Chase Percy
 * @date 15/05/2022
 *
 */
public class SpriteSwitcher : MonoBehaviour
{
    [SerializeField] [Tooltip("The image to swap the spites into")]
    private Image image;

    [SerializeField] [Tooltip("The scenes dialogue manager")]
    private DialogueManager dialogueManager;

    /**
     * A map of page numbers to sprites
     */
    private Dictionary<int, Sprite> _sprites = new Dictionary<int, Sprite>();

    /**
     * The current page
     */
    private int _currentPage = 1;

    /**
     * If the current dialogue is a SpriteDialogue then the switcher is active
     */
    private bool _active;

    /**
     * Checks if the current dialogue in the manager is a sprite Dialogue or not.
     * If so it fills the _sprite map and returns true, else false.
     */
    private bool Cast()
    {
        var dialogue = dialogueManager.CurrentDialogue();
        if (dialogue != null && dialogue.GetType() == typeof(SpriteDialogue))
        {
            var spriteDialogue = (SpriteDialogue) dialogue;
            foreach (var spriteMap in spriteDialogue.sprites) _sprites.Add(spriteMap.pageToShowOn, spriteMap.sprite);
            return true;
        }

        return false;
    }

    /**
     * Called when a new dialogue is processed, resets the current state.
     */
    public void NewDialogue()
    {
        _currentPage = 1;
        _sprites = new Dictionary<int, Sprite>();
        _active = Cast();
        SetSprite();
    }

    /**
     * Increments the page relative to the current dialogue.
     */
    public void NextPage()
    {
        ++_currentPage;
        SetSprite();
    }

    /**
     * Decrements the current page relative to the current dialogue.
     */
    public void PreviousPage()
    {
        --_currentPage;
        SetSprite();
    }

    /**
     * Sets the sprite if active and there is a sprite for the current page.
     */
    private void SetSprite()
    {
        if (_active && _sprites.ContainsKey(_currentPage))
        {
            image.sprite = _sprites[_currentPage];
            image.gameObject.SetActive(true);
        }
        else
        {
            image.gameObject.SetActive(false);
        }
    }

    /**
     * Shows the image
     */
    public void Show()
    {
        if (_active)
            image.gameObject.SetActive(true);
    }

    /**
     * Hides the image
     */
    public void Hide()
    {
        image.gameObject.SetActive(false);
    }
}