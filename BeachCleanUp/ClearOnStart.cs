using UnityEngine;

public class ClearOnStart : MonoBehaviour
{
    /**
     * Clears the instance upon starting a new beach clean up mini-game
     */
    void Start()
    {
        Collected.Instance.Clear();
    }
}