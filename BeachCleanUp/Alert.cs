using UnityEngine;

public class Alert : MonoBehaviour
{
    [SerializeField] [Tooltip("The alert sound")]
    private AudioSource audioSource;

    [SerializeField] [Tooltip("The alert game object")]
    private GameObject alertIcon;

    [SerializeField] [Tooltip("The transform of the game object to appear above")]
    private Transform appearAbove;

    [SerializeField] [Tooltip("How high to place the alert above the game object")]
    private float heightOffset = 10f;

    /**
     * Creates an alert icon instance and sets up the animator
     */
    private void Start()
    {
        alertIcon = Instantiate(alertIcon);

        alertIcon.SetActive(false);
    }

    /**
     * Enables the alert icon and plays the associated sound/ainimation
     */
    public void Play()
    {
        if (audioSource)
            audioSource.Play();

        var pos = appearAbove.position;
        pos.y += heightOffset;
        pos.x -= 3f;
        alertIcon.transform.position = pos;

        alertIcon.SetActive(true);
    }

    /**
     * Makes the icon invisible
     */
    public void Stop()
    {
        alertIcon.SetActive(false);
    }
}