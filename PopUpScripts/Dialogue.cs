using System.Collections.Generic;
using UnityEngine;

/**
 * @class MobileUtility
 * @brief A dialogue object containing pages of dialogue to be displayed.
 *
 * @author Chase Percy, Rhys Mader (Design Edits)
 * @version 02
 * @date 23/03/2022
 *
 */
[CreateAssetMenu(fileName = "Dialogue", menuName = "PopUp/Dialogue", order = 0)]
public class Dialogue : ScriptableObject
{
    /**
     * @class MobileUtility
     * @brief Class to contain the information that should be displayed on a dialogue page.
     *
     * @author Chase Percy, Rhys Mader (Design Edits)
     * @version 01
     * @date 23/03/2022
     *
     */
    [System.Serializable]
    public class Page
    {
        [Tooltip("The header displayed at the top of the dialog box")]
        public string header;

        [Tooltip("The text displayed in the body of the dialog box")]
        [TextArea(3, 10)]
        public string body = "";

        [Tooltip("The other dialogue to call when more info is requested (can be left as none)\nThis is no longer used; use the compendium instead")]
        public Dialogue moreInfo = null;

        [Tooltip("The compendium header to link to when more info is requested\nLeave empty for no link")]
        public string compendiumHeader;
    }

    public List<Page> pages;    /// The pages to be displayed during the dialogue.
}