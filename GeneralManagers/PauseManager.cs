using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

/**
 * @class PauseManager
 * @brief Class to manage Pause Menu specific features, such as UI.
 *
 * @author Chase Percy
 * @version 01
 * @date 24/03/2022
 *
 */
public class PauseManager : MonoBehaviour
{
    [Tooltip("The pause menu canvas.")]
    [SerializeField] private GameObject pauseMenu;  /// Pause Menu canvas game object

    [Tooltip("The initial button to select when opening the pause menu. (Gamepad)")]
    [SerializeField] private GameObject initialButton;  /// The initial button for the event system to select when using gamepad.

    private GameObject returnControlTo = null;  /// The game object for the Pause menu to return control to if there are multiple UI's layered.

    private Pauser _pause;

	private void Awake()
	{
        this._pause = this.GetComponent<Pauser>();
	}

	/**
     *  Toggle for simple pausing usage, i.e. pressing the pause key multiple times.
     */
	public void TogglePause()
    {
        if (this._pause.IsPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    /**
     *  Pauses the game (Time & Sound) and stores the currentSelectedGameObject.
     */
    public void PauseGame()
    {
        this._pause.Pause();
        returnControlTo = EventSystem.current.currentSelectedGameObject;
        displayPauseMenu();
    }

    /**
     *  Resumes the game (Time & Sound) and restores the pre-pause currentSelectedGameObject.
     */
    public void ResumeGame()
    {
        this._pause.Unpause();
        hidePauseMenu();
        ControllerUtility.SelectGameObject(returnControlTo);
    }

    /**
     *  Resumes the game (Time & Sound) and restarts the current scene.
     */
    public void RestartScene()
    {
        this._pause.Unpause();
        SceneUtility.Restart();
    }

    /**
     *  Exits from the current scene (Non-Additive) to the scene specified.
     *  @param sceneName the scene to exit to. 
     */
    public void ExitSceneTo(string sceneName)
    {
        this._pause.Unpause();
        SceneUtility.ExitSceneTo(sceneName);
    }

    /**
     *  Displays the pause menu UI
     */
    private void displayPauseMenu()
    {
        pauseMenu.SetActive(true);
        ControllerUtility.SelectGameObject(initialButton);
    }

    /**
     *  Hides the pause menu UI
     */
    private void hidePauseMenu()
    {
        pauseMenu.SetActive(false);
    }
}