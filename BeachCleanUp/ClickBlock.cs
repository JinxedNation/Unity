using UnityEngine;

/**
 * @class ClickBlock
 * @brief Class to block clicking of other elements in a scene
 *
 * @author Chase Percy
 * @date 10/05/2022
 *
 */
public class ClickBlock : MonoBehaviour
{
    [SerializeField] [Tooltip("The blank ui blocker")]
    private GameObject blocker;

    /**
     * Tracks if the blocker is active
     */
    private bool _isActive;

    /**
     * Enables the blocker
     */
    public void Enable()
    {
        blocker.SetActive(true);
        _isActive = true;
    }

    /**
     * Disables the blocker
     */
    public void Disable()
    {
        blocker.SetActive(false);
        _isActive = false;
    }

    /**
     * Returns the blockers state
     */
    public bool IsActive()
    {
        return _isActive;
    }
}
