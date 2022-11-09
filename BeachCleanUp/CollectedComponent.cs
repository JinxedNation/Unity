using System.Linq;
using UnityEngine;

/**
 * @class CollectedComponent
 * @brief Wrapper for singleton class so it can be accessed by components
 *
 * @author Chase Percy
 * @date 26/04//2022
 *
 */
public class CollectedComponent : MonoBehaviour
{
    /**
     * cooldown for removing items too fast
     */
    private float _cooldown;

    /**
     * Todo: fix the issue with events triggering over three frames
     */
    private void Update()
    {
        if (_cooldown >= 0) _cooldown -= Time.deltaTime;
    }

    /**
     * Add a clickable item to the list
     */
    public void Add(Collectable item)
    {
        Collected.Instance.Add(item);
        item.gameObject.SetActive(false);
    }

    /**
     * Removes the last collected item and sets the object to active again
     */
    public void RemoveLast()
    {
        var collected = Collected.Instance.GetCollected();

        if (collected.Count <= 0 || _cooldown > 0) return;

        collected.Last().gameObject.SetActive(true);
        Collected.Instance.Remove(collected.Last());
        if (collected.Count > 0)
        {
            FindObjectOfType<UIManager>().UpdateImage(collected.Last().GetName(), collected.Last().GetImage());
        }
        else
        {
            FindObjectOfType<UIManager>().UpdateImage(string.Empty, null);
        }

        _cooldown = 0.5f;
        Collected.Instance.SetCollected(collected);
    }

    /**
     * Returns the last collected item
     */
    public Collectable GetLast()
    {
        return Collected.Instance.GetCollected().Last();
    }

    /**
     * Clears all collected items.
     */
    public void Clear()
    {
        Collected.Instance.Clear();
    }
}