using System.Collections.Generic;
using UnityEngine;

/**
 * @class SpriteDialogue
 * @brief A dialogue variant that contains information to display a sprite.
 * Pages start from one.
 *
 * @author Chase Percy
 * @date 15/05/2022
 *
 */
[CreateAssetMenu(fileName = "Dialogue", menuName = "PopUp/SpriteDialogue", order = 0)]
public class SpriteDialogue : Dialogue
{
    [System.Serializable]
    public class SpriteMap
    {
        [Tooltip("The page to show the image on, starting from 1")]
        public int pageToShowOn;

        [Tooltip("The image to show on this page")]
        public Sprite sprite;
    }

    public List<SpriteMap> sprites;
}