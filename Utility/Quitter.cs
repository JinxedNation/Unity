using UnityEngine;

/**
 * A help script for quitting the game
 * \author Rhys MAder 33705134
 * \since 26 April 2022
 */
public class Quitter : MonoBehaviour
{
	/**
	 * Quit the game
	 */
	public void Quit()
	{
		Application.Quit();
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#endif
	}
}
