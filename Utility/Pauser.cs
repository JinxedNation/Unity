using UnityEngine;

/**
 * @class Pauser
 * @brief Class to provide general Time related utility functions.
 *
 * @author Chase Percy
 * @version 01
 * @date 26/03/2022
 *
 */
public class Pauser : MonoBehaviour
{
    public bool IsPaused
	{
        get
		{
            return Time.timeScale == 0;
		}
	}

    /**
     *  Pauses Time (Time.timeScale) and pauses the audio listener.
     */
    public void Pause()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
    }

    /**
     *  Unpauses time (Time.timeScale) and the audio listener.
     */
    public void Unpause()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
    }
}
