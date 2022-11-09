using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @class MobileUtility
 * @brief Class to provide general Mobile related utility functions.
 *
 * @author Chase Percy
 * @version 01
 * @date 28/03/2022
 *
 */
static public class MobileUtility
{

    /**
     *  Checks if a handHeld device (phone/tablet) is in use and sets handheldInUse accordingly.
     */
    static public bool HandheldInUse()
    {
        return (SystemInfo.deviceType == DeviceType.Handheld) ? true : false;
    }
}
