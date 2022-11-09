using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @class MobileManager
 * @brief Class to manage Mobile (handheld) specific features, such as custom UI.
 *
 * @author Chase Percy
 * @version 01
 * @date 24/03/2022
 *
 */
public class MobileManager : MonoBehaviour
{
    [Tooltip("The mobile pause canvas.")]
    [SerializeField] private GameObject mobileOverlay = null;   /// UI overlay object.

    /**
     *  Called on start up, checks if the mobile overlay and landscape mode should be used.
     */
    void Start()
    {
        if (MobileUtility.HandheldInUse())
        {
            enableMobileOverlay();
            Shader.EnableKeyword("WATER_EDGEBLEND_OFF");
        }
    }

    /**
     *  enables the mobile overlay game object.
     */
    void enableMobileOverlay()
    {
        if (mobileOverlay != null)
        {
            mobileOverlay.SetActive(true);
        }
    }
}
