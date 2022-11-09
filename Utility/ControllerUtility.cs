using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/**
 * @class ControllerUtility
 * @brief Class to provide general Controller related utility functions.
 *
 * @author Chase Percy
 * @version 01
 * @date 26/03/2022
 *
 */
static public class ControllerUtility
{
    private static bool _disabled;

    /**
     *  Selects a game object if a gamepad is in use, else sets it to null. Used for simplified UI management.
     */
    static public void SelectGameObject(GameObject gameObject)
    {
        if (!_disabled && isGamepadInUse() && !MobileUtility.HandheldInUse())
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    /**
     *  Checks if a gamepad is in use
     */
    static public bool isGamepadInUse()
    {
        return Gamepad.current != null;
    }

    /**
     * Disables controller support
     */
    static public void DisableSupport()
    {
        _disabled = true;
    }

    /**
     * Enables controller support
     */
    static public void EnableSupport()
    {
        _disabled = false;
    }
}