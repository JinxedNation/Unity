using UnityEngine;

/**
 * @class Beach_AudioManager
 * @brief Class to handle sounds for the beach clean up and recycle scenes
 *
 * @author Chase Percy
 * @date 30/04/2022
 *
 */
public class Beach_AudioManager : MonoBehaviour
{
    [SerializeField] [Tooltip("The audio source for the sounds")]
    private AudioSource audioSource;

    [SerializeField] [Tooltip("The click sound")]
    private AudioClip clickSound;

    [SerializeField] [Tooltip("The alert sound")]
    private AudioClip alertSound;

    /**
     * Plays the set click sound
     */
    public void Click()
    {
        if (audioSource && clickSound)
            audioSource.PlayOneShot(clickSound);
    }

    /**
     * Plays the set alert sound
     */
    public void Alert()
    {
        if (audioSource && alertSound)
            audioSource.PlayOneShot(alertSound);
    }
}