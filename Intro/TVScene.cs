using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/**
 * @class TVScene
 * @brief Manages the tv text transitions
 *
 * @author Chase Percy
 * @date 10/05/2022
 */
public class TVScene : MonoBehaviour
{
    [Tooltip("The text to display")] public List<TVText> displayText;

    [Tooltip("The text to edit")] public Text onScreenText;

    /**
     * Represents if the scene is finished or not
     */
    private bool _isComplete;

    /**
     * How long a tv text will display for
     */
    private float _timer;

    private void Update()
    {
        if (displayText.Count == 0 && _timer <= 0)
        {
            _isComplete = true;
        }
        else if (_timer <= 0)
        {
            var newText = displayText.First();
            displayText.Remove(newText);

            onScreenText.text = newText.text;
            _timer = newText.duration;
            if (newText.audio)
                GetComponent<AudioSource>().PlayOneShot(newText.audio);
        }

        if (_timer > 0) _timer -= Time.deltaTime;
    }

    /**
     * Returns if this tv scene is compete or not
     */
    public bool IsComplete()
    {
        return _isComplete;
    }
}