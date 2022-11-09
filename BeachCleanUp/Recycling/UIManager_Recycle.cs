using UnityEngine;
using UnityEngine.UI;

/**
 * @class UIManager_Recycle
 * @brief Class to manage the Recycling related UI
 *
 * @author Chase Percy
 * @date 29/04//2022
 *
 */
public class UIManager_Recycle : MonoBehaviour
{
    [SerializeField] [Tooltip("The Time text field")]
    private Text time;

    [SerializeField] [Tooltip("The Image Name text field")]
    private Text imageNameText;

    [SerializeField] [Tooltip("The remaining item count")]
    private Text remainingItemCount;

    [SerializeField] [Tooltip("The Image field")]
    private Image imageContainer;

    /**
     * The time the mini-game started
     */
    private float _startTime;

    private void Start()
    {
        _startTime = Time.time;
    }

    private void Update()
    {
        time.text = "Time: " + (int) (Time.time - _startTime);
    }

    /**
     * Updates the last collected items image and text.
     */
    public void UpdateImage(string imageName, Sprite sprite)
    {
        if (sprite != null)
        {
            imageContainer.gameObject.SetActive(true);
            imageContainer.sprite = sprite;
            imageNameText.text = imageName;
        }
        else
        {
            imageContainer.gameObject.SetActive(false);
            imageNameText.text = string.Empty;
        }

        remainingItemCount.text = "Items Left: " + Collected.Instance.GetCollected().Count;
    }
}