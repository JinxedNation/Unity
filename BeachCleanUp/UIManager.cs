using UnityEngine;
using UnityEngine.UI;

/**
 * @class UIManager
 * @brief Class to manage the Beach cleanup game related UI
 *
 * @author Chase Percy
 * @date 26/04//2022
 *
 */
public class UIManager : MonoBehaviour
{
    [SerializeField] [Tooltip("The Time text field")]
    private Text time;

    [SerializeField] [Tooltip("The Image Name text field")]
    private Text imageNameText;

    [SerializeField] [Tooltip("The Image field")]
    private Image imageContainer;

    [SerializeField] [Tooltip("The button for removing items")]
    private GameObject removeButton;

    [SerializeField] [Tooltip("The button for finishing the level")]
    private GameObject finishButton;

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
            removeButton.SetActive(true);
            finishButton.SetActive(Collected.Instance.HasWaste());
        }
        else
        {
            imageContainer.gameObject.SetActive(false);
            removeButton.SetActive(false);
            finishButton.SetActive(false);
            imageNameText.text = string.Empty;
        }
    }
}