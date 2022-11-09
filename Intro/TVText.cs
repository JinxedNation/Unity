using UnityEngine;

/**
 * @class TVText
 * @brief Represents a segment within a tv scene.
 *
 * @author Chase Percy
 * @date 10/05/2022
 */
[CreateAssetMenu(fileName = "TV_Text", menuName = "TV/Text", order = 0)]
public class TVText : ScriptableObject
{
    [Tooltip("The text to display")] [TextArea(3, 10)]
    public string text;

    [Tooltip("How long the text will be displayed for")]
    public float duration;

    [Tooltip("The audio reading the text")]
    public AudioClip audio;
}