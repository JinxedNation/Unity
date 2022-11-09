using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/**
 * @class TVManager
 * @brief Manages the tv scene transitions.
 *
 * @author Chase Percy
 * @date 10/05/2022
 */
public class TVManager : MonoBehaviour
{
    [SerializeField] [Tooltip("The scenes to display")]
    private List<TVScene> scenes = new List<TVScene>();

    [SerializeField] [Tooltip("The text game object to toggle on/off")]
    private GameObject text;

    /**
     * If the tv is on
     */
    private bool _on;

    private void Start()
    {
        text.SetActive(false);
        Invoke(nameof(ShowText), 5);
    }

    private void Update()
    {
        if (scenes.Count == 0 && _on)
        {
            _on = false;
            SceneUtility.ExitSceneTo("hub");
        }
        else if (_on && scenes.First().IsComplete())
        {
            scenes.First().gameObject.SetActive(false);
            scenes.Remove(scenes.First());
            if (scenes.Count > 0)
                scenes.First().gameObject.SetActive(true);
        }
    }

    /**
     * Shows the tv scenes
     */
    private void ShowText()
    {
        text.SetActive(true);
        scenes.First().gameObject.SetActive(true);
        _on = true;
    }
}